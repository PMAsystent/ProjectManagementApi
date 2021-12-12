using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Core.Base.Behaviours;
using System.Reflection;
using MediatR.Behaviors.Authorization.Extensions.DependencyInjection;
using ProjectManagement.Core.Base.Utils;

namespace ProjectManagement.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddMediatorAuthorization(Assembly.GetExecutingAssembly());
            // Register all `IAuthorizer` implementations for a given assembly
            services.AddAuthorizersFromAssembly(Assembly.GetExecutingAssembly());
            
            services.AddTransient<IProgressPercentageService, ProgressPercentageService>();

            return services;
        }
    }
}
