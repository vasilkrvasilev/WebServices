using MusicStore.Data;
using MusicStore.Models;
using MusicStore.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.ConsoleClient
{
    class MusicStoreConsole
    {
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("http://localhost:53152/") };

        static void Main()
        {
            //Create and initialize the database

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion
            //    <MusicStoreContext, MusicStore.Data.Migrations.Configuration>());

            //var context = new MusicStoreContext();
            //using (context)
            //{
            //    var artist = new Artist { Name = "Artist", Country = "USA", DateOfBirth = DateTime.Now };
            //    var song = new Song { SongTitle = "Song", SongGenre = Genre.Country, Description = "...", SongYear = 2000, SongArtist = artist };
            //    var album = new Album { AlbumTitle = "Album", Producer = "P", AlbumYear = 2001, Artists = new Artist[] { artist }, Songs = new Song[] { song } };
            //    context.Artists.Add(artist);
            //    context.Songs.Add(song);
            //    context.Albums.Add(album);
            //    context.SaveChanges();
            //}

            Console.WriteLine("Enter command");
            string command = Console.ReadLine();
            if (command == "Get artists")
            {
                ArtistCommand.GetArtists(Client);
            }
            else if (command == "Get artist")
            {
                ArtistCommand.GetArtist(Client);
            }
            else if (command == "Add artist - json")
            {
                ArtistCommand.AddArtistJson(Client);
            }
            else if (command == "Add artist - xml")
            {
                ArtistCommand.AddArtistXml(Client);
            }
            else if (command == "Put artist - json")
            {
                ArtistCommand.PutArtistJson(Client);
            }
            else if (command == "Put artist - xml")
            {
                ArtistCommand.PutArtistXml(Client);
            }
            else if (command == "Delete artist")
            {
                Delete("artists");
            }
            else if (command == "Get songs")
            {
                SongCommand.GetSongs(Client);
            }
            else if (command == "Get song")
            {
                SongCommand.GetSong(Client);
            }
            else if (command == "Add song - json")
            {
                SongCommand.AddSongJson(Client);
            }
            else if (command == "Add song - xml")
            {
                SongCommand.AddSongXml(Client);
            }
            else if (command == "Put song - json")
            {
                SongCommand.PutSongJson(Client);
            }
            else if (command == "Put song - xml")
            {
                SongCommand.PutSongXml(Client);
            }
            else if (command == "Delete song")
            {
                Delete("songs");
            }
            else if (command == "Get albums")
            {
                AlbumCommand.GetAlbums(Client);
            }
            else if (command == "Get album")
            {
                AlbumCommand.GetAlbum(Client);
            }
            else if (command == "Add album - json")
            {
                AlbumCommand.AddAlbumJson(Client);
            }
            else if (command == "Add album - xml")
            {
                AlbumCommand.AddAlbumXml(Client);
            }
            else if (command == "Put album - json")
            {
                AlbumCommand.PutAlbumJson(Client);
            }
            else if (command == "Put album - xml")
            {
                AlbumCommand.PutAlbumXml(Client);
            }
            else if (command == "Delete album")
            {
                Delete("albums");
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        }

        static void Delete(string controller)
        {
            Console.WriteLine("Enter id");
            string id = Console.ReadLine();
            HttpResponseMessage response =
                Client.DeleteAsync(string.Format("api/{0}/{1}", controller, id)).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Element deleted");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
