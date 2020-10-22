# Qeslin Player for Windows
> Use Amazon Polly voices with VBCABLE for TTS using common VoIP applications.

## End-User Instructions

1. A developer should have provided you with an .exe setup file of Qeslin Player. Run that, and follow the prompts, answering "Next", "Yes" or "OK" to all.
2. Run the program "Qeslin Player" from the start menu.
3. When prompted, navigate to your home directory and select the file "qeslin-iam.json".
4. When prompted, enter the password supplied to you by your developer.
5. Test out the program with a few sample strings of text, making sure to select an output device that you can hear through your speakers or headphones.
6. Go into your favorite Voice Over IP (VoIP) application (examples: Skype, Discord, ...) and set the "Input" device to the VB-CABLE device.
7. Set your VoIP application to Push To Talk.
8. Get a friend to help you test and tell you whether they can hear your speech as you enter lines into Qeslin Player.


## Developer Instructions

These notes are for *developers* planning to create a distribution of Qeslin Player.

## Setting Up Your Development Environment

1. Install [Visual Studio 2019 - Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
2. Install vcpkg
````
> git clone https://github.com/Microsoft/vcpkg.git
> cd vcpkg

PS> .\bootstrap-vcpkg.bat
````

## Qeslin Player Installer Preparation

1. Download VB-Cable from https://vb-audio.com/Cable/ and place the extracted folder in the root of the repository.
2. Download InnoSetup from https://jrsoftware.org/isdl.php and run the setup installer.
3. Create credentials as per the instructions under "Setup AWS IAM Account" and "Create Credentials" headings.
4. Run InnoSetup's "Compile" option against the .iss file. It will build in the "installer" folder of the root of the repository clone.
5. Distribute the resulting .exe to your end-users and give them the password you used to create the encrypted credential.

## Setup AWS IAM Account

1. Create an AWS account if you don't have one.
2. Login to [your AWS account](https://console.aws.amazon.com/console/home)
3. After you've logged in, click `Services` from the top menu bar, then type `iam` in the search box. Click `IAM` when it pops up.
4. On the left, click `Users`
5. Click `Add User`
6. Type in `polly-windows-user` (you can use any name)
7. Click the `Programmatic access` checkbox and leave `AWS Management Console access` unchecked
8. Click `Next: Permissions`
9. Click `Attach existing policies directly`
10. At the bottom of the page, in the search box next to `Filter: Policy type`, type `polly`
11. Click the checkbox next to `AmazonPollyFullAccess`
12. Click `Next: Review`
13. Click `Create user`
> **IMPORTANT:** *Don't close the web page. You'll need both the `Access key ID` and the `Secret access key` in the next step.*

## Create Credentials

1. Open the PollyPlayer.sln solution in Visual Studio 2019.
2. Build the solution.
3. Run PollyPlayer\bin\Debug\Crypter.exe
4. Follow the instructions in the console to create your *encrypted IAM credentials file*. You will need your IAM user's access key and secret access key.
5. Make sure you save your 32-character encryption password! You will need to provide this to your end user(s).
6. Copy the IAM credentials file to the folder "PrivateData" in your local clone of this GitHub repository's root directory.

--------

# Old instructions from AWS

## Installation QuickStart

## Step 1: Install the AWS Client (*if it's not already installed on your machine*) ##

Follow the instructions on [this page](http://docs.aws.amazon.com/cli/latest/userguide/awscli-install-windows.html) to install the AWS Command Line Interface on Microsoft Windows.

> **IMPORTANT:** If you're not sure what to do here, follow the instructions in the `MSI Installer` section.

## Step 2: Setup your AWS user account ##

1. Login to [your AWS account](https://console.aws.amazon.com/console/home)
2. After you've logged in, click `Services` from the top menu bar, then type `iam` in the search box. Click `IAM` when it pops up.
4. On the left, click `Users`
5. Click `Add User`
6. Type in `polly-windows-user` (you can use any name)
7. Click the `Programmatic access` checkbox and leave `AWS Management Console access` unchecked
8. Click `Next: Permissions`
9. Click `Attach existing policies directly`
10. At the bottom of the page, in the search box next to `Filter: Policy type`, type `polly`
11. Click the checkbox next to `AmazonPollyFullAccess`
12. Click `Next: Review`
13. Click `Create user`
> **IMPORTANT:** *Don't close the web page. You'll need both the `Access key ID` and the `Secret access key` in the next step.*


## Step 3: Configure the AWS Client
> Amazon Polly TTS Engine for Windows requires an AWS profile called `polly-windows`. It's in the roadmap to allow custom profiles. 

1. Open a Windows command prompt.
2. Type this command:

         aws configure --profile polly-windows

3. For `AWS Access Key ID` and `AWS Secret Access Key`, use the values from the previous step.
4. For `Default region name`, you can hit `Enter` for the default (`us-east-1`) or enter a different region. **Make sure to use all lower-case. This is case-sensitive.**
5. For `Default output format`, just hit `Enter`
6. Verify this worked by running this command. You should see a list of voices:

         aws --profile polly-windows polly describe-voices

## Step 4: Install Amazon Polly TTS Engine for Windows
Download and run the [installer](https://s3.amazonaws.com/polly-tts-windows/setup.exe)

Verify that the installer worked by opening `Control Panel` and go to `Change text to speech settings`. In the `Voice selection` drop-down, you should see all of the Amazon Polly voices. Picking a voice will automatically play a sample.

## Adobe Captivate Support
Even though there is a drop-down voice selection in the `Audio / Speech Management` window, apparently Adobe Captivate only uses the default voice you choose in Windows Control Panel. The voice selection in Captivate is completely ignored.

To get around this, you can manually specify the voice name that you want to use by using the `voice` tag. 
 
### Instructions for Manually Specifying a Voice in Adobe Captivate ###

1.	Set the default Windows voice in Control Panel to **any** of the Amazon Polly voices.
2.	Start Adobe Captivate and open any of your previous documents.
3.	Choose `Speech Management` from the `Audio` menu
4.	(See the screenshot below): Where you enter the text to speak, put the highlighted yellow tags around the text, replacing the name with the voice name you want to use. For example, `<speak><voice name="Matthew">Hi, this is Matthew</voice></speak>` will automatically speak the phrase *Hi, this is Matthew* in the **Matthew** voice and  `<speak><voice name="Ivy">Hi, this is Ivy</voice></speak>` will say *Hi, this is Ivy* in the **Ivy** voice.
 
Click [here](http://docs.aws.amazon.com/polly/latest/dg/voicelist.html) for the complete list of Polly voice names.
 
> **IMPORTANT:** It doesn’t matter which voice you select at the top or the left side menu, as long as it’s a Polly voice. The <voice> tag will override those selections. However, if you choose a non-Polly voice as the Windows default voice, then the <voice> tag will NOT work with the Polly voices.
 
Also, you can only use one `<speak>` tag per block of text, and you can only use one `<voice>` tag per `<speak>` tag (e.g., you can’t do `<speak><voice name="ivy">I’m Ivy.</voice><voice name="Matthew">I’m Matthew</voice></speak>`).

![](https://i.imgur.com/LMlNszU.png)
