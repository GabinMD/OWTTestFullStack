using Azure.Core;
using BoatApplication.Domain.Common.Interfaces;
using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.DTOs;
using BoatApplication.Domain.Identity.Interfaces;
using BoatApplication.Infrastructure.Identity.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            UserManager<User> userManager, SignInManager<User> signInManager, IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory, IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<(Result result, IUser? user)> CreateUserAsync(string userName, string password)
        {
            var user = new User
            {
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return (Result.Failure(Constants.IdentityErrors.CouldNotCreateUser), null);

            return (result.ToApplicationResult(), user.ToDto());
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.UserName;
        }

        public async Task<Result> AuthorizeAsync(string userId, string policy)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Result.Failure(Constants.IdentityErrors.UserNotFound);

            var claims = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(claims, policy);
            return result.ToApplicationResult();
        }

        public async Task<Result> HasRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            bool isValid = user != null && await _userManager.IsInRoleAsync(user, roleName);

            return isValid ? Result.Success() : Result.Failure(Constants.IdentityErrors.UserMissingRole);
        }

        public async Task<(Result result, IUser? user)> PasswordSignInAsync(string userName, string passwd)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return (Result.Failure(Constants.IdentityErrors.UserNotFound), null);
            var result = await _signInManager.PasswordSignInAsync(user, passwd, false, false);
            return (result.ToApplicationResult(), user.ToDto());
        }
    }
}
