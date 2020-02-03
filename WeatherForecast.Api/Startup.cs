using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WeatherForecast.Api.ExternalServices;
using WeatherForecast.Api.ExternalServices.Openweathermap;
using WeatherForecast.Api.Services;
using AutoMapper;

namespace WeatherForecast.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddMediatR(typeof(Startup));

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "WeatherForecast API", Version = "0.1" });
            });

            services.AddSingleton(new OpenWeatherApiSettings(Configuration.GetValue<string>("OpenWeatherMap:ServiceUri"),
                                                            Configuration.GetValue<string>("OpenWeatherMap:AppKey"),
                                                            Configuration.GetValue<string>("OpenWeatherMap:CountryCode")));

            services.AddScoped<IWeatherService, OpenWeatherMapService>();
            services.AddScoped<IHttpClientFactory, HttpClientFactory>();
            services.AddScoped<IOpenWeathermapClient, OpenWeathermapClient>();
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options => {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder => {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecast API");
            });
        }
    }
}
