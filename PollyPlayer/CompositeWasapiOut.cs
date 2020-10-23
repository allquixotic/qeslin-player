using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;

namespace PollyPlayer
{
    internal class CompositeWasapiOut : ISoundOut
    {
        public WasapiOut realOut { get; private set; }
        public WasapiOut primaryOut { get; private set; }

        public static MMDevice GetDefaultDevice()
        {
            return MMDeviceEnumerator.DefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        public CompositeWasapiOut(MMDevice dev)
        {
            realOut = new WasapiOut
            {
                Device = dev
            };

            if (dev.Equals(GetDefaultDevice()))
            {
                primaryOut = null;
            }
            else
            {
                primaryOut = new WasapiOut();
            }
        }

        public float Volume { get => ((ISoundOut)realOut).Volume; set => ((ISoundOut)realOut).Volume = value; }

        public IWaveSource WaveSource => ((ISoundOut)realOut).WaveSource;

        public PlaybackState PlaybackState
        {
            get
            {
                PlaybackState p1 = realOut.PlaybackState;
                PlaybackState? p2 = primaryOut?.PlaybackState;
                if (p1 == PlaybackState.Playing || p2 == PlaybackState.Playing)
                    return PlaybackState.Playing;
                if (p1 == PlaybackState.Paused || p2 == PlaybackState.Paused)
                    return PlaybackState.Paused;
                return p1;
            }
        }

        public event EventHandler<PlaybackStoppedEventArgs> Stopped
        {
            add
            {
                ((ISoundOut)realOut).Stopped += value;
                if(primaryOut != null) ((ISoundOut)primaryOut).Stopped += value;
            }

            remove
            {
                ((ISoundOut)primaryOut).Stopped -= value;
                if (primaryOut != null) ((ISoundOut)primaryOut).Stopped += value;
            }
        }

        public void Dispose()
        {
            ((IDisposable)realOut).Dispose();
            ((IDisposable)primaryOut)?.Dispose();
        }

        public void Initialize(IWaveSource source)
        {
            if (primaryOut != null)
            {
                MemoryStream ms = new MemoryStream();
                source.WriteToWaveStream(ms);
                ms.Position = 0;
                source.Position = 0;
                WaveFileReader wfr = new WaveFileReader(ms);
                ((ISoundOut)primaryOut).Initialize(wfr);
            }
            
            ((ISoundOut)realOut).Initialize(source);
        }

        public void Pause()
        {
            ((ISoundOut)realOut).Pause();
            ((ISoundOut)primaryOut)?.Pause();
        }

        public void Play()
        {
            ((ISoundOut)realOut).Play();
            ((ISoundOut)primaryOut)?.Play();
        }

        public void Resume()
        {
            ((ISoundOut)realOut).Resume();
            ((ISoundOut)primaryOut)?.Resume();
        }

        public void Stop()
        {
            ((ISoundOut)realOut).Stop();
            ((ISoundOut)primaryOut)?.Stop();
        }
    }
}
