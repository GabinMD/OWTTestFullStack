using BoatApplication.Application.Common.Attributes;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Extentions;
using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.Interfaces;
using System.Reflection;

namespace BoatApplication.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUser _user;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(
            IUser user, 
            IIdentityService identityService)

        {
            _user = user;
            _identityService = identityService;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                if (!_user.IsUser())
                {
                    throw new UnauthorizedAccessException();
                }

                await ValidateRoles(authorizeAttributes);
                await ValidatePolicies(authorizeAttributes);
            }

            return await next();
        }

        private async Task ValidateRoles(IEnumerable<AuthorizeAttribute> authorizeAttributes)
        {
            var roles = authorizeAttributes.GetRoles();
            if (!roles.Any())
                return;
            bool isAuthorized = false;
            foreach (var role in roles)
            {
                var hasRole = await _identityService.HasRoleAsync(_user, role);
                if (hasRole.Succeeded)
                {
                    isAuthorized = true;
                    break;
                }
            }
            if (!isAuthorized)
                throw new ForbiddenAccessException(new Error(Constants.RequestErrors.ForbbidenAccess));
        }

        private async Task ValidatePolicies(IEnumerable<AuthorizeAttribute> authorizeAttributes)
        {
            var policies = authorizeAttributes.GetPolicies();
            if (!policies.Any())
                return;
            bool isAuthorized = false;
            foreach (var policy in authorizeAttributes.GetPolicies())
            {
                var hasRole = await _identityService.AuthorizeAsync(_user, policy);
                if (hasRole.Succeeded)
                {
                    isAuthorized = true;
                    break;
                }
            }
            if (!isAuthorized)
                throw new ForbiddenAccessException(new Error(Constants.RequestErrors.ForbbidenAccess));
        }
    }
}
