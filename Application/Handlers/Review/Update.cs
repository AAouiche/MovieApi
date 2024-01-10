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
    public class UpdateCommand : IRequest<Result<Unit>>
    {
        public MovieReview MovieReview { get; set; }
    }

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(command => command.MovieReview).SetValidator(new MovieReviewValidator());
        }
    }

    public class UpdateHandler : IRequestHandler<UpdateCommand, Result<Unit>>
    {
        private readonly IMovieReviewRepository _movieReviewRepository;

        public UpdateHandler(IMovieReviewRepository movieReviewRepository)
        {
            _movieReviewRepository = movieReviewRepository;
        }

        public async Task<Result<Unit>> Handle(UpdateCommand command, CancellationToken cancellationToken)
        {
            await _movieReviewRepository.UpdateReview(command.MovieReview);
            return Result<Unit>.SuccessResult(Unit.Value);
        }
    }
}
