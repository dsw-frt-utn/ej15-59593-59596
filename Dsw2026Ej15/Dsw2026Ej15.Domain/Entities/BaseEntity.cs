namespace Dsw2026Ej15.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; }

        protected BaseEntity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }
    }
}
