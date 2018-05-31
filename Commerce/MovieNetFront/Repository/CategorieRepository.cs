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
    public class CategorieRepository
    {
        HttpClient client = new HttpClient();

        public CategorieRepository()
        {
            client.BaseAddress = new Uri("http://localhost:51820/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Categorie>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            HttpResponseMessage response = await client.GetAsync("api/Categorie", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var stringResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Categorie>>(stringResult);
            }
            return new List<Categorie>();
        }

        public async Task AddAsync(Categorie categorie)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/Categorie");

            string json = JsonConvert.SerializeObject(categorie);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }

        public async Task EditAsync(Categorie categorie)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "api/Categorie/" + categorie.Id);

            string json = JsonConvert.SerializeObject(categorie);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
            }
            else
            {
            }
        }

        public async Task RemoveAsync(Categorie categorie)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "api/Categorie/" + categorie.Id);

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
