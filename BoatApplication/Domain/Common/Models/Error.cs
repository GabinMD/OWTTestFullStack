namespace BoatApplication.Domain.Common.Models
{
    public class Error
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Error() { }
        public Error((string code, string description) error)
        {
            Code = error.code;
            Description = error.description;
        }
        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
