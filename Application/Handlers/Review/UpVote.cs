using Application.Utility;
using Domain.Interfaces.IRepositories;
using Domain.Interfaces.Security;
using Domain.Models;
using Domain.Return.ReturnType;
using FluentValidation;
using Infrastructure.AppDbContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Review
{
    public class UpVote
    {
        public class UpvoteCommand : IRequest<Result<UpvoteResult>>
        {
            public int ReviewId { get; set; }
        }
        public class UpvoteCommandValidator : AbstractValidator<UpvoteCommand>
        {
            public UpvoteCommandValidator()
            {
               // RuleFor(x => x.ReviewId).GreaterThan(0).WithMessage("Review ID must be greater than 0.");
            }
        }
        public class UpvoteMovieReviewHandler : IRequestHandler<UpvoteCommand, Result<UpvoteResult>>
        {
            private readonly IMovieReviewRepository _movieReviewRepository;
            private readonly MovieContext _context;
            private readonly IAccessUser _accessUser;

            public UpvoteMovieReviewHandler(IMovieReviewRepository movieReviewRepository, MovieContext context, IAccessUser accessUser)
            {
                _movieReviewRepository = movieReviewRepository;
                _context = context;
                _accessUser = accessUser;
            }

            public async Task<Result<UpvoteResult>> Handle(UpvoteCommand command, CancellationToken cancellationToken)
            {
                var review = await _movieReviewRepository.GetReview(command.ReviewId);
                if (review == null)
                {
                    return Result<UpvoteResult>.Failure("Review not found.", ErrorCode.BadRequest);
                }

                var userId = _accessUser.GetUserId();
                var existingUpvote = await _context.Upvotes
                    .FirstOrDefaultAsync(u => u.UserId == userId && u.ReviewId == command.ReviewId, cancellationToken);

                bool upvoteAdded = false;
                if (existingUpvote != null)
                {
                    _context.Upvotes.Remove(existingUpvote);
                }
                else
                {
                    var upvote = new MovieReviewUpvote { UserId = userId, ReviewId = command.ReviewId };
                    _context.Upvotes.Add(upvote);
                    upvoteAdded = true;
                }

                await _context.SaveChangesAsync(cancellationToken);

                
                int newUpvoteCount = await _context.Upvotes.CountAsync(u => u.ReviewId == command.ReviewId, cancellationToken);

                var result = new UpvoteResult
                {
                    UpvoteCount = newUpvoteCount
                };

                return Result<UpvoteResult>.SuccessResult(result);
            }
        }
    }
}
