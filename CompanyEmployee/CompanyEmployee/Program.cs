
using CompanyEmployee.Extensions;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
            
                builder.Services.AddDbContext<ApplicationDBContext>(opttions => opttions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection"))

                );
                builder.Services.AddAutoMapper(typeof(Program));
                builder.Services.ConfigureRepositoryManager();
               

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            } catch (Exception ex)
            {

            }
        }
    }
}