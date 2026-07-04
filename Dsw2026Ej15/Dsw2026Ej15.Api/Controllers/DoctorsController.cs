using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                throw new ValidationException("La matrícula es requerida");

            var speciality = await _persistence.GetSpecialityById(request.SpecialityId);

            if (speciality is null)
                throw new ValidationException("La especialidad no existe");

            var doctor = new Doctor(
                request.Name,
                request.LicenseNumber,
                speciality
            );

            await _persistence.SaveDoctor(doctor);

            var response = new DoctorModel.Response(
                doctor.Id,
                doctor.Name,
                doctor.LicenseNumber,
                doctor.Speciality!.Name
            );

            return Created($"api/doctors/{doctor.Id}", response);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveDoctors()
        {
            var doctors = await _persistence.GetActiveDoctors();

            var response = doctors
                .Select(d => new DoctorModel.Response(
                    d.Id,
                    d.Name,
                    d.LicenseNumber,
                    d.Speciality!.Name
                ))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActiveDoctorById(Guid id)
        {
            var doctor = await _persistence.GetActiveDoctorById(id);

            if (doctor is null)
                return NotFound();

            var response = new DoctorModel.DetailResponse(
                doctor.Name,
                doctor.LicenseNumber,
                doctor.Speciality!.Name
            );

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DesactivateDoctor(Guid id)
        {
            var desactivated = await _persistence.DesactivateDoctor(id);

            if (!desactivated)
                return NotFound();

            return NoContent();
        }
    }
}

