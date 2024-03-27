using Domain.Interfaces.IRepositories;
using Domain.Interfaces.Security;
using Domain.Models;
using Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _movieContext;
        private readonly IAccessUser _accessUser;

        public MovieRepository(MovieContext movieContext, IAccessUser accessUser)
        {
            _movieContext = movieContext;
            _accessUser = accessUser;
        }
        public async Task<Movie> GetOrCreateMovie(Movie movieData)
        {

            var movie = await _movieContext.Movies
                                        .FirstOrDefaultAsync(m => m.imdbID == movieData.imdbID);


            if (movie != null)
            {
                return movie;
            }


            var newMovie = new Movie
            {
                imdbID = movieData.imdbID,
                Title = movieData.Title,
                ReleaseDate = movieData.ReleaseDate,
                Genre = movieData.Genre,
                Director = movieData.Director,
                Plot = movieData.Plot,
                Year = movieData.Year,
                Poster = movieData.Poster,
                Runtime = movieData.Runtime,
                Actors = movieData.Actors,
                Released = movieData.Released,
                imdbRating = movieData.imdbRating,
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
            if (!currentUser.WatchedMovies.Any(m => m.imdbID == movieToAdd.imdbID))
            {
                currentUser.WatchedMovies.Add(movieToAdd);
                await _movieContext.SaveChangesAsync();
            }


        }
        public async Task RemoveWatchedMovie(string userId, string MovieId)
        {
            
            var user = await _movieContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.WatchedMovies)
                .FirstOrDefaultAsync();
            var movie = user.WatchedMovies.FirstOrDefault(m => m.imdbID == MovieId);
            if (movie != null)
            {
                user.WatchedMovies.Remove(movie);
                await _movieContext.SaveChangesAsync();
            }
        }
        public async Task<List<Movie>> ListWatchedMoviesForUser()
        {
            var userId = _accessUser.GetUserId(); 
            var userWithMovies = await _movieContext.Users 
                .Where(u => u.Id == userId)
                .Include(u => u.WatchedMovies)
                .FirstOrDefaultAsync();

            if (userWithMovies != null)
            {
                return userWithMovies.WatchedMovies.ToList();
            }

            return new List<Movie>();
        }

    }
}
