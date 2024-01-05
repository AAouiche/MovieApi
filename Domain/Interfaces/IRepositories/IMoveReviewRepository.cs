using Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IRepositories
{
    public interface IMoveReviewRepository
    {
        Task<List<MovieReview>> GetReviews(string userId);
        Task<MovieReview> GetReview(int reviewId);
        Task CreateReview(MovieReview review);
        Task DeleteReview(int id);
        Task UpdateReview(MovieReview updatedReview);
    }
}
