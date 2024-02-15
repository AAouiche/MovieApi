﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class MovieDTO
    {
        public string imdbID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Plot { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
        public string Runtime { get; set; }
        public string Actors { get; set; }
        public string Released { get; set; }
        public string imdbRating { get; set; }
    }
}
