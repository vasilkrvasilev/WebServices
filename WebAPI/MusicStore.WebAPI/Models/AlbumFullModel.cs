using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.WebAPI.Models
{
    public class AlbumFullModel : AlbumModel
    {
        public IEnumerable<ArtistModel> Artists { get; set; }

        public IEnumerable<SongModel> Songs { get; set; }

        public static new AlbumFullModel Convert(Album album)
        {
            AlbumFullModel model = new AlbumFullModel
            {
                AlbumId = album.AlbumId,
                AlbumTitle = album.AlbumTitle,
                AlbumYear = album.AlbumYear,
                Producer = album.Producer,
                ArtistsCount = album.Artists.Count,
                SongsCount = album.Songs.Count,
                Songs = (
                from song in album.Songs
                select SongModel.Convert(song)).ToList(),
                Artists = (
                from artist in album.Artists
                select ArtistModel.Convert(artist)).ToList()
            };

            return model;
        }
    }
}