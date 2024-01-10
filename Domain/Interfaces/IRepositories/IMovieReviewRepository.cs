using Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
    public interface IMovieReviewRepository
    {
        Task<IEnumerable<MovieReview>> GetReviews(string movieId);
        Task<MovieReview> GetReview(int reviewId);
        Task CreateReview(MovieReview review);
        Task DeleteReview(int id);
        Task UpdateReview(MovieReview updatedReview);
        Task<IEnumerable<MovieReview>> GetUserReviews(string userId);
    }
}
