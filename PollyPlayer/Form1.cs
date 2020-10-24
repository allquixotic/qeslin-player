﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using CSCore;
using CSCore.Codecs.MP3;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PollyPlayer
{
    public partial class MainForm : Form
    {
        private AmazonPollyClient apc = null;
        private MySettings sett = new MySettings();
        private readonly string settingsFileName = Environment.GetFolderPath(
            Environment.SpecialFolder.UserProfile) + Path.DirectorySeparatorChar + "qeslin-settings.json";

        private readonly SortedDictionary<string, Voice> vozes = new SortedDictionary<string, Voice>();

        public struct MySettings
        {
            public string key { get; set; }
            public string credentialsFile { get; set; }
            public string voiceId { get; set; }
            public bool clearAfterSay { get; set; }
            public bool listenAndChat { get; set; }
        }

        public struct Creds
        {
            public string accessKey { get; set; }
            public string secretKey { get; set; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void SaveSettings()
        {
            File.WriteAllText(settingsFileName, JsonConvert.SerializeObject(sett, Formatting.Indented));
        }

        private void LoadSettings()
        {
            sett = JObject.Parse(File.ReadAllText(settingsFileName))
                .ToObject<MySettings>();
        }

        private void LoadVoices()
        {
            var dvr = new DescribeVoicesRequest
            {
                IncludeAdditionalLanguageCodes = true
            };
            bool firstIter = true;
            for (var resp = apc.DescribeVoices(dvr);
                firstIter || !string.IsNullOrEmpty(resp.NextToken);
                resp = apc.DescribeVoices(dvr))
            {
                foreach (var voice in resp.Voices)
                {
                    string hq = "";
                    if (voice.SupportedEngines.Contains("neural"))
                        hq = "(HD) ";
                    vozes.Add(hq + voice.Name + " (" + voice.LanguageName + ")", voice);
                }


                if (!String.IsNullOrEmpty(resp.NextToken))
                {
                    dvr.NextToken = resp.NextToken;
                }

                firstIter = false;
            }

            

            voicesListBox.DataSource = new BindingSource(vozes, null);
            voicesListBox.DisplayMember = "Key";
            voicesListBox.ValueMember = "Value";
        }

        public static Dictionary<string, MMDevice> LoadDevices()
        {
            var devices = new Dictionary<string, MMDevice>();
            foreach (var dev in MMDeviceEnumerator.EnumerateDevices(DataFlow.Render, DeviceState.Active))
            {
                devices.Add(dev.FriendlyName, dev);
            }

            return devices;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Enter))
            {
                DoSpeak();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private (string, string) GetCryptedCredentials(string diagnostic)
        {
            string crypted = null;
            string fileName = null;
            MessageBox.Show(this,
                String.IsNullOrEmpty(diagnostic)
                    ? "General error. Please pick the AWS credentials file."
                    : diagnostic, "Information");
            var ofd = new OpenFileDialog();

            while (String.IsNullOrWhiteSpace(crypted))
            {
                while (ofd.ShowDialog(this) != DialogResult.OK) ;
                try
                {
                    crypted = File.ReadAllText(ofd.FileName);
                    fileName = ofd.FileName;
                }
                catch (IOException ioe)
                {
                    MessageBox.Show(this, "Can't open file " + ofd.FileName, "Error");
                }
            }

            return (crypted, fileName);
        }

        private string GetPassword()
        {
            string retval = null;
            while(String.IsNullOrWhiteSpace(retval))
            {
                retval = Interaction.InputBox("Please enter your AWS credentials file's decryption password.",
                    "Qeslin Player - Question");
            };
            return retval;
        }

        private (MySettings, Creds, AWSCredentials) GetCredentials()
        {
            string ak = null, sak = null;
            string diagnostic = "";
            string crypted = null;
            string decrypted = null;
            bool settingsChanged = false;
            Creds creds = new Creds();
            

            //Try to look for existing credentials
            try
            {
                LoadSettings();
            }
            catch (FileNotFoundException fnfe)
            {
                diagnostic =
                    "It appears this is the first time you've started Qeslin Player. Please select your AWS credentials file.";
            }
            catch (IOException ioe)
            {
                diagnostic =
                        "There was a problem reading Qeslin Player's settings file. Please select your AWS credentials file.";
            }
            catch (JsonException)
            {
                diagnostic = "Settings file " + settingsFileName + " does not contain valid JSON.";
            }

            if (String.IsNullOrWhiteSpace(sett.credentialsFile))
            {
                (crypted, sett.credentialsFile) = GetCryptedCredentials(diagnostic);
                settingsChanged = true;
            }
            else
            {
                crypted = File.ReadAllText(sett.credentialsFile);
            }

            if (String.IsNullOrWhiteSpace(sett.key))
            {
                sett.key = GetPassword();
                settingsChanged = true;
            }

            diagnostic = null;

            try
            {
                decrypted = Crypto.DecryptString(sett.key, crypted);
            }
            catch (Exception ioe)
            {
                diagnostic = "Incorrect password or invalid data in credentials file.";
                MessageBox.Show(this, diagnostic, "Error");
                (crypted, sett.credentialsFile) = GetCryptedCredentials(diagnostic);
                sett.key = GetPassword();
                settingsChanged = true;
            }


            try
            {
                creds = JObject.Parse(decrypted).ToObject<Creds>();
            }
            catch(JsonException)
            {
                //Tried to parse the decrypted file but it's not JSON or doesn't contain required fields
            }

            ak = creds.accessKey;
            sak = creds.secretKey;
            if (String.IsNullOrWhiteSpace(ak) || String.IsNullOrWhiteSpace(sak))
            {
                MessageBox.Show(this, "Something went wrong trying to determine AWS access key and secret access key.",
                    "Error");
            }

            if (settingsChanged)
            {
                SaveSettings();
            }

            return (sett, creds, new BasicAWSCredentials(ak, sak));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Creds mycreds;
            AWSCredentials creds;
            (sett, mycreds, creds) = GetCredentials();
            lblCredsFile.Text = sett.credentialsFile;
            listenAndChat.Checked = sett.listenAndChat;
            delAfterSay.Checked = sett.clearAfterSay;
            apc = new AmazonPollyClient(creds, RegionEndpoint.USEast1);
            LoadVoices();

            //Load saved voice from JSON
            Voice voc = vozes.Values.FirstOrDefault((voz) => voz.Id.Value.Equals(sett.voiceId));
            if (voc != null)
            {
                voicesListBox.SelectedValue = voc;
            }


            var devs = MainForm.LoadDevices();
            cbSoundDevice.DataSource = new BindingSource(devs, null);
            cbSoundDevice.DisplayMember = "Key";
            cbSoundDevice.ValueMember = "Value";
            MMDevice devToSet = null;
            foreach (var dev in devs)
            {
                if (dev.Key.ToLower().Trim().Equals("vb-audio virtual cable"))
                {
                    devToSet = dev.Value;
                    break;
                }
                if (dev.Key.ToLower().Contains("vb-audio virtual"))
                {
                    devToSet = dev.Value;
                    break;
                }
                if (dev.Key.ToLower().Contains("virtual"))
                {
                    devToSet = dev.Value;
                    break;
                }
            }
            cbSoundDevice.SelectedValue = devToSet;
            this.voicesListBox.SelectedIndexChanged += new System.EventHandler(this.voicesListBox_SelectedIndexChanged);
        }

        private Stream RenderAudio(Voice voice)
        {
            var ssr = new SynthesizeSpeechRequest
            {
                Engine = voice.SupportedEngines.Contains("neural") ? Engine.Neural : Engine.Standard,
                OutputFormat = "mp3",
                LanguageCode = voice.LanguageCode,
                VoiceId = voice.Id,
                TextType = TextType.Text,
                SampleRate = "24000",
                Text = speechTextBox.Text
            };
            var resp = apc.SynthesizeSpeech(ssr);
            var mp3 = new MemoryStream();
            resp.AudioStream.CopyTo(mp3);
            mp3.Position = 0;
            return mp3;
        }

        private void DoSpeak()
        {
            sayItButton.Enabled = false;
            var dsd = (MMDevice) cbSoundDevice.SelectedValue;
            var voice = (Voice)voicesListBox.SelectedValue;

            var tsk = Task.Run(() =>
            {
                var mp3 = RenderAudio(voice);

                //Contains the sound to play
                using (IWaveSource soundSource = GetSoundSource(mp3))
                {
                    //SoundOut implementation which plays the sound
                    using (ISoundOut soundOut = GetSoundOut(dsd))
                    {
                        //Tell the SoundOut which sound it has to play
                        soundOut.Initialize(soundSource);
                        //Play the sound
                        soundOut.Play();

                        while (soundOut.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(250);
                        }

                        //Stop the playback
                        soundOut.Stop();
                    }
                }

                this.Invoke((MethodInvoker) delegate
                {
                    if (delAfterSay.Checked)
                    {
                        speechTextBox.Clear();
                    }

                    sayItButton.Enabled = true;
                    speechTextBox.Focus();
                });
            });
        }

        private ISoundOut GetSoundOut(MMDevice dso)
        {
            ISoundOut retval;
            if (listenAndChat.Checked)
            {
                retval = new CompositeWasapiOut(dso);
            }
            else
            {
                retval = new WasapiOut();
                ((WasapiOut)retval).Device = dso;
            }
            
            return retval;
        }

        private IWaveSource GetSoundSource(Stream stream)
        {
            // Instead of using the CodecFactory as helper, you specify the decoder directly:
            return new DmoMp3Decoder(stream);
        }

        private void SayItButton_Click(object sender, EventArgs e)
        {
            DoSpeak();
        }

        private void voicesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dynamic v = voicesListBox.SelectedValue;


            if (v.GetType() == typeof(Voice))
            {
                sett.voiceId = v?.Id?.Value;
            }
            else
            {
                sett.voiceId = v?.Value?.Id?.Value;
            }
            SaveSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to delete the settings file " + settingsFileName + "?",
                "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    File.Delete(settingsFileName);
                }
                catch (IOException) { }

                MessageBox.Show(this,
                    "The settings file has been cleared. Restart the program to pick new credentials.", "Message");
                this.Close();
            }
        }

        private void saveToFile_Click(object sender, EventArgs e)
        {
            var mp3 = RenderAudio((Voice)voicesListBox.SelectedValue);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "mp3";
            sfd.OverwritePrompt = true;
            sfd.Filter = "MP3 (*.mp3)|*.mp3";

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                using (var fd = File.OpenWrite(sfd.FileName))
                {
                    mp3.CopyTo(fd);
                    fd.Flush();
                }

                MessageBox.Show(this, "Saved " + sfd.FileName + " with mp3 data.", "Information");
            }
        }

        private void delAfterSay_CheckedChanged(object sender, EventArgs e)
        {
            sett.clearAfterSay = delAfterSay.Checked;
            SaveSettings();
        }

        private void listenAndChat_CheckedChanged(object sender, EventArgs e)
        {
            sett.listenAndChat = listenAndChat.Checked;
            SaveSettings();
        }

        private void btnNonsense_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var rand = new Random();
            int numWords = rand.Next(4, 31);
            for (int i = 0; i < numWords; i++)
            {
                int wordLength = rand.Next(2, 15);
                for (int j = 0; j < wordLength; j++)
                {
                    if (rand.Next(1, 5) == 1)
                    {
                        int vowel = rand.Next(1, 7);
                        switch (vowel)
                        {
                            case 1:
                                sb.Append('a');
                                break;
                            case 2:
                                sb.Append('e');
                                break;
                            case 3:
                                sb.Append('i');
                                break;
                            case 4:
                                sb.Append('o');
                                break;
                            case 5:
                                sb.Append('u');
                                break;
                            case 6:
                                sb.Append('y');
                                break;
                        }
                    }
                    else
                    {
                        char c = (char)rand.Next(0x0061, 0x007B);
                        sb.Append(c);
                    }
                }

                sb.Append(" ");
            }

            speechTextBox.Text = sb.ToString().Trim();

        }
    }
}
