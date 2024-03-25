using Application.Utility;
using Application.Validators;
using AutoMapper;
using Domain.Interfaces.IRepositories;
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
    public class UpdateCommand : IRequest<Result<Unit>>
    {
        public ReviewUpdateDTO MovieReview { get; set; }
    }

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(command => command.MovieReview).SetValidator(new ReviewUpdateDTOValidator());
        }
    }

    public class UpdateHandler : IRequestHandler<UpdateCommand, Result<Unit>>
    {
        private readonly IMovieReviewRepository _movieReviewRepository;
        private readonly IMapper _mapper;

        public UpdateHandler(IMovieReviewRepository movieReviewRepository, IMapper mapper)
        {
            _movieReviewRepository = movieReviewRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateCommand command, CancellationToken cancellationToken)
        {

            var movieReview = await _movieReviewRepository.GetReview(command.MovieReview.ReviewId);

            if (movieReview == null)
            {
                return Result<Unit>.Failure("Review not found",ErrorCode.BadRequest);
            }

            movieReview.Content = command.MovieReview.Content;
            movieReview.Rating = command.MovieReview.Rating;
            await _movieReviewRepository.UpdateReview(movieReview);
            return Result<Unit>.SuccessResult(Unit.Value);
        }
    }
}