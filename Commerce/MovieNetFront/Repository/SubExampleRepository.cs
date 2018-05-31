//using System;
//using System.Net;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading;
//using Newtonsoft.Json;
//using API.Model;

//namespace MovieNetFront.Repository
//{
//    public class SubExampleRepository
//    {
//        HttpClient client = new HttpClient();

//        public SubExampleRepository()
//        {
//            client.BaseAddress = new Uri("http://localhost:51820/");
//            client.DefaultRequestHeaders.Accept.Add(
//                new MediaTypeWithQualityHeaderValue("application/json"));
//        }

//        public async Task<List<SubExample>> GetAsync(CancellationToken cancellationToken)
//        {
//            cancellationToken.ThrowIfCancellationRequested();

//            HttpResponseMessage response = await client.GetAsync("api/SubExample", cancellationToken);
//            if (response.IsSuccessStatusCode)
//            {
//                var stringResult = await response.Content.ReadAsStringAsync();
//                return JsonConvert.DeserializeObject<List<SubExample>>(stringResult);
//            }
//            return new List<SubExample>();
//        }

//        public async Task AddAsync(SubExample example)
//        {
//            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/SubExample");

//            string json = JsonConvert.SerializeObject(example);

//            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

//            HttpResponseMessage response = await client.SendAsync(request);

//            if (response.IsSuccessStatusCode)
//            {
//            }
//            else
//            {
//            }
//        }

//        public async Task EditAsync(SubExample example)
//        {
//            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "api/SubExample/" + example.Id);

//            string json = JsonConvert.SerializeObject(example);

//            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

//            HttpResponseMessage response = await client.SendAsync(request);

//            if (response.IsSuccessStatusCode)
//            {
//            }
//            else
//            {
//            }
//        }

//        public async Task RemoveAsync(SubExample example)
//        {
//            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "api/SubExample/" + example.Id);

//            HttpResponseMessage response = await client.SendAsync(request);

//            if (response.IsSuccessStatusCode)
//            {
//            }
//            else
//            {
//            }
//        }
//    }
//}
