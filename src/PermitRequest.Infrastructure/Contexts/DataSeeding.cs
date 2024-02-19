using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PermitRequest.Domain.Entities;
using PermitRequest.Domain.Enums;
using PermitRequest.Domain.ValueObjets;

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
               new AdUser() { Id = Guid.Parse("e21cd525-031c-4364-b173-4150a4e18c37"), FullName = new FullName("Münir", "Özkul"), Email = "munir.ozkul@negzel.net", UserType = UserType.Manager, ManagerId = null },

               new AdUser() { Id = Guid.Parse("59fb152a-2d59-435d-8fc1-cbc35c0f1d82"), FullName = new FullName("Şener", "Şen"), Email = "sener.sen@negzel.net", UserType = UserType.WhiteCollarEmployee, ManagerId = Guid.Parse("e21cd525-031c-4364-b173-4150a4e18c37") },

              new AdUser() { Id = Guid.Parse("23591451-1cf1-46a5-907a-ee3e52abe394"), FullName = new FullName("Kemal", "Sunal"), Email = "kemal.sunal@negzel.net", UserType = UserType.BlueCollarEmployee, ManagerId = Guid.Parse("59fb152a-2d59-435d-8fc1-cbc35c0f1d82") },

              new AdUser() { Id = Guid.Parse("8b30cf18-cac1-4ced-a281-f0763dbdf78d"), FullName = new FullName("Musab", "Akan"), Email = "kemal.sunal@negzel.net", UserType = UserType.BlueCollarEmployee, ManagerId = Guid.Parse("23591451-1cf1-46a5-907a-ee3e52abe394") }

              );

                context.SaveChanges();
            }
        }
    }
}
