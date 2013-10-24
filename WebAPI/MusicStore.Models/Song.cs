using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SongTitle { get; set; }

        public int SongYear { get; set; }

        public Genre SongGenre { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int ArtistId { get; set; }
        public virtual Artist SongArtist { get; set; }
    }
}
