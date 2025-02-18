using BoatApplication.Application.Common.Exceptions;
using BoatApplication.Domain.Common.Models;
using FluentValidation.Results;

namespace BoatApplication.Domain.Common.Exceptions;

public class ValidationException : BaseException
{
    private const string DefaultMessage = "One or more validation failures have occurred.";
    public ValidationException(Error error)
    : base(error, DefaultMessage)
    {
        Errors = new List<Error>() { error };
    }
    public ValidationException(IEnumerable<Error> errors)
    : base(errors, DefaultMessage)
    {
        Errors = errors.ToList();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : base(DefaultMessage)
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray())
            .Select(e => new Error() { Code = e.Key, Description = string.Join(", ", e.Value) }).ToList();
    }



}
