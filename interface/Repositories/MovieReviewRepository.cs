using Domain.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieReviewRepository:IMovieReviewRepository
    {
        private readonly MovieContext _movieContext;

        public MovieReviewRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        public async Task<IEnumerable<MovieReview>> GetUserReviews(string userId)
        {
            
                return await _movieContext
                    .movieReviews
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
     
        }
        public async Task<IEnumerable<MovieReview>> GetReviews(string MovieId)
        {

            return await _movieContext
                .movieReviews
                .Where(x => x.imdbId == MovieId)
                .ToListAsync();

        }
        public async Task<MovieReview> GetReview(int reviewId)
        {
            return await _movieContext.movieReviews.FindAsync(reviewId);
        }

        public async Task CreateReview(MovieReview review)
        {
            _movieContext.movieReviews.Add(review);
            await _movieContext.SaveChangesAsync();
        }

        public async Task DeleteReview(int reviewId)
        {
            var review = await _movieContext.movieReviews.FindAsync(reviewId);
            if (review != null)
            {
                _movieContext.movieReviews.Remove(review);
                await _movieContext.SaveChangesAsync();
            }
        }

        public async Task UpdateReview(MovieReview review)
        {
            _movieContext.movieReviews.Update(review);
            await _movieContext.SaveChangesAsync();
        }

        

    }
}
