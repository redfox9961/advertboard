using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Common;
using DataAccessLayer.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess.EntityFramework
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<DbContext>().ImplementedBy<AdvertDbContext>().LifeStyle.HybridPerWebRequestScoped());
            container.Register(Component.For<IUserStore<ApplicationUser>>().ImplementedBy<UserStore<ApplicationUser>>().LifestylePerWebRequest());
        }
    }
}
