﻿using Hitek.GSU.Logic.Database;
using Hitek.GSU.Logic.Interfaces;
using LightInject;
using LightInject.Mvc;
using LightInject.WebApi;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Hitek.GSU.Models;
using Hitek.GSU.Logic.Service;
using System.Web.Http;

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
        /// 
        public static IServiceContainer container;
        public static void Register(HttpConfiguration config)
        {
            container = new ServiceContainer();
            container.RegisterControllers(Assembly.GetExecutingAssembly());
             container.RegisterApiControllers();        
    //registe other services
     
            Register(container);
            RegisterRepositores(container);
            RegisterProviders(container);
            //LightInjectHttpModule.SetServiceContainer(container);
            DependencyResolver.SetResolver(new LightInjectMvcDependencyResolver(container));
            container.EnableMvc();
          //  container.EnablePerWebRequestScope();
            container.EnableWebApi(config); 
        }

        internal static void RegisterRepositores(IServiceContainer container)
        {
            container.Register<Entities>(new PerScopeLifetime());
            container.Register<Repository>(new PerScopeLifetime());

              container.Register<ITestRepository, Repository>();
/*            container.Register<IAccountRepository, Entities>();
            container.Register<IMedalRepository, Entities>();
            container.Register<IVersionControlSystemRepository, Entities>();
            container.Register<IImageRepository, Entities>();*/
        }


        internal static void RegisterProviders(IServiceContainer container)
        {
       //     container.Register<IUserStore<MyAccount,long>, MyUserStore>(); 

            container.Register<ApplicationDbContext, Entities>();
            container.Register<IUserStore<ApplicationUser, long>, UserStoreLongPk>();
            container.Register<AppSignInManager>();
            container.Register<AppUserManager>();
            container.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);
            container.Register<Logic.AuthRepository>();
            using (container.BeginScope())
            {

                container.Register<Logic.Providers.SimpleRefreshTokenProvider>();
            }
            /*
            container.Register<MedalForHero.Logic.IMembershipProvider, MedalForHero.Logic.Providers.MembershipProvider>();
            container.Register<MedalForHero.Logic.IRoleProvider, MedalForHero.Logic.Providers.RoleProvider>();
            container.Register<MedalForHero.Logic.IAuthentication, MedalForHero.Logic.Auth.Authentication>(new PerRequestLifeTime());*/
        }

        internal static void Register(IServiceContainer container)
        {
            container.Register<ITestService, TestService>();
            container.Register<ITestSubjectService, TestSubjectService>();
            container.Register<IAccountService, AccountService>();
            /*
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