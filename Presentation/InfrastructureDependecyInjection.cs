using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class PresentationDependecyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            var assembly = typeof(PresentationDependecyInjection).Assembly;
           

            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
