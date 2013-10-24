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
    public static class AlbumCommand
    {
        public static void GetAlbums(HttpClient client)
        {
            HttpResponseMessage response =
                client.GetAsync("api/albums").Result;

            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content
                    .ReadAsAsync<IEnumerable<AlbumModel>>().Result;
                foreach (var item in albums)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5}",
                        item.AlbumId, item.AlbumTitle, item.AlbumYear, item.Producer, item.SongsCount, item.ArtistsCount);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void GetAlbum(HttpClient client)
        {
            Console.WriteLine("Enter album id");
            string id = Console.ReadLine();
            HttpResponseMessage response =
                client.GetAsync(string.Format("api/albums/{0}", id)).Result;

            if (response.IsSuccessStatusCode)
            {
                var album = response.Content
                    .ReadAsAsync<AlbumFullModel>().Result;

                Console.WriteLine("{0} {1} {2} {3} {4} {5}",
                    album.AlbumId, album.AlbumTitle, album.AlbumYear, album.Producer, album.ArtistsCount, album.SongsCount);

                foreach (var song in album.Songs)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8:dd.MM.yyyy}",
                        song.SongId, song.SongTitle, song.SongYear, song.SongGenre, song.Description,
                        song.Artist.ArtistId, song.Artist.Name, song.Artist.Country, song.Artist.DateOfBirth);
                }

                foreach (var artist in album.Artists)
                {
                    Console.WriteLine("{0} {1} {2} {3:dd.MM.yyyy}",
                        artist.ArtistId, artist.Name, artist.Country, artist.DateOfBirth);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void AddAlbumJson(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Enter album title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter album year");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter producer");
            string producer = Console.ReadLine();
            Album album = new Album { AlbumTitle = title, AlbumYear = year, Producer = producer };
            HttpResponseMessage response =
                client.PostAsJsonAsync("api/albums", album).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album added");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void AddAlbumXml(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            Console.WriteLine("Enter album title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter album year");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter producer");
            string producer = Console.ReadLine();
            Album album = new Album { AlbumTitle = title, AlbumYear = year, Producer = producer };
            HttpResponseMessage response =
                client.PostAsXmlAsync("api/albums", album).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album added");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void PutAlbumJson(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Enter album id");
            string albumId = Console.ReadLine();

            Console.WriteLine("Enter album title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter album year");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter producer");
            string producer = Console.ReadLine();
            Album album = new Album { AlbumTitle = title, AlbumYear = year, Producer = producer };
            HttpResponseMessage response =
                client.PutAsJsonAsync(string.Format("api/albums/{0}", albumId), album).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album changed");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public static void PutAlbumXml(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/xml"));

            Console.WriteLine("Enter album id");
            string albumId = Console.ReadLine();

            Console.WriteLine("Enter album title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter album year");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter producer");
            string producer = Console.ReadLine();
            Album album = new Album { AlbumTitle = title, AlbumYear = year, Producer = producer };
            HttpResponseMessage response =
                client.PutAsXmlAsync(string.Format("api/songs/{0}", albumId), album).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album changed");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
