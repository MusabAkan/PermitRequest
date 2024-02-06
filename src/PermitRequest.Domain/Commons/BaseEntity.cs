﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PermitRequest.Domain.Commons
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
        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);  
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
