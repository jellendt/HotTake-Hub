
using HotTake_Hub_Backend.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotTake_Hub_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterServices();
            builder.Services.AddDatabase(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                ApplyDatabaseMigrations(app);
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ApplyDatabaseMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DbHotTakeContext>();
            //dbContext.Database.Migrate();
        }
    }
}
