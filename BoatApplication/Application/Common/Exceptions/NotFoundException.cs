using BoatApplication.Application.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        private const string DefaultMessage = "Ressource not found.";
        public NotFoundException(Error error)
        : base(error, DefaultMessage)
        {
            Errors = new List<Error>() { error };
        }
        public NotFoundException(IEnumerable<Error> errors)
        : base(errors, DefaultMessage)
        {
            Errors = errors.ToList();
        }
    }
}
