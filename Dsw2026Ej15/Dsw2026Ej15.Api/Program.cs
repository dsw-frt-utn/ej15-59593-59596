using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión DefaultConnection.");

        builder.Services.AddDbContext<Dsw2026Ej15DbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IPersistence, PersistenceEf>();

        builder.Services.AddHealthChecks();

        var app = builder.Build();

        // Aplica las migraciones y carga las especialidades.
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<Dsw2026Ej15DbContext>();

            await DatabaseSeeder.Seed(context);
        }

        app.UseMiddleware<ExceptionMiddlewares>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapHealthChecks("/health-check");
        app.MapControllers();

        await app.RunAsync();
    }
}