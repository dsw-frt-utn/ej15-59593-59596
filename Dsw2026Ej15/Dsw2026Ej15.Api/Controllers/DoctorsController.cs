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
            var doctors = _persistence.GetActiveDoctors()
                .Select(d => new DoctorModel.Response(
                    d._id,
                    d._name,
                    d._licenseNumber,
                    d._specialty!._name
                ))
                .ToList();

            return Ok(doctors);
        }

    }
}

