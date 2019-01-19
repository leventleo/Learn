using LearnCode.Bussiness.Concreats;
using LearnCode.Bussiness.Interfaces;
using LearnCode.Bussiness.SignalIR;
using LearnCode.Entities;
using LLearnCode.Bussiness.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Caching;
using ServiceStack.Redis;
using System;

namespace LearnCode.MvcUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {

                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
           
            
            services.AddDbContext<LessonDbContext>(ServiceLifetime.Transient);

            services.AddSingleton<ILesson, LessonDal>();
            services.AddSingleton<IContent, ContentDal>();
            services.AddSingleton<ISubject, SubjectDal>();
            services.AddSingleton<IViewLevel, ViewLevelDal>();
            services.AddSingleton<ISourceFile, SourceFileDal>();
            services.AddSingleton<ICommandIndex, CommandIndexDal>();
            services.AddSingleton<IFileStore, FileStoreDal>();
            services.AddSingleton<IRedisClient, RedisClient>();
            services.AddResponseCaching();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1:6379";
                option.InstanceName = "master";
            });
            services.AddSignalR();
            services.AddMvc();

            //services.AddCors(options => options.AddPolicy("CorsPolicy",
            //builder =>
            //{
            //    builder.AllowAnyMethod().AllowAnyHeader()
            //           .WithOrigins("http://localhost:61258/")
            //           .AllowCredentials();
            //}));

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCaching();

            app.Use(async (context, next) =>
            {
                // For GetTypedHeaders, add: using Microsoft.AspNetCore.Http;
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };

                await next();
            });


            app.Use(async (context, next) =>
            {
               

               

                await next();
            });



            app.UseStaticFiles();
            app.UseCookiePolicy();
           
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=lesson}/{action=list}/{id?}");
            });
            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<UpdateHub>("/updatehub");
            });

            ///signalR Setting
           



        }


    }
}
