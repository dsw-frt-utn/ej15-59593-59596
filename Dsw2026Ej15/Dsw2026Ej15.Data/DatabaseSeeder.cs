using System.Text.Json;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public static class DatabaseSeeder
{
    public static async Task Seed(Dsw2026Ej15DbContext context)
    {

        await context.Database.MigrateAsync();

        if (await context.Specialities.AnyAsync())
            return;

        var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");

        if (!File.Exists(jsonPath))
            throw new FileNotFoundException("No se encontró el archivo de especialidades.", jsonPath);

        var json = await File.ReadAllTextAsync(jsonPath);

        var specialityDtos = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];

        var specialities = specialityDtos.Select(s => new Speciality(s.Name, s.Description, s.Id)).ToList();

        await context.Specialities.AddRangeAsync(specialities);
        await context.SaveChangesAsync();
    }
}
