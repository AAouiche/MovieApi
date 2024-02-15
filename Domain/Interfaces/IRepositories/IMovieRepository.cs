using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
    public interface IMovieRepository
    {

        Task<Movie> GetOrCreateMovie(Movie movieData);
        Task AddWatched(Movie movie);
        Task RemoveWatchedMovie(ApplicationUser user, string MovieId);
        Task<List<Movie>> ListWatchedMoviesForUser();


    }
}
