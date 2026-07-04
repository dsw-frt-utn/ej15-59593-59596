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
    public async Task<bool> DesactivateDoctor(Guid id)
    {
        var doctor = _context.Doctorts.FirstOrDefault(d => d.Id == id && d.IsActive);
        if (doctor is null)
            return false;
        doctor.Desactivate();
        return true;
    }

    public async Task<Doctor?> GetActiveDoctorById(Guid id)
    {
        return await _context.Doctorts.FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
    }

    public async Task<IEnumerable<Doctor>> GetActiveDoctors()
    {
        return _context.Doctorts.Where(d => d.IsActive);
    }

    public async Task<Speciality?> GetSpecialityById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveDoctor(Doctor doctor)
    {
        _context.Add(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveDoctor(Guid id)
    {
        var doctor = _context.Doctorts.FirstOrDefaultAsync(d => d.Id == id);
        if (doctor is not null)
        {
            _context.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
