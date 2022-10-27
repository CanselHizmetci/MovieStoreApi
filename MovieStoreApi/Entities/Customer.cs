using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreApi.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Movie> PurchasedMovies { get; set; }
        public int MovieId { get; set; }
        public Genre FavoriteGenres { get; set; }
        public int GenreId { get; set; }
    }
}

