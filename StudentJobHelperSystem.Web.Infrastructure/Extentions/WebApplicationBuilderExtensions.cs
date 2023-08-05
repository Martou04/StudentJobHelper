namespace StudentJobHelperSystem.Web.Infrastructure.Extentions
{
    using Microsoft.Extensions.DependencyInjection;

    using Services.Data;
    using Services.Data.Interfaces;
    using System.Reflection;

    public static class WebApplicationBuilderExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly serviceAssembly = Assembly.GetAssembly(serviceType);

            if(serviceAssembly == null)
                throw new InvalidOperationException("Invalid service type!");
            

            Type[] servicesTypes = serviceAssembly
                .GetTypes()
                .Where(t=> t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (Type st in servicesTypes)
            {
                Type? interfaceType = st
                    .GetInterface($"I{st.Name}");

                if (interfaceType == null)
                    throw new InvalidOperationException("No interface is provided");

                services.AddScoped(interfaceType, st);
            }

            services.AddScoped<IJobAdService, JobAdService>();
        }
    }
}
