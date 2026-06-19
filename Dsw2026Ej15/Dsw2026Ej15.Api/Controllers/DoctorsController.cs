using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Entities;


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
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
                return BadRequest("Nombre y Matrícula son requeridos");

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality is null)
                return BadRequest("Especialidad no existe");

            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
            _persistence.SaveDoctor(doctor);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveDoctors()
        {
            var doctors = _persistence.GetActiveDoctors().Select(d => new DoctorModel.Response(d.Id, d.Name,d.LicenseNumber, d.Specialty!.Name)).ToList();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActiveDoctorById(Guid id)
        {
            var doctor = _persistence.GetActiveDoctorById(id);

            if (doctor is null)
                return NotFound();

            var response = new DoctorModel.DetailResponse(doctor.Name, doctor.LicenseNumber, doctor.Specialty!.Name);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DesactivateDoctor(Guid id)
        {
            var desactivate = _persistence.DesactivateDoctor(id);

            if (!desactivate)
                return NotFound();

            return NoContent();
        }

    }
}

