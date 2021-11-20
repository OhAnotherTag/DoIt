using Application.Common.Interfaces;
using Application.TodoItems;
using Application.TodoLists;
using Domain.Validators;
using FluentValidation.AspNetCore;
using Infrastructure.Database;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SharedKernel.Interfaces;

namespace WebAPI
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
            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TodoListValidator>());
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DoIt", Version = "v1"});
                c.EnableAnnotations();
            });
            
            services.AddSingleton(new DatabaseConfig(Configuration["DatabaseName"]));
            services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
            
            services.AddScoped<ITodoItemRepository, SqliteTodoItemRepository>();
            services.AddScoped<ITodoListRepository, SqliteTodoListRepository>();
            
            services.AddScoped<ITodoItemService, TodoItemService>();
            services.AddScoped<ITodoListService, TodoListService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoIt v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.ApplicationServices.GetService<IDatabaseBootstrap>()?.Setup();
        }
    }
}