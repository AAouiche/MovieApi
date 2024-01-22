using Application.Utility;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.Security;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class ListWatchedMovie
    {
        public class ListWatchedMoviesQuery : IRequest<Result<List<Movie>>>
        {

        }

        public class Handler : IRequestHandler<ListWatchedMoviesQuery, Result<List<Movie>>>
        {
            private readonly IMovieRepository _movieRepository;
            private readonly IAccessUser _accessUser;
            public Handler(IMovieRepository movieRepository,IAccessUser accessUser)
            {
                _movieRepository = movieRepository;
                _accessUser = accessUser;
            }

            public async Task<Result<List<Movie>>> Handle(ListWatchedMoviesQuery request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    return Result<List<Movie>>.Failure("Null request", ErrorCode.BadRequest);
                }
                var currentUser = await _accessUser.GetUser();
                var watchedMovie = currentUser.WatchedMovies.Select(m => new Movie
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,        
                    Director = m.Director,     
                    Description = m.Description 
                                                
                }).ToList();

                return Result<List<Movie>>.SuccessResult(watchedMovie);
            }
        }
    }
}
