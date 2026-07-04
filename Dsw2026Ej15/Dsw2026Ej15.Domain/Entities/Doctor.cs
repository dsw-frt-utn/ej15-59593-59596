namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string LicenseNumber { get; private set; } = string.Empty;
        public bool IsActive { get; private set; }
        public Speciality? Speciality { get; private set; }

        private Doctor()
        {
        }

        public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            Speciality = speciality;
            IsActive = true;
        }

        public void Desactivate()
        {
            IsActive = false;
        }
    }
}
