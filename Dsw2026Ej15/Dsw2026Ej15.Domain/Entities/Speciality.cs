namespace Dsw2026Ej15.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        private Speciality()
        {
        }

        public Speciality(string name, string description, Guid? id = null) : base(id)
        {
            Name = name;
            Description = description;
        }
    }
}
