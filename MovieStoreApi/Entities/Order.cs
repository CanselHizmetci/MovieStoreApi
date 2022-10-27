using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreApi.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Movie> PurchasedMovies { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedDate { get; set; }
    }
}

