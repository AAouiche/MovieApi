using Application.Utility;
using Domain.Interfaces.IRepositories;
using Domain.Models;
using Domain.Return.DTO;
using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Review
{
    public class List
    {

        public class ListQuery : IRequest<Result<IEnumerable<MovieReviewDTO>>>
        {
            public string movieReviewId {  get; set; }

        } 

            public class ListHandler:IRequestHandler<ListQuery, Result<IEnumerable<MovieReviewDTO>>>
            {
                private readonly IMovieReviewRepository _movieReviewRepository;
                public ListHandler(IMovieReviewRepository movieReviewRepository)
                {
                    _movieReviewRepository = movieReviewRepository;
                }
                public async Task<Result<IEnumerable<MovieReviewDTO>>> Handle(ListQuery query,CancellationToken cancellationToken)
                {
                var reviews = await _movieReviewRepository.GetReviews(query.movieReviewId);
                return Result<IEnumerable<MovieReviewDTO>>.SuccessResult(reviews);
                }
            }

        
    }
}
