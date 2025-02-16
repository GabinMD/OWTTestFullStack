namespace BoatApplication.Domain.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<(string code,string description)> errors)
        {
            Succeeded = succeeded;
            Errors = errors.Select(e => new Error()
            {
                Code = e.code,
                Description = e.description
            }).ToArray();
        }

        internal Result(bool succeeded, IEnumerable<Error> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; init; }

        public Error[] Errors { get; init; }

        public static Result Success()
        {
            return new Result(true, Array.Empty<(string, string)>());
        }

        public static Result Failure(IEnumerable<(string, string)> errors)
        {
            return new Result(false, errors);
        }

        public static Result Failure((string, string) error)
        {
            return new Result(false, [error]);
        }
    }
}
