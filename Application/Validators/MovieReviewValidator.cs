using Domain.Models;
using Domain.Return.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class MovieReviewValidator : AbstractValidator<MovieReviewDTO>
    {
        public MovieReviewValidator()
        {
            
            

            
            RuleFor(x => x.imdbID).NotEmpty().WithMessage("Movie ID is required.");

            
            RuleFor(x => x.Content).NotEmpty().WithMessage("Review content is required.")
                                   .MaximumLength(1000).WithMessage("Review content must be less than 1000 characters.");


            RuleFor(x => x.Rating).InclusiveBetween(1.0, 10.0).WithMessage("Rating must be between 1 and 10.");


            

            
        }
    }
}
