using BankEntity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;



namespace BankMvcApp
{



    public static class Utilities
    {
        public async static Task<T> SendDataToApi<T>(
            this Controller controller,
            string baseUri,
            string requestUrl,
            T model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                var postTask = await client.PostAsJsonAsync<T>(requestUrl,model);

                var result = postTask;
                if (result.IsSuccessStatusCode)
                {
                    return default(T);
                }
                return default(T);
            }
        }

        public async static Task<T> GetResponseFromApi<T>(
            this Controller controller,
            string baseUri,
            string requestUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "Token");

                var response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<T>(
                        await response.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    return result;
                }
                return default(T);
            }
        }

    }
}


