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
    public class OrderRepository
    {
        HttpClient client = new HttpClient();

        public OrderRepository()
        {
            client.BaseAddress = new Uri("http://localhost:51820/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Order>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            HttpResponseMessage response = await client.GetAsync("api/order/all", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Order>>(stringResult);
            }
            return new List<Order>();
        }

        public async Task AddAsync(Order order)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/Order/1");

            string json = JsonConvert.SerializeObject(order);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }

        public async Task AddProductToOrderAsync(Order order, Product product)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/Order/1" + product.Id);

            string json = JsonConvert.SerializeObject(order);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }

        public async Task EditAsync(Order order)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "api/Order/1/" + order.Id);

            string json = JsonConvert.SerializeObject(order);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }

        public async Task RemoveAsync(Order order)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "api/Order/" + order.Id);

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
