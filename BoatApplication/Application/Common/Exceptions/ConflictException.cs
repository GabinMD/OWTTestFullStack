using BoatApplication.Application.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Common.Exceptions
{
    public class ConflictException : BaseException
    {
        private const string DefaultMessage = "Conflict Exception.";
        public ConflictException(Error error)
        : base(error, DefaultMessage)
        {
            Errors = new List<Error>() { error };
        }
        public ConflictException(IEnumerable<Error> errors)
        : base(errors, DefaultMessage)
        {
            Errors = errors.ToList();
        }
    }
}
