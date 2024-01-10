using Application.Utility;
using Application.Validators;
using Domain.Interfaces.IRepositories;
using Domain.Models;
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
            public MovieReview review {  get; set; }
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
            public CreateHandler(IMovieReviewRepository movieReviewRepository)
            {
               _movieReviewRepository = movieReviewRepository;
            }

            public async Task<Result<Unit>> Handle(CreateCommand command,CancellationToken cancellationToken)
            {
                var newReview = command.review;
                
                 await _movieReviewRepository.CreateReview(newReview);



                return Result<Unit>.SuccessResult(Unit.Value);
            }
        }
    }
}
