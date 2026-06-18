namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string _name { get; init; } 
        public string _licenseNumber { get; init; } 
        public bool _isActive { get; private set; }
        public Speciality? _specialty { get; private set; }

        public Doctor(string name, string licenseNumber, Speciality specialty, Guid? id = null) : base(id)
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
