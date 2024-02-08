using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;

namespace PermitRequest.Infrastructure.Contexts
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetService<PermitRequestContext>();
            context.Database.Migrate();

            if (context.AdUsers.Count() == 0)
            {
                context.AddRange(
               new AdUser() { Id = Guid.Parse("e21cd525-031c-4364-b173-4150a4e18c37"), FirstName = "Münir", LastName = "Özkul", Email = "munir.ozkul@negzel.net", UserType = UserType.Manager, ManagerId = null },


               new AdUser() { Id = Guid.Parse("59fb152a-2d59-435d-8fc1-cbc35c0f1d82"), FirstName = "Şener", LastName = "Şen", Email = "sener.sen@negzel.net", UserType = UserType.WhiteCollarEmployee, ManagerId = Guid.Parse("e21cd525-031c-4364-b173-4150a4e18c37") },


              new AdUser() { Id = Guid.Parse("23591451-1cf1-46a5-907a-ee3e52abe394"), FirstName = "Kemal", LastName = "Sunal", Email = "kemal.sunal@negzel.net", UserType = UserType.BlueCollarEmployee, ManagerId = Guid.Parse("59fb152a-2d59-435d-8fc1-cbc35c0f1d82") });

                context.SaveChanges();
            }
        }
    }
}
