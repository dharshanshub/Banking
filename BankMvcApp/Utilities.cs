using BankEntity;
using Microsoft.AspNetCore.Http;
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
        public async static Task<TResult> SendDataToApi<TInput, TResult>(
            this Controller controller,
            string baseUri,
            string requestUrl,
            TInput model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", 
                    controller.HttpContext.Session.GetString("Token"));


                var response = await client.PostAsJsonAsync(requestUrl, model);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TResult>();
                    return result;
                }

                return default(TResult);
            }

        }

        public async static Task<TResult> GetResponseFromApi<TResult>(
            this Controller controller,
            string baseUri,
            string requestUrl,
            int idParameter)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "Token");

                var response = await client.GetAsync($"{requestUrl}/{idParameter}");
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<TResult>(
                        await response.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    return result;
                }
                return default(TResult);
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


