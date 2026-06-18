using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data.Interfaces
{
    public interface IPersistence
    {
        Speciality? GetSpecialityById(Guid id);
        void SaveDoctor(Doctor doctor);
    }
}
