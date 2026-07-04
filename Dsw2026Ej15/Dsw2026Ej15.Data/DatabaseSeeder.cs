using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public static class DatabaseSeeder
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();

            var hasSpecialities = await context.Specialities.AnyAsync();

            if (hasSpecialities)
                return;

            string jsonPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Sources",
                "specialities.json"
            );

            if (!File.Exists(jsonPath))
                return;

            var json = await File.ReadAllTextAsync(jsonPath);

            var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [];

            var entities = specialities
                .Select(s => new Speciality(s.Name, s.Description, s.Id))
                .ToList();

            await context.Specialities.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
    }
}