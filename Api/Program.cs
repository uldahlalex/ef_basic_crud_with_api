using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MyDbContext>(options =>
        {
            //SQLITE connection: (uncomment to use)
            //options.UseSqlite("Data Source=../DataAccess/mydatabase.db");
            
            //Postgres connection: (comment the line below to use sqlite instead)
            options.UseNpgsql(builder.Configuration.GetConnectionString("MyDbConn")); //The connection string is in the appsettings file
        });

        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument();
        
        var app = builder.Build();

 
        
        using (var scope = app.Services.CreateScope())
        {
           scope.ServiceProvider.GetRequiredService<MyDbContext>().Database.EnsureDeleted();
           scope.ServiceProvider.GetRequiredService<MyDbContext>().Database.EnsureCreated();
        }
        
        app.MapControllers();
        app.UseOpenApi();
        app.UseSwaggerUi();

        app.Run();
    }
}