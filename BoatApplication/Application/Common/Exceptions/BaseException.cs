using BoatApplication.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application.Common.Exceptions
{
    public class BaseException : Exception
    {
        public List<Error> Errors = new List<Error>();
        public BaseException(string msg) : base(msg)
        {
            Errors = new List<Error>() { };
        }
        public BaseException(Error error, string msg) : base(msg)
        {
            Errors = new List<Error>() { error };
        }
        public BaseException(IEnumerable<Error> errors, string msg) : base(msg)
        {
            Errors = errors.ToList();
        }
    }
}
