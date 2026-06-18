namespace Dsw2026Ej15.Domain.Entities
{
    public class Speciality : BaseEntity
    {
        public string _name { get; init; }
        public string _description { get; init; }

        public Speciality(string name, string description, Guid? id = null) : base(id)
        {
            _name = name;
            _description = description;
        }
    }
}
