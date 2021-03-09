using GraphQL;
using GraphQL.Types;
using GraphQL.NewtonsoftJson;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace graphql_training_2
{
    public class Startup
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            }
        );

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<ISchema, GameStoreSchema>();
            services.AddTransient<GameStoreQuery>();
            services.AddTransient<GameStoreMutation>();
            services.AddTransient<ItemInputType>();
            services.AddTransient<ItemType>();
            services.AddTransient<CustomerType>();
            services.AddTransient<CustomerInputType>();
            services.AddTransient<OrderType>();
            services.AddTransient<OrderInputType>();
            services.AddTransient<OrderItemType>();
            services.AddTransient<OrderItemInputType>();

            services.AddGraphQL(options =>
            {
                options.EndPoint = "/graphql";
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddTransient<IRepository, Repository>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseLoggerFactory(loggerFactory)
                    .UseInMemoryDatabase("InMemoryDb")
            );
            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseLoggerFactory(loggerFactory)
            //         .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GameStoreDb;Trusted_Connection=True;"),
            //     ServiceLifetime.Transient
            // );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseGraphQL();
        }
    }
}
