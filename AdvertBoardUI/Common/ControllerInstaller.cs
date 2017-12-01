using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http.Controllers;

namespace AdvertBoardUI.Common
{
    public class ControllerInstaller : IWindsorInstaller
    {
        //сборка откуда брать контроллеры
        private readonly Assembly _assembly;

        public ControllerInstaller(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Зарегистрировать все контроллеры из текущей сборки, которые наследуют Controller
            container.Register(Classes.FromAssembly(_assembly).BasedOn<Controller>().LifestylePerWebRequest());

            // Зарегистрировать все контроллеры из текущей сборки, которые наследуют IHttpController
            container.Register(Classes.FromAssembly(_assembly).BasedOn<IHttpController>().LifestylePerWebRequest());
        }
    }
}