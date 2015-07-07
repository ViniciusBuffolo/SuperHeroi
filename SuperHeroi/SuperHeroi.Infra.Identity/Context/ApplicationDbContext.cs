using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SuperHeroi.Infra.Identity.Models;

namespace SuperHeroi.Infra.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SuperHeroiIdentity", throwIfV1Schema: false)
        {
        }

        public DbSet<Client> Client { get; set; }

        public DbSet<Claims> Claims { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}