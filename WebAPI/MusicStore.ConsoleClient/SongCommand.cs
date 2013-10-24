using MusicStore.Models;
using MusicStore.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.ConsoleClient
{
    public static class SongCommand
    {
        public static void GetSongs(HttpClient client)
        {
            HttpResponseMessage response =
                client.GetAsync("api/songs").Result;

            if (response.IsSuccessStatusCode)
            {
                var songs = response.Content
                    .ReadAsAsync<IEnumerable<SongModel>>().Result;
                foreach (var item in songs)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}",
                        item.SongId, item.SongTitle, item.SongYear, item.SongGenre, item.Description, item.Artist.ArtistId);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void GetSong(HttpClient client)
        {
            Console.WriteLine("Enter song id");
            string id = Console.ReadLine();
            HttpResponseMessage response =
                client.GetAsync(string.Format("api/songs/{0}", id)).Result;

            if (response.IsSuccessStatusCode)
            {
                var song = response.Content
                    .ReadAsAsync<SongModel>().Result;

                Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8:dd.MM.yyyy}",
                        song.SongId, song.SongTitle, song.SongYear, song.SongGenre, song.Description,
                        song.Artist.ArtistId, song.Artist.Name, song.Artist.Country, song.Artist.DateOfBirth);
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void AddSongJson(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Enter song title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter song year");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter song genre code");
            Genre genre = (Genre)int.Parse(Console.ReadLine());
            Console.WriteLine("Enter song description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter song artist id");
            int id = int.Parse(Console.ReadLine());
            Song song = new Song { SongTitle = title, SongYear = year, SongGenre = genre, Description = description, ArtistId = id };
            HttpResponseMessage response =
                client.PostAsJsonAsync("api/songs", song).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song added");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void AddSongXml(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            Console.WriteLine("Enter song title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter song year");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter song genre code");
            Genre genre = (Genre)int.Parse(Console.ReadLine());
            Console.WriteLine("Enter song description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter song artist id");
            int id = int.Parse(Console.ReadLine());
            Song song = new Song { SongTitle = title, SongYear = year, SongGenre = genre, Description = description, ArtistId = id };
            HttpResponseMessage response =
                client.PostAsXmlAsync("api/songs", song).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song added");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void PutSongJson(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Enter song id");
            string songId = Console.ReadLine();

            Console.WriteLine("Enter song title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter song year");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter song genre code");
            Genre genre = (Genre)int.Parse(Console.ReadLine());
            Console.WriteLine("Enter song description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter song artist id");
            int id = int.Parse(Console.ReadLine());
            Song song = new Song { SongTitle = title, SongYear = year, SongGenre = genre, Description = description, ArtistId = id };
            HttpResponseMessage response =
                client.PutAsJsonAsync(string.Format("api/songs/{0}", songId), song).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song changed");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void PutSongXml(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            Console.WriteLine("Enter song id");
            string songId = Console.ReadLine();

            Console.WriteLine("Enter song title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter song year");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter song genre code");
            Genre genre = (Genre)int.Parse(Console.ReadLine());
            Console.WriteLine("Enter song description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter song artist id");
            int id = int.Parse(Console.ReadLine());
            Song song = new Song { SongTitle = title, SongYear = year, SongGenre = genre, Description = description, ArtistId = id };
            HttpResponseMessage response =
                client.PutAsXmlAsync(string.Format("api/songs/{0}", songId), song).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Song changed");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
