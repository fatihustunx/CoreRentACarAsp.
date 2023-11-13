﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstracts;
using Business.BusinessRules.Abstracts;
using Business.BusinessRules.Conceretes;
using Business.Conceretes;
using Business.MapperProfiles.AutoMapper;
using Castle.DynamicProxy;
using Core.Utilities.AFiles;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstracts;
using DataAccess.Conceretes.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolver.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfCarDal>().As<ICarDal>().SingleInstance();
            builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();

            builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
            builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();

            builder.RegisterType<EfColorDal>().As<IColorDal>().SingleInstance();
            builder.RegisterType<ColorManager>().As<IColorService>().SingleInstance();

            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance();
            builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();

            builder.RegisterType<EfRentalDal>().As<IRentalDal>().SingleInstance();
            builder.RegisterType<RentalManager>().As<IRentalService>().SingleInstance();
            builder.RegisterType<RentalBusinessRules>().As<IRentalBusinessRules>().SingleInstance(); 

            builder.RegisterType<EfCarImageDal>().As<ICarImageDal>().SingleInstance();
            builder.RegisterType<CarImageManager>().As<ICarImageService>().SingleInstance();
            builder.RegisterType<CarImageBusinessRules>().As<ICarImageBusinessRules>().SingleInstance();

            builder.RegisterType<PayOfRentManager>().As<IRentPaymentService>().SingleInstance();
            builder.RegisterType<PaymentManager>().As<IPaymentService>().SingleInstance();
            builder.RegisterType<EfPaymentDal>().As<IPaymentDal>().SingleInstance();

            builder.RegisterType<FileOperations>().As<IFileOperations>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<AutoMapperProfile>();
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}