namespace PermitRequest.Domain.Common
{
    public abstract class BaseEntity  
    {
        public Guid Id { get; set; }

        private readonly List<DomainEvent> _domainEvents = new();        
        public IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

        public void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
