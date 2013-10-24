using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.WebAPI.Models
{
    public class SongModel
    {
        public int SongId { get; set; }

        public string SongTitle { get; set; }

        public int SongYear { get; set; }

        public Genre SongGenre { get; set; }

        public string Description { get; set; }

        public ArtistModel Artist { get; set; }

        public static SongModel Convert(Song song)
        {
            SongModel model = new SongModel
            {
                SongId = song.SongId,
                SongTitle = song.SongTitle,
                SongYear = song.SongYear,
                SongGenre = song.SongGenre,
                Description = song.Description,
                Artist = new ArtistModel
                {
                    ArtistId = song.SongArtist.ArtistId,
                    Name = song.SongArtist.Name,
                    DateOfBirth = song.SongArtist.DateOfBirth,
                    Country = song.SongArtist.Country
                }
            };

            return model;
        }
    }
}