using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Services
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<UserService>();
            //TODO: add IGameStorage and IUserStorage

            return serviceDescriptors;
        }
    }
}
