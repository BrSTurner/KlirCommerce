﻿using Klir.TechChallenge.Web.Api.Abstraction;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Klir.TechChallenge.Web.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            var serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndpointBuilder)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpointBuilder), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {
            var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpointBuilder>>();
            IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

            foreach (IEndpointBuilder endpoint in endpoints)
            {
                endpoint.MapEndpoints(builder);
            }

            return app;
        }
    }
}
