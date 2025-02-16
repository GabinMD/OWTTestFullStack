using BoatApplication.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Identity.Models
{
    public class AuthResponse : BaseAPIResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public static AuthResponse Success(string token, string refreshToken)
        {
            return new AuthResponse() { Token = token, RefreshToken = refreshToken };
        }

        public static AuthResponse Failure(List<Error> errors)
        {
            return new AuthResponse() {  Status = EStatus.Error, Errors = errors };
        }
    }
}
