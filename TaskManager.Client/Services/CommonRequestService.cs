using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TaskManager.Client.Models;

namespace TaskManager.Client.Services
{
    public abstract class CommonRequestService
    {
        public const string HOST = "http://localhost:1329/api/";
        private static readonly HttpClient httpClient = new HttpClient();



        protected string GetDataByUrl(string url, AuthToken token, HttpMethodType httpMethod, string userName = null, string password = null, Dictionary<string, string> parameters = null)
        {
            string result = string.Empty;

            if (userName != null && password != null)
            {
                string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
            }
            else if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            }
            try
            {

                if (httpMethod == HttpMethodType.POST)
                {
                    var content = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());
                    HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                }
                else if (httpMethod == HttpMethodType.GET)
                {
                    if (parameters != null)
                    {
                        var query = new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result;
                        url += "?" + query;
                    }
                    HttpResponseMessage response = httpClient.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        result = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch { }
            return result;
        }
        protected HttpStatusCode SendDataByUrl(HttpMethod method, string url, AuthToken token, string data)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);

            var content = new StringContent(data, Encoding.UTF8, "application/json");

            if (method == HttpMethod.Post)
                result = client.PostAsync(url, content).Result;

            if (method == HttpMethod.Patch)
                result = client.PatchAsync(url, content).Result;
            return result.StatusCode;
        }
        protected HttpStatusCode DeleteDataByUrl(string url, AuthToken token)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);
            result = client.DeleteAsync(url).Result;

            return result.StatusCode;
        }
    }
}
