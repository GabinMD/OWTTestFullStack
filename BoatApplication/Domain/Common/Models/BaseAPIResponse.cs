using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Common.Models
{

    public class BaseAPIResponse
    {
        public enum EStatus
        {
            Success = 0,
            Error
        }
        public EStatus Status { get; set; } = EStatus.Success;
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
