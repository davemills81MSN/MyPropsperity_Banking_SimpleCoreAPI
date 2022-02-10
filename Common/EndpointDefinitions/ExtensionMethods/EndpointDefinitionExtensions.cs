using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Banking_SimpleCoreApi.Setup
{
    public static class EndpointDefinitionExtensions
    {
        public static void AddEndpointDefinitionsFromAssemblies(this IServiceCollection services, params Assembly[] assembliesToCheck)
        {
            var endpointDefinitions = new List<IEndpointDefinition>();
            foreach (var assembly in assembliesToCheck)
            {
                var possibleEndpoint = assembly.ExportedTypes;
                var foundEndpoints = possibleEndpoint
                    .Where(possibleEndPointClasses => typeof(IEndpointDefinition).IsAssignableFrom(possibleEndPointClasses)
                                                && !possibleEndPointClasses.IsInterface && !possibleEndPointClasses.IsAbstract);

                var createdEndpoints = foundEndpoints.Select(Activator.CreateInstance).Cast<IEndpointDefinition>();

                endpointDefinitions.AddRange(createdEndpoints);
            }

            endpointDefinitions.ForEach(x => x.DefineServices(services));

            services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
        }

        public static void UseEndpointDefinitions(this WebApplication app)
        {
            var endPointDefn = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>().ToList();
            endPointDefn.ForEach(x => x.DefineEndpoints(app));
        }
    }
}