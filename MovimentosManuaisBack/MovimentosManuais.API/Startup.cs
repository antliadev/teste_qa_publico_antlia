using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MovimentosManuais.Data;
using MovimentosManuais.Data.Repositories;
using MovimentosManuais.Service;

namespace MovimentosManuais.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            RegisterRepositories(services);
            RegisterServices(services);

            services.AddCors();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Antlia - Teste de Avaliação QA 19/04/2022",
                    Description = "Teste de Aviação QA",
                    Contact = new OpenApiContact
                    {
                        Name = "Antlia - Consultoria e Tecnologia",
                        Email = "suporte_portalcomunicacao@antlia.com.br",
                        Url = new Uri("http://www.antlia.com.br/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Antlia - Consultoria e Tecnologia",
                        Url = new Uri("http://www.antlia.com.br/")
                    }
                });
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Antlia - Avaliação QA");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials()
           );

            app.UseResponseCompression();

            app.UseMvc();
        }

        public void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ProdutoRepository, ProdutoRepository>();
            services.AddScoped<ProdutoCosifRepository, ProdutoCosifRepository>();
            services.AddScoped<MovimentoManualRepository, MovimentoManualRepository>();
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<MovimentoService, MovimentoService>();
        }
    }
}
