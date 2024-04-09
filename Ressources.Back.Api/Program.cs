using Ressources.Back.Data.Repositories;
using Ressources.Back.Data.Repositories.Sql;
using Microsoft.EntityFrameworkCore;
namespace Ressources.Back.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DataContext>(options =>
            {
                // Remplacez ces informations par vos propres informations de connexion MySQL
                options.UseMySql("server=mysql-ibanmarlats.alwaysdata.net;user=350475_iban;password=Mdp123*;database=ibanmarlats_ressources_relationnelles;",
                    new MySqlServerVersion(new Version(8, 0, 23)));
            });
            // Add services to the container.
            builder.Services.AddScoped<ITypeUserRepository, SqlTypeUserRepository>();
            builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
            builder.Services.AddScoped<ICategoryRepository, SqlCategoryRepository>();
            builder.Services.AddScoped<IRessourceRepository, SqlRessourceRepository>();
            builder.Services.AddScoped<IVuRepository, SqlVuRepository>();
            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseCors("AllowOrigin");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
