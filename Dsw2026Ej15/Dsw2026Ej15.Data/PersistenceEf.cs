using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly Dsw2026Ej15DbContext _context;

    public PersistenceEf(Dsw2026Ej15DbContext context)
    {
        _context = context;
    }

    public async Task<Speciality?> GetSpecialityById(Guid id)
    {
        return await _context.Specialities.SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task SaveDoctor(Doctor doctor)
    {
        await _context.Doctorts.AddAsync(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Doctor>> GetActiveDoctors()
    {
        return await _context.Doctorts
            .Include(d => d.Speciality)
            .AsNoTracking()
            .Where(d => d.IsActive)
            .ToListAsync();
    }

    public async Task<Doctor?> GetActiveDoctorById(Guid id)
    {
        return await _context.Doctorts.Include(d => d.Speciality).AsNoTracking().SingleOrDefaultAsync(d => d.Id == id && d.IsActive);
    }

    public async Task<bool> DesactivateDoctor(Guid id)
    {
        var doctor = await _context.Doctorts.SingleOrDefaultAsync(d => d.Id == id && d.IsActive);

        if (doctor is null)
            return false;

        doctor.Desactivate();

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task RemoveDoctor(Guid id)
    {
        var doctor = await _context.Doctorts.SingleOrDefaultAsync(d => d.Id == id);

        if (doctor is null)
            return;

        _context.Doctorts.Remove(doctor);

        await _context.SaveChangesAsync();
    }
}
