using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreApi.Entities
{
    public class Director
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DirectorName { get; set; }
        public string DirectorSurname { get; set; }
        public ICollection<Movie> Movies { get; set; }
        public int MovieId { get; set; }
    }
}

