﻿using BoatApplication.Domain.Boats.Entitites;
using BoatApplication.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Boats.Events
{
    public class BoatCreatedEvent : BaseDomainEvent
    {
        public Boat Boat { get; }
        public BoatCreatedEvent(Boat boat) {
            Boat = boat;
        }
    }
}
