using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.WebAPI.Models
{
    public class AlbumModel
    {
        public int AlbumId { get; set; }

        public string AlbumTitle { get; set; }

        public int AlbumYear { get; set; }

        public string Producer { get; set; }

        public int ArtistsCount { get; set; }

        public int SongsCount { get; set; }

        public static AlbumModel Convert(Album album)
        {
            AlbumModel model = new AlbumModel
            {
                AlbumId = album.AlbumId,
                AlbumTitle = album.AlbumTitle,
                AlbumYear = album.AlbumYear,
                Producer = album.Producer,
                ArtistsCount = album.Artists.Count,
                SongsCount = album.Songs.Count
            };

            return model;
        }
    }
}