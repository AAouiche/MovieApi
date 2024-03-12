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
        public MovieReviewDTO MovieReview { get; set; }
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
        private readonly IMapper _mapper;

        public UpdateHandler(IMovieReviewRepository movieReviewRepository, IMapper mapper)
        {
            _movieReviewRepository = movieReviewRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateCommand command, CancellationToken cancellationToken)
        {
            
            var movieReview = _mapper.Map<MovieReview>(command.MovieReview);

            
            await _movieReviewRepository.UpdateReview(movieReview);
            return Result<Unit>.SuccessResult(Unit.Value);
        }
    }
}