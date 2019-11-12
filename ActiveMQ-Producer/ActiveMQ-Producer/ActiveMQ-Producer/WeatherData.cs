using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ActiveMQ_Producer
{
    public class DataObject
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }
    class WeatherData
    {
        public WeatherData() { }

        public void FetchData()
        {
            var constProject = new Constants();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(constProject.WEATHERAPI[0]);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(constProject.WEATHERAPI[1] + constProject.WEATHERAPI[2]).Result;
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<string>>().Result.ToString();
                foreach (var d in dataObjects)
                {
                    Console.WriteLine("{0}");
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            client.Dispose();
        }

    }
}
