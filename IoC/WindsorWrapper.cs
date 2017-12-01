using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common;
using DataAccess.Abstraction;
using DataAccess.EntityFramework;

namespace IoC
{
    public static class WindsorWrapper
    {
        public static WindsorContainer Container = new WindsorContainer();
        public static void Init(IWindsorInstaller installer = null)
        {
            Container.Register(Component.For<IAdvertService>().ImplementedBy<AdvertService>().LifeStyle.HybridPerWebRequestScoped());
            Container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(RepositoryBase<>)).LifeStyle.HybridPerWebRequestScoped());
            Container.Install(new EntityFrameworkInstaller());
            Container.Register(Component.For(typeof(IApplicationUserService)).ImplementedBy<ApplicationUserService>().LifestylePerWebRequest());
            Container.Register(Component.For(typeof(IDataSession)).ImplementedBy<EntityDataSession>().LifeStyle.HybridPerWebRequestScoped());            
            if (installer != null)
            {
                Container.Install(installer);
            }
        }
    }
}
