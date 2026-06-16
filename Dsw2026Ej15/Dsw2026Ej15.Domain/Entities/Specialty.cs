namespace Dsw2026Ej15.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string _name { get; set; } = string.Empty;
        public string _description { get; set; } = string.Empty;

        public Specialty(string name, string description, Guid? id = null) : base(id)
        {
            _name = name;
            _description = description;
        }
    }
}
