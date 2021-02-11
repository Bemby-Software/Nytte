using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nytte.Events.Abstractions;
using Nytte.Sample.ModuleA;
using Nytte.Sample.ModuleA.Events.External;
using Nytte.Sample.ModuleB;

namespace Nytte.Sample.Modules.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddModuleA();
            services.AddModuleB();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var sc = app.ApplicationServices.CreateScope();
            var handler = sc.ServiceProvider.GetRequiredService<IEventHandler<SampleMessage>>();

            app.UseModuleA();
            app.UseModuleB();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}