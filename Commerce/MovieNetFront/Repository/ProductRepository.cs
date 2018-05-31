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
    public class ProductRepository
    {
        HttpClient client = new HttpClient();

        public CategorieRepository()
        {
            client.BaseAddress = new Uri("http://localhost:51820/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Product>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            HttpResponseMessage response = await client.GetAsync("api/Product", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(stringResult);
            }
            return new List<Product>();
        }

        public async Task AddAsync(Product product)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/Product");

            string json = JsonConvert.SerializeObject(product);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }

        public async Task EditAsync(Product product)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "api/Product/" + product.Id);

            string json = JsonConvert.SerializeObject(product);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }

        public async Task RemoveAsync(Product product)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "api/Product/" + product.Id);

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }
    }
}
