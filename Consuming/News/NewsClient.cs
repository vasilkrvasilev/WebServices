using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace News
{
    class NewsClient
    {
        static void Main()
        {
            Console.WriteLine("Enter query string");
            string query = Console.ReadLine();
            Console.WriteLine("Enter number of articles");
            int number = int.Parse(Console.ReadLine());

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://api.feedzilla.com/v1/articles/search.json");
            string search = "?q=" + query + "&count=" + number;
            PrintArticles(httpClient, search);
        }

        private static async void PrintArticles(HttpClient httpClient, string search)
        {
            var response = httpClient.GetAsync(search).Result;

            string articleJson = await response.Content.ReadAsStringAsync();
            ArticleList articleDeserialized = JsonConvert.DeserializeObject<ArticleList>(articleJson);
            foreach (var item in articleDeserialized.Articles)
            {
                Console.WriteLine("Deserialized\n\r(Title): {0},\n\r(Url): {1}\n\r", item.Title, item.URL);
            }
        }
    }
}
