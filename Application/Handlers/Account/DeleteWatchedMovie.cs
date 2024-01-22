using Application.Utility;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.Security;
using Infrastructure.AppDbContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Account
{
    public class DeleteWatchedMovie
    {
        public class DeleteWatchedMovieCommand : IRequest<Result<Unit>>
        {
            public string MovieId { get; set; }
        }

        public class Handler : IRequestHandler<DeleteWatchedMovieCommand, Result<Unit>>
        {
            private readonly IMovieRepository _movieRepository;
            private readonly IAccessUser _accessUser;
            public Handler(IMovieRepository movieRepository, IAccessUser accessUser)
            {
                _movieRepository = movieRepository;
                _accessUser = accessUser;
            }

            public async Task<Result<Unit>> Handle(DeleteWatchedMovieCommand request, CancellationToken cancellationToken)
            {
                var currentUser = await _accessUser.GetUser();

                
                await _movieRepository.RemoveWatchedMovie(currentUser, request.MovieId);

                return Result<Unit>.SuccessResult(Unit.Value);
            }
        }
    }
}
