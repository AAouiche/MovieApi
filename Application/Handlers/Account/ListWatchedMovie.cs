using Application.Utility;
using Domain.DTO;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.Security;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class ListWatchedMovie
    {
        public class ListWatchedMoviesQuery : IRequest<Result<List<MovieDTO>>>
        {

        }

        public class Handler : IRequestHandler<ListWatchedMoviesQuery, Result<List<MovieDTO>>>
        {
            private readonly IMovieRepository _movieRepository;
            private readonly IAccessUser _accessUser;
            public Handler(IMovieRepository movieRepository,IAccessUser accessUser)
            {
                _movieRepository = movieRepository;
                _accessUser = accessUser;
            }

            public async Task<Result<List<MovieDTO>>> Handle(ListWatchedMoviesQuery request, CancellationToken cancellationToken)
            {
                if (request == null)
                {
                    return Result<List<MovieDTO>>.Failure("Null request", ErrorCode.BadRequest);
                }

                var watchedMovies = await _movieRepository.ListWatchedMoviesForUser();

                
                var movieDtos = watchedMovies.Select(m => new MovieDTO
                {
                    imdbID = m.imdbID,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    Director = m.Director,
                    Plot = m.Plot,
                    Year = m.Year,           
                    Poster = m.Poster,       
                    Runtime = m.Runtime,      
                    Actors = m.Actors,         
                    Released = m.Released,     
                    imdbRating = m.imdbRating


                }).ToList();

                return Result<List<MovieDTO>>.SuccessResult(movieDtos);
            }
        }
    }
}
