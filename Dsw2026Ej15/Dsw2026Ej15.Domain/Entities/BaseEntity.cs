namespace Dsw2026Ej15.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid _id { get; }

        protected BaseEntity(Guid? id = null)
        {
            _id = id ?? Guid.NewGuid();
        }
    }
}
