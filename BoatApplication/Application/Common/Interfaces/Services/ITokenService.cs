using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.DTOs;
using BoatApplication.Domain.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Common.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(IUser user);
        string GenerateRefreshToken();
        (Result result, ClaimsPrincipal? principal, IUser? user) ParseToken(string token);
        Task<Result> ValidateRefreshTokenAsync(string userId, string token);
        Task<Result> ValidateRefreshTokenAsync(IUser user, string token) { return ValidateRefreshTokenAsync(user.Id ?? "", token); }
        Task<Result> UpdateRefreshTokenAsync(string userId, string token);
        Task<Result> UpdateRefreshTokenAsync(IUser user, string token) { return UpdateRefreshTokenAsync(user.Id ?? "", token); }
    }
}
