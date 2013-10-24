using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.WebAPI.Models
{
    public class ArtistModel
    {
        public int ArtistId { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Country { get; set; }

        public static ArtistModel Convert(Artist artist)
        {
            ArtistModel model = new ArtistModel
            {
                ArtistId = artist.ArtistId,
                Name = artist.Name,
                DateOfBirth = artist.DateOfBirth,
                Country = artist.Country
            };

            return model;
        }
    }
}