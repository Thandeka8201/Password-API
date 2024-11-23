using Newtonsoft.Json;
using Password_API_Assessment.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Password_API_Assessment.Data
{
    public class FileUploader : IFileUploader
    {
        public async Task UploadFile(string url, string zipFileName)
        {
            Console.WriteLine("Submitting CV...");

            byte[] zipBytes = File.ReadAllBytes(zipFileName);
            string base64Zip = Convert.ToBase64String(zipBytes);

            var payload = new
            {
                Data = base64Zip,
                Name = "Your Name",
                Surname = "Your Surname",
                Email = "email@domain.com"
            };

            // Use Newtonsoft.Json's JsonConvert for serialization
            string jsonPayload = JsonConvert.SerializeObject(payload);

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Submission successful!");
                }
                else
                {
                    Console.WriteLine("Failed to submit CV.");
                }
            }
        }
    }
}

