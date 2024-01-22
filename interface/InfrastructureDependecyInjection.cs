﻿using FluentValidation;
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
using Infrastructure.Services;
using Infrastructure.Security;
using Domain.Interfaces.Security;

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
            services.AddScoped<TokenService>();
            services.AddValidatorsFromAssembly(assembly);
            services.AddScoped<IMovieReviewRepository, MovieReviewRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IAccessUser, AccessUser>();
            return services;
        }
    }
}
