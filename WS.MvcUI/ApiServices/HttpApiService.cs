
using Microsoft.Net.Http.Headers;
using System.Drawing.Text;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using WS.MvcUI.Areas.AdminPanel.Models.ApiTypes;

namespace WS.MvcUI.ApiServices
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpApiService(IHttpClientFactory httpclientfactory)
        {
            _httpClientFactory = httpclientfactory;
        }

        public async Task<bool> Delete(string requestUri)
        {

            //servise gönderecegim request 
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://localhost:5003/api{requestUri}"),
                Headers = { { HeaderNames.Accept, "application/json" } }
            };
            // servisle haberleşecek olan client nesnesi elde ediliyor
            var client = _httpClientFactory.CreateClient();

            //servise request gönderiliyor
            var responseMessage = await client.SendAsync(requestMessage);


            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<T> GetData<T>(string requestUri, string token = null)

        {
            T response = default(T);


            //servise gönderecegim request 
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://localhost:5003/api{requestUri}"),
                Headers = { { HeaderNames.Accept, "application/json" } }
            };

            //requestimizin içine tokenimizide eklemiş olduk
            if (!string.IsNullOrEmpty(token))
                requestMessage.Headers.Authorization =new AuthenticationHeaderValue("Bearer", token);

            // servisle haberleşecek olan client nesnesi elde ediliyor
            var client = _httpClientFactory.CreateClient();

            //servise request gönderiliyor
            var responseMessage = await client.SendAsync(requestMessage);

            // servisten gelen yanıt T tipine dönüstürülüyor
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;

        }

        public async Task<T> PostData<T>(string requestUri, string jsonData)
        {
            T response = default(T);


            //servise gönderecegim request 
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://localhost:5003/api{requestUri}"),
                Headers = { { HeaderNames.Accept, "application/json" } },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")

            };
            // servisle haberleşecek olan client nesnesi elde ediliyor
            var client = _httpClientFactory.CreateClient();

            //servise request gönderiliyor
            var responseMessage = await client.SendAsync(requestMessage);

            // servisten gelen yanıt T tipine dönüstürülüyor
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }
    }
}
