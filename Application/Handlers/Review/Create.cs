using Application.Utility;
using Application.Validators;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.Security;
using Domain.Models;
using Domain.Return.DTO;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Review
{
    public class Create
    {

        public class CreateCommand : IRequest<Result<Unit>>
        {
            public MovieReviewDTO review {  get; set; }
           
        }
        public class CreateCommandValidator : AbstractValidator<CreateCommand>
        {
            public CreateCommandValidator()
            {
                
                RuleFor(command => command.review)
                    .SetValidator(new MovieReviewValidator());
            }
        }
        public class CreateHandler : IRequestHandler<CreateCommand, Result<Unit>>
        {
            private readonly IMovieReviewRepository _movieReviewRepository;
            private readonly IMovieRepository _movieRepository;
            private readonly IAccessUser _accessUser;

            public CreateHandler(IMovieReviewRepository movieReviewRepository, IMovieRepository movieRepository,IAccessUser accessUser)
            {
                _movieReviewRepository = movieReviewRepository;
                _movieRepository = movieRepository;
                _accessUser = accessUser;
            }

            public async Task<Result<Unit>> Handle(CreateCommand command, CancellationToken cancellationToken)
            {

                //var movie = await _movieRepository.GetOrCreateMovie(command.movie);

                var userId = _accessUser.GetUserId();
                var newReview = new MovieReview
                {

                    UserId = userId, 
                    imdbID = command.review.imdbID, 
                    Content = command.review.Content,
                    Rating = command.review.Rating,
                    ReviewDate = DateTime.UtcNow,
                };

                
                await _movieReviewRepository.CreateReview(newReview);

                return Result<Unit>.SuccessResult(Unit.Value);
            }
        }
    }
}
