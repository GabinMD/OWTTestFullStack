using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.Interfaces;

namespace BoatApplication.Domain.Common.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);
        Task<string?> GetUserNameAsync(IUser user) { return GetUserNameAsync(user.Id ?? ""); }
        Task<Result> HasRoleAsync(string userId, string roleName);
        Task<Result> HasRoleAsync(IUser user, string roleName) { return HasRoleAsync(user.Id ?? "", roleName); }
        Task<Result> AuthorizeAsync(string userId, string policy);
        Task<Result> AuthorizeAsync(IUser user, string policy) { return AuthorizeAsync(user.Id ?? "", policy); }
        Task<(Result result, IUser? user)> PasswordSignInAsync(string userName, string passwd);
        Task<(Result result, IUser? user)> CreateUserAsync(string userName, string passwd);
        Task<(Result result, IUser? user)> CreateUserAsync(IUser user, string passwd) { return CreateUserAsync(user.Name ?? "", passwd); }
    }
}
