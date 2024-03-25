using Domain.Return.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class ReviewUpdateDTOValidator : AbstractValidator<ReviewUpdateDTO>
    {
        public ReviewUpdateDTOValidator()
        {
            RuleFor(x => x.ReviewId)
                .GreaterThan(0)
                .WithMessage("Review ID must be greater than zero.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Review content cannot be empty.")
                .MaximumLength(500)
                .WithMessage("Review content must not exceed 500 characters.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 10)
                .WithMessage("Rating must be between 0 and 10.");
        }
    }
}
