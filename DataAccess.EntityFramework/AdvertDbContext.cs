using System.Data.Entity;
using DataAccessLayer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess.EntityFramework
{
    public class AdvertDbContext : IdentityDbContext<ApplicationUser>
    {
        public AdvertDbContext() : base("advert_db")
        {

        }

        public DbSet<Advert> Adverts { get; set; }

        public DbSet<City> Cities { get; set; }

        public static AdvertDbContext Create()
        {
            return new AdvertDbContext();
        }
    }
}
