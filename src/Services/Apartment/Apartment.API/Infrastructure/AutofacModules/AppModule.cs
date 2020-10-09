using Apartment.API.Infrastructure.Services;
using Autofac;
using Microsoft.AspNetCore.Http;

namespace Apartment.API.Infrastructure.AutofacModules
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>()
                    .As<IHttpContextAccessor>()
                    .SingleInstance();
            
            builder.RegisterType<AzureStorage>()
                .As<IStorage>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LocalStorage>()
                    .As<IStorage>()
                    .InstancePerLifetimeScope();
            
            builder.RegisterType<PicService>()
                    .As<IPicService>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<PicServicesHandler>()
                    .As<IPicServicesHandler>()
                    .InstancePerLifetimeScope();

            builder.RegisterType<IdentityService>()
                    .As<IIdentityService>()
                    .InstancePerLifetimeScope();

        }
    }
}
