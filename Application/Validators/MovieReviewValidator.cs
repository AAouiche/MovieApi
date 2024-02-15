using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class MovieReviewValidator : AbstractValidator<MovieReview>
    {
        public MovieReviewValidator()
        {
            
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");

            
            RuleFor(x => x.imdbId).NotEmpty().WithMessage("Movie ID is required.");

            
            RuleFor(x => x.Content).NotEmpty().WithMessage("Review content is required.")
                                   .MaximumLength(1000).WithMessage("Review content must be less than 1000 characters.");

            
            RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            
            RuleFor(x => x.ReviewDate).LessThanOrEqualTo(DateTime.Now).WithMessage("Review date cannot be in the future.");

            
        }
    }
}
