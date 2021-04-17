using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolver;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors();


            TokenOptions tokenOptions = new TokenOptions
            {
                Audience = "emek@cicek.com",
                Issuer = "emek@cicek.com",
                AccessTokenExpiration = 10,
                SecurityKey = "mysupersecretkeymysupersecretkeymysupersecretkeymysupersecretkey"
            };
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            services.AddDependencyResolvers(new ICoreModule[]{
                new CoreModule(),
            });

            //constructor iproductservice isterse ona arka planda newlenmiş product manager ver
            //IOC Containerlar bizim için instance üretimi yapıyorlar örn Autofac
            //services.AddSingleton<IProductService, ProductManager>();
            //services.AddSingleton<IProductDal, EFProductDal>();
            // Add services to the collection
            ServiceTool.Create(services);
            services.AddOptions();
            // Create a container-builder and register dependencies
            var builder = new ContainerBuilder();

            builder.Populate(services);

            builder.RegisterModule(new AutofacBusinessModule());

            AutofacContainer = builder.Build();

            return new AutofacServiceProvider(AutofacContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder=>builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

    }
}
