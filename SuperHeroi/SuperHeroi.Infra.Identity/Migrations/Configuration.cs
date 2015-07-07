using System;
using System.Web.Security;
using SuperHeroi.Infra.Identity.Models;

namespace SuperHeroi.Infra.Identity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SuperHeroi.Infra.Identity.Context.ApplicationDbContext";
        }

        protected override void Seed(Context.ApplicationDbContext context)
        {
            context.Claims.AddOrUpdate(
                new Claims { Id = Guid.NewGuid(), Name = "AdminClaims" },
                new Claims { Id = Guid.NewGuid(), Name = "AdminUsers" }
            );
        }
    }
}
