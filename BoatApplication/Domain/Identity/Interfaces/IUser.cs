using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Identity.Interfaces
{
    public interface IUser
    {
        string? Id { get; }
        string? Name { get; }
    }
}
