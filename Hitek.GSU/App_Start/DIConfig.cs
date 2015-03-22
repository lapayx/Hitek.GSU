using Hitek.GSU.Logic.Database;
using Hitek.GSU.Logic.Interfaces;
using LightInject;
using LightInject.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Hitek.GSU
{
    /// <summary>
    /// Конфигурация Dependency Injection.
    /// </summary>
    public class DIConfig
    {
        /// <summary>
        /// Регистрация зависимостей.
        /// </summary>
        public static void Register()
        {
            IServiceContainer container = new ServiceContainer();
            container.RegisterControllers(Assembly.GetExecutingAssembly());

            Register(container);
            RegisterRepositores(container);
            RegisterProviders(container);
            //LightInjectHttpModule.SetServiceContainer(container);
            DependencyResolver.SetResolver(new LightInjectMvcDependencyResolver(container));
            container.EnableMvc();
        }

        internal static void RegisterRepositores(IServiceContainer container)
        {
            container.Register<Entities>(new PerScopeLifetime());

            container.Register<IAccountRepository, Entities>();
/*            container.Register<IAccountRepository, Entities>();
            container.Register<IMedalRepository, Entities>();
            container.Register<IVersionControlSystemRepository, Entities>();
            container.Register<IImageRepository, Entities>();*/
        }


        internal static void RegisterProviders(IServiceContainer container)
        {/*
            container.Register<MedalForHero.Logic.IMembershipProvider, MedalForHero.Logic.Providers.MembershipProvider>();
            container.Register<MedalForHero.Logic.IRoleProvider, MedalForHero.Logic.Providers.RoleProvider>();
            container.Register<MedalForHero.Logic.IAuthentication, MedalForHero.Logic.Auth.Authentication>(new PerRequestLifeTime());*/
        }

        internal static void Register(IServiceContainer container)
        {/*
            container.Register<IImportService, ImportService>();
            container.Register<ISearchService, SearchSevice>();
            container.Register<IVcsService, VcsService>();
            container.Register<IMedalService, MedalService>();
            container.Register<IAdminService, AdminService>();
            container.Register<IAccountService, AccountService>();
            container.Register<IImageService, ImageService>();*/
        }
    }
}