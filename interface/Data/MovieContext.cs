using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.AppDbContext
{
    public class MovieContext : IdentityDbContext
    {

        public DbSet<MovieReview> movieReviews { get; set; }
       
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieReview>()
                .HasKey(mr => mr.ReviewId);

            modelBuilder.Entity<MovieReview>()
              .HasOne(r => r.User)
              .WithMany(u => u.Reviews)
              .HasForeignKey(r => r.UserId);


        }
    }
}
