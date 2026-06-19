using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Speciality> _specialities = [];
        private List<Doctor> _doctors = [];

        public PersistenceInMemory()
        {
            LoadSpecialities();
            LoadDoctors();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.SingleOrDefault(s => s._id == id);
        }

        public void SaveDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public IEnumerable<Doctor> GetActiveDoctors()
        {
            return _doctors
                .Where(d => d._isActive)
                .ToList();
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                var json = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? [];
                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
            }
            catch (Exception)
            {
            }
        }
        private void LoadDoctors()
        {
            if (!_specialities.Any())
                return;

            var speciality1 = _specialities[0];
            var speciality2 = _specialities.Count > 1 ? _specialities[1] : _specialities[0];
            var speciality3 = _specialities.Count > 2 ? _specialities[2] : _specialities[0];

            var doctor1 = new Doctor(
                "Juan Pérez",
                "MP12345",
                speciality1,
                Guid.Parse("11111111-1111-1111-1111-111111111111")
            );

            var doctor2 = new Doctor(
                "María Gómez",
                "MP67890",
                speciality2,
                Guid.Parse("22222222-2222-2222-2222-222222222222")
            );

            var doctor3 = new Doctor(
                "Carlos Díaz",
                "MP54321",
                speciality3,
                Guid.Parse("33333333-3333-3333-3333-333333333333")
            );

            doctor3.Inactivate();

            _doctors.Add(doctor1);
            _doctors.Add(doctor2);
            _doctors.Add(doctor3);
        }
    }
}
