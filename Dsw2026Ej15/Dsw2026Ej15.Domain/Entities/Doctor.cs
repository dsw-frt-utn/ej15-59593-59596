namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string _name { get; set; } = string.Empty;
        public string _licenseNumber { get; set; } = string.Empty;
        public bool _isActive { get; set; }
        public Specialty? _specialty { get; set; } = null;

        public Doctor(string name, string licenseNumber, Specialty specialty, Guid? id = null) : base(id)
        {
            _name = name;
            _licenseNumber = licenseNumber;
            _specialty = specialty;
            _isActive = true;
        }
        public void Inactivate()
        {
            _isActive = false;
        }
    }
}
