using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreApi.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public DateTime MovieYear { get; set; }
        public Genre MovieGenre { get; set; }
        public int GenreId { get; set; }
        public Director Director { get; set; }
        public int DirectorId { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public int ActorId { get; set; }
        public decimal Price { get; set; }
    }
}

