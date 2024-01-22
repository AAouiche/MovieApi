﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace   Domain.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string? ImageUrl { get; set; }
        public string UserName { get; set; }
        public ICollection<MovieReview>? Reviews { get; set; }
        public ICollection<Movie>? WatchedMovies { get; set; }
    }
}
