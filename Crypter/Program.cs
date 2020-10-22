using System;
using System.IO;
using Newtonsoft.Json;

namespace PollyPlayer
{
    class Crypter
    {
        static void Main(string[] args)
        {
            MainForm.Creds creds = new MainForm.Creds();

            Console.WriteLine("Please enter your AWS access key.");
            creds.accessKey = Console.ReadLine().Trim();

            Console.WriteLine("Please enter your AWS secret access key.");
            creds.secretKey = Console.ReadLine().Trim();
            
            string key = null;
            while (String.IsNullOrWhiteSpace(key) || key.Length != 32)
            {
                Console.WriteLine("Please enter your encryption password. This must be EXACTLY 32 alphanumeric characters.");
                key = Console.ReadLine().Trim();
            }
            
            string json = JsonConvert.SerializeObject(creds, Formatting.Indented);
            string encryptedJson = Crypto.EncryptString(key, json);

            Console.WriteLine("Please enter a file name where you want to store the encrypted result.");
            string fileName = Console.ReadLine().Trim();

            File.WriteAllText(fileName, encryptedJson);

            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey();
        }
    }
}