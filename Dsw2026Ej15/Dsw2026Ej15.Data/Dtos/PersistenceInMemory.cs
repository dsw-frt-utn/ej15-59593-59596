using Dsw2026Ej15.Data.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Dtos
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Specialty> _specialties = [];
        private readonly List<Doctor> _doctors = [];

        public PersistenceInMemory()
        {
            _specialties = LoadSpecialties();
            _doctors = new List<Doctor>();
        }

        private List<Specialty> LoadSpecialties()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "specialties.json");

            string json = File.ReadAllText(filePath);

            List<Specialty>? specialties = JsonSerializer.Deserialize<List<Specialty>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return specialties ?? new List<Specialty>();
        }

        //Implementación de métodos para especialidades 
        public List<Specialty> GetSpecialties()
        {
            return _specialties;
        }
        public Specialty? GetSpecialty(Guid id)
        {
            return _specialties.FirstOrDefault(s => s._id == id);
        }
        public void AddSpecialty(Specialty specialty)
        {
            _specialties.Add(specialty);
        }
        public void UpdateSpecialty(Specialty specialty)
        {
            var existingSpecialty = GetSpecialty(specialty._id);
            if (existingSpecialty != null)
            {
                existingSpecialty._name = specialty._name;
                existingSpecialty._description = specialty._description;
            }
        }
        public void DeleteSpecialty(Guid id)
        {
            var specialty = GetSpecialty(id);
            if (specialty != null)
            {
                _specialties.Remove(specialty);
            }
        }

        //Implementación de métodos para doctores

        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }
        public Doctor? GetDoctor(Guid id)
        {
            return _doctors.FirstOrDefault(d => d._id == id);
        }
        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }
        public void UpdateDoctor(Doctor doctor)
        {
            var existingDoctor = GetDoctor(doctor._id);
            if (existingDoctor != null)
            {
                existingDoctor._name = doctor._name;
                existingDoctor._licenseNumber = doctor._licenseNumber;
                existingDoctor._isActive = doctor._isActive;
                existingDoctor._specialty = doctor._specialty;
            }
        }
        public void DeleteDoctor(Guid id)
        {
            var doctor = GetDoctor(id);
            if (doctor != null)
            {
                _doctors.Remove(doctor);
            }
        }




    }
}
