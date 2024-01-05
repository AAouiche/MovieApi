using FluentValidation;
using Infrastructure.AppDbContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces.IRepositories;
using Infrastructure.Repositories;

namespace Application
{
    public static class InfrastructureDependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(InfrastructureDependecyInjection).Assembly;

            services.AddDbContext<MovieContext>(options =>
               options.UseSqlServer(
                  configuration.GetConnectionString("MovieConnectionString"),
                  x => x.MigrationsAssembly("Infrastructure")));
            services.AddValidatorsFromAssembly(assembly);
            services.AddScoped<IMoveReviewRepository, MovieReviewRepository>();
            return services;
        }
    }
}
