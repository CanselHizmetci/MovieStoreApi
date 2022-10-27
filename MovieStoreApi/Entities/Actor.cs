using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreApi.Entities
{
    public class Actor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ActorName { get; set; }
        public string ActorSurname { get; set; }
        public ICollection<Movie> Movies { get; set; }
        public int MovieId { get; set; }
    }
}

