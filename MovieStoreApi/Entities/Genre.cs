﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreApi.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
}

