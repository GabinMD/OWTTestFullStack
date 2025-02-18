using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.Interfaces
{
    public interface ITargetBoatRequest : IBoatRequest
    {
        public int Id { get; }
    }
}
