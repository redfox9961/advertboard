using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AdvertBoardUI.Common;
using BusinessLogicLayer.Mappings;
using IoC;
using AdvertBoardUI.ControllerFactory;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace AdvertBoardUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorWebApiControllerActivator(WindsorWrapper.Container));
            //Инициализация IOC с прокидыванием туда инсталлера контроллера. Этот IOC собирает в себе все сущности приложения
            WindsorWrapper.Init(new ControllerInstaller(Assembly.GetExecutingAssembly()));

            //Чтобы сделать IOC доступным в любой точке приложения, передаем его общедоступной сущности CommonContainer
            CommonContainer.CommonContainer.Initialize(WindsorWrapper.Container);

            MapperConfigurator.Configure();

            //Создание фабрики Windsor, позволяющей делать внедрение зависимостей через конструктор
            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(WindsorWrapper.Container.Kernel);
            //замена дефолтной фабрики контроллеров на фабрику Windsor
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);                       
        }
    }
}
