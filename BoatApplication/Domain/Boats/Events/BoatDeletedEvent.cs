using BoatApplication.Domain.Boats.DTOs;
using BoatApplication.Domain.Boats.Entitites;
using BoatApplication.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Boats.Events
{
    public class BoatDeletedEvent : BaseDomainEvent
    {
        public BoatDto Boat { get; }
        public BoatDeletedEvent(BoatDto boat)
        {
            Boat = boat;
        }
    }
}
