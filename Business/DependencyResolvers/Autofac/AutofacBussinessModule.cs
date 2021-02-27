using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBussinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
            builder.RegisterType<EfBrandDAL>().As<IBrandDAL>().SingleInstance();

            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
            builder.RegisterType<EfCarDAL>().As<ICarDAL>().SingleInstance();

            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();
            builder.RegisterType<EfColorDAL>().As<IColorDAL>().SingleInstance();

            builder.RegisterType<CustomersManager>().As<ICustomersService>().SingleInstance();
            builder.RegisterType<EfCustomersDAL>().As<ICustomersDAL>().SingleInstance();

            builder.RegisterType<RentalsManager>().As<IRentalsServices>().SingleInstance();
            builder.RegisterType<EfRentalsDAL>().As<IRentalsDAL>().SingleInstance();

            builder.RegisterType<UsersManager>().As<IUsersService>().SingleInstance();
            builder.RegisterType<EfUsersDAL>().As<IUsersDAL>().SingleInstance();

            builder.RegisterType<CarImageManager>().As<ICarImageService>().SingleInstance();
            builder.RegisterType<EfCarImagesDAL>().As<ICarImagesDAL>().SingleInstance();




            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
