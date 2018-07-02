using System;
using System.IO;
using GraphQL.Http;
using GraphQL.Linq.Model;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Linq
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services
                .AddSingleton<IDocumentExecuter, DocumentExecuter>()
                .AddSingleton<IDocumentWriter, DocumentWriter>()
                .AddSingleton<Query>();

            services
                .AddSingleton(sp => new GraphQLSettings { BuildUserContext = ctx => new GraphQLUserContext { User = ctx.User } })
                .AddSingleton(BuildSchema);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }

        private ISchema BuildSchema(IServiceProvider sp)
        {
            var schema = Schema.For
            (
                File.ReadAllText("wwwroot/Schemas/Schema.schema"),
                builder =>
                {

                    builder.Types.Include<Query>();
                    builder.Types.Include<Products>();
                    builder.Types.Include<Product>();

                    builder.DependencyResolver = sp.GetService<IDependencyResolver>();
                }
            );

            return schema;
        }
    }
}
