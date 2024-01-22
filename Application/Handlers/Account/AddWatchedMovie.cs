using Application.Utility;
using Domain.Interfaces.IRepositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class AddWatchedMovie
    {
        public class AddWatchedMovieCommand : IRequest<Result<Unit>>
        {
            public Movie Movie { get; set; }
        }
        public class Handler : IRequestHandler<AddWatchedMovieCommand, Result<Unit>>
        {
            private readonly IMovieRepository _movieRepository;
            public Handler(IMovieRepository movieRepository)
            {
                _movieRepository = movieRepository;
            }
            public async Task<Result<Unit>> Handle(AddWatchedMovieCommand request,CancellationToken cancellationToken)
            {

                if(request.Movie==null)
                {
                    return Result<Unit>.Failure("Movie is null", ErrorCode.BadRequest);
                }
                await _movieRepository.AddWatched(request.Movie);

                return Result<Unit>.SuccessResult(Unit.Value);
            }
        }
    }
}
