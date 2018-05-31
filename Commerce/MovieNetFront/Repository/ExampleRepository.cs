using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;
using API.Model;

namespace MovieNetFront.Repository
{
    public class ExampleRepository
    {
        HttpClient client = new HttpClient();

        public ExampleRepository()
        {
            client.BaseAddress = new Uri("http://localhost:51820/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<User>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            HttpResponseMessage response = await client.GetAsync("api/User", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(stringResult);
            }
            return new List<User>();
        }


        //public async Task<List<Example>> GetAsync(CancellationToken cancellationToken)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    HttpResponseMessage response = await client.GetAsync("api/Example", cancellationToken);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var stringResult = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<List<Example>>(stringResult);
        //    }
        //    return new List<Example>();
        //}

        //public async Task AddAsync(Example example)
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/Example");

        //    string json = JsonConvert.SerializeObject(example);

        //    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await client.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //    }
        //    else
        //    {
        //    }
        //}

        //public async Task EditAsync(Example example)
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "api/Example/" + example.Id);

        //    string json = JsonConvert.SerializeObject(example);

        //    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await client.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //    }
        //    else
        //    {
        //    }
        //}

        //public async Task RemoveAsync(Example example)
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "api/Example/" + example.Id);

        //    HttpResponseMessage response = await client.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //    }
        //    else
        //    {
        //    }
        //}
    }
}
