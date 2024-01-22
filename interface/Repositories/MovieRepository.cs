using Domain.Interfaces.IRepositories;
using Domain.Interfaces.Security;
using Domain.Models;
using Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository:IMovieRepository
    {
        private readonly MovieContext _movieContext;
        private readonly IAccessUser _accessUser;

        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        public async Task<Movie> GetOrCreateMovie(Movie movieData)
        {

            var movie = await _movieContext.Movies
                                        .FirstOrDefaultAsync(m => m.MovieId == movieData.MovieId);


            if (movie != null)
            {
                return movie;
            }


            var newMovie = new Movie
            {
                MovieId = movieData.MovieId,
                Title = movieData.Title,
                ReleaseDate = movieData.ReleaseDate,
                Genre = movieData.Genre,
                Director = movieData.Director,
                Description = movieData.Description,

                Reviews = new List<MovieReview>()
            };


            _movieContext.Movies.Add(newMovie);
            await _movieContext.SaveChangesAsync();

            return newMovie;
        }
        public async Task AddWatched(Movie movie)
        {
            

            var currentUser = await _accessUser.GetUser();

            var movieToAdd = await GetOrCreateMovie(movie);
            if (!currentUser.WatchedMovies.Any(m => m.MovieId == movieToAdd.MovieId))
            {
                currentUser.WatchedMovies.Add(movieToAdd);
                await _movieContext.SaveChangesAsync();
            }


        }
        public async Task RemoveWatchedMovie(ApplicationUser user, string movieId)
        {
            var movie = user.WatchedMovies.FirstOrDefault(m => m.MovieId == movieId);
            if (movie != null)
            {
                user.WatchedMovies.Remove(movie);
                await _movieContext.SaveChangesAsync();
            }
        }
    }
}
