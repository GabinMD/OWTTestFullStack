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

        public static BaseAPIResponse Success()
        {
            return new BaseAPIResponse() { Status = EStatus.Success };
        }

        public static BaseAPIResponse Failure(List<Error> errors)
        {
            return new BaseAPIResponse() { Status = EStatus.Error, Errors = errors };
        }
    }
}
