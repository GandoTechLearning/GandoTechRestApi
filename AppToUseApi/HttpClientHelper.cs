using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppToUseApi
{
    public static class HttpClientHelper
    {
        public static async Task<T> GetAsync<T>(HttpClient client, string uri)
        {
            using var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var productValue = await response.Content.ReadAsStringAsync();
                var tObject = JsonConvert.DeserializeObject<T>(productValue);
                return tObject;
            }

            return default;
        }

        public static HttpContent GetContent<T>(T tValue)
        {
            return new StringContent(JsonConvert.SerializeObject(tValue), Encoding.UTF8, "application/json");
        }
    }
}
