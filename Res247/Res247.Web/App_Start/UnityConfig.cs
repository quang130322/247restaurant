using Res247.Data;
using Res247.Data.Infrastructure;
using Res247.Data.Infrastructure.Repositories;
using Res247.Models.Common;
using Res247.Models.Security;
using Res247.Services;
using Res247.Web.Areas.Identity.Controllers;
using Res247.Web.Controllers;
using System;

using Unity;
using Unity.Injection;

namespace Res247.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterSingleton<Res247Context, Res247Context>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<ICoreRepository<Category>, CoreRepository<Category>>();
            container.RegisterType<ICoreRepository<CovidInfo>, CoreRepository<CovidInfo>>();
            container.RegisterType<ICoreRepository<Food>, CoreRepository<Food>>();
            container.RegisterType<ICoreRepository<Order>, CoreRepository<Order>>();
            container.RegisterType<ICoreRepository<OrderDetail>, CoreRepository<OrderDetail>>();
            container.RegisterType<ICategoryService, CategoryServices>();
            container.RegisterType<IFoodServices, FoodServices>();
            container.RegisterType<IOrderServices, OrderServices>();
            container.RegisterType<ICheckoutService, CheckoutService>();
            container.RegisterType<IOrderDetailServices, OrderDetailServices>();
            container.RegisterType<ICovidInfoServices, CovidInfoServices>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<RolesAdminController>(new InjectionConstructor());
            container.RegisterType<UsersAdminController>(new InjectionConstructor());
        }
    }
}