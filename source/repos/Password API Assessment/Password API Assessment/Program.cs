using Password_API_Assessment.Data;
using Password_API_Assessment.Interfaces;
using System;
using System.Threading.Tasks;

namespace Password_API_Assessment
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string username = "John";
            string dictFileName = "dict.txt";
            string zipFileName = "submission.zip";
            string authUrl = "http://recruitment.warpdevelopment.co.za/api/authenticate";

            IDictionaryGenerator dictionaryGenerator = new DictionaryGenerator();
            IAuthenticator authenticator = new Authenticator();
            IFileCompressor fileCompressor = new FileCompressor();
            IFileUploader fileUploader = new FileUploader();

            // Generate dictionary
            dictionaryGenerator.GenerateDictionary(dictFileName, "password");

            // Authenticate
            string uploadUrl = await authenticator.Authenticate(authUrl, username, dictFileName);

            if (string.IsNullOrEmpty(uploadUrl))
            {
                Console.WriteLine("Failed to authenticate. No valid password found.");
                return;
            }

            // Create ZIP file
            string[] filesToInclude = { "CV.pdf", "dict.txt", "Program.cs" };
            fileCompressor.CreateZipFile(zipFileName, filesToInclude);

            // Submit CV
            await fileUploader.UploadFile(uploadUrl, zipFileName);
        }
    }
}
