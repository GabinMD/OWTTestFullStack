using BoatApplication.Application.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Common.Exceptions
{
    public class ForbiddenAccessException : BaseException
    {
        private const string DefaultMessage = "Forbidden Access Exception.";
        public ForbiddenAccessException(Error error)
        : base(error, DefaultMessage)
        {
            Errors = new List<Error>() { error };
        }
        public ForbiddenAccessException(IEnumerable<Error> errors)
        : base(errors, DefaultMessage)
        {
            Errors = errors.ToList();
        }
    }
}
