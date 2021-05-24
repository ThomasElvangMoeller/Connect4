using Connect4.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Services
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<UserService>();
            service.AddScoped<IUserStorage, UserStorage>(); //The dbContext inside the UserStorage is already singleton, so UserStorage doesn't need to be singleton as it just transforms the data
            service.AddScoped<IGameStorage, GameStorage>();
            //TODO: add IGameStorage and IUserStorage

            return service;
        }
    }
}
