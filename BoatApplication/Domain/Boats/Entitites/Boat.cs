using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Events;
using BoatApplication.Domain.Common.Entities;

namespace BoatApplication.Domain.Boats.Entitites
{
    public class Boat : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public void OnCreated() => AddDomainEvent(new BoatCreatedEvent(this));
        public void OnModified(BoatDto oldState) => AddDomainEvent(new BoatModifiedEvent(oldState, this));
        public void OnDeleted(BoatDto boat) => AddDomainEvent(new BoatDeletedEvent(boat));
    }
}
