using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data.Interfaces
{
    public interface IPersistence
    {
        //Metodos para especialidades
        List<Specialty> GetSpecialties();
        Specialty? GetSpecialty(Guid id);
        void AddSpecialty(Specialty specialty);
        void UpdateSpecialty(Specialty specialty);
        void DeleteSpecialty(Guid id);

        //Metodos para doctores
        List<Doctor> GetDoctors();
        Doctor? GetDoctor(Guid id);
        void AddDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(Guid id);
    }
}
