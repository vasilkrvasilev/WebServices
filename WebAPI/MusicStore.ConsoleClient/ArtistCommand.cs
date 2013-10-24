using MusicStore.Models;
using MusicStore.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.ConsoleClient
{
    public static class ArtistCommand
    {
        public static void GetArtists(HttpClient client)
        {
            HttpResponseMessage response =
                client.GetAsync("api/artists").Result;

            if (response.IsSuccessStatusCode)
            {
                var artists = response.Content
                    .ReadAsAsync<IEnumerable<ArtistModel>>().Result;
                foreach (var item in artists)
                {
                    Console.WriteLine("{0,4} {1} {2} {3:dd.MM.yyyy}",
                        item.ArtistId, item.Name, item.Country, item.DateOfBirth);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void GetArtist(HttpClient client)
        {
            Console.WriteLine("Enter artist id");
            string id = Console.ReadLine();
            HttpResponseMessage response =
                client.GetAsync(string.Format("api/artists/{0}", id)).Result;

            if (response.IsSuccessStatusCode)
            {
                var artist = response.Content
                    .ReadAsAsync<ArtistModel>().Result;

                Console.WriteLine("{0,4} {1} {2} {3:dd.MM.yyyy}",
                    artist.ArtistId, artist.Name, artist.Country, artist.DateOfBirth);
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void AddArtistJson(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Enter artist name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter artist country");
            string country = Console.ReadLine();
            Console.WriteLine("Enter artist birth date");
            DateTime birthDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", new CultureInfo("en-US"));
            Artist artist = new Artist { Name = name, Country = country, DateOfBirth = birthDate };
            HttpResponseMessage response =
                client.PostAsJsonAsync("api/artists", artist).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist added");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void AddArtistXml(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            Console.WriteLine("Enter artist name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter artist country");
            string country = Console.ReadLine();
            Console.WriteLine("Enter artist birth date");
            DateTime birthDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", new CultureInfo("en-US"));
            Artist artist = new Artist { Name = name, Country = country, DateOfBirth = birthDate };
            HttpResponseMessage response =
                client.PostAsXmlAsync("api/artists", artist).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist added");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void PutArtistJson(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Enter artist id");
            string id = Console.ReadLine();

            Console.WriteLine("Enter artist name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter artist country");
            string country = Console.ReadLine();
            Console.WriteLine("Enter artist birth date");
            DateTime birthDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", new CultureInfo("en-US"));
            Artist artist = new Artist { Name = name, Country = country, DateOfBirth = birthDate };
            HttpResponseMessage response =
                client.PutAsJsonAsync(string.Format("api/artists/{0}", id), artist).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist changed");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void PutArtistXml(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            Console.WriteLine("Enter artist id");
            string id = Console.ReadLine();

            Console.WriteLine("Enter artist name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter artist country");
            string country = Console.ReadLine();
            Console.WriteLine("Enter artist birth date");
            DateTime birthDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", new CultureInfo("en-US"));
            Artist artist = new Artist { Name = name, Country = country, DateOfBirth = birthDate };
            HttpResponseMessage response =
                client.PutAsXmlAsync(string.Format("api/artists/{0}", id), artist).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Artist changed");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
