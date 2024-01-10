using Application.Utility;
using Domain.Interfaces.IRepositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Review
{
    public class DeleteCommand : IRequest<Result<Unit>>
    {
        public int ReviewId { get; set; }
    }

    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(x => x.ReviewId).GreaterThan(0).WithMessage("Review ID must be greater than 0.");
        }
    }
    public class DeleteMovieReviewHandler : IRequestHandler<DeleteCommand, Result<Unit>>
    {
        private readonly IMovieReviewRepository _movieReviewRepository;

        public DeleteMovieReviewHandler(IMovieReviewRepository movieReviewRepository)
        {
            _movieReviewRepository = movieReviewRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            await _movieReviewRepository.DeleteReview(command.ReviewId);
            return Result<Unit>.SuccessResult(Unit.Value);
        }
    }
}
