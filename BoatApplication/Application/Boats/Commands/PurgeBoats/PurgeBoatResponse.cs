using BoatApplication.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Boats.Commands.PurgeBoats
{
    public class PurgeBoatResponse : BaseAPIResponse
    {
        public int PurgedBoats { get; set; } = 0;

        public static PurgeBoatResponse Success(int purgedBoats)
        {
            return new PurgeBoatResponse() { PurgedBoats = purgedBoats };
        }

        public static new PurgeBoatResponse Failure(List<Error> errors)
        {
            return new PurgeBoatResponse() { Status = EStatus.Error, Errors = errors };
        }
    }
}
