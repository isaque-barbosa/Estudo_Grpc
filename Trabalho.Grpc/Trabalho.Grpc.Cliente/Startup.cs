using Microsoft.OpenApi.Models;

namespace Trabalho.Grpc.Cliente
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cliente Grpc",
                    Description = "Cliente Api responsável por se comunicar com um serviço Grpc. Trabalho da matéria de Sistemas Distribuidos da Unileste. Professor Thales",
                    Contact = new OpenApiContact() { Name = "Isaque de Oliveira, Cinthia Lima Carvalho, Tulio Augusto, Denilson Vieira, Pedro", Email = "isaque.barbosa@a.unileste.edu.br" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            app.UseCors(cors =>
                cors.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
