using BoatApplication.Application.Boats.Attributes;
using BoatApplication.Application.Boats.Interfaces;
using BoatApplication.Domain.Boats.Constants;
using BoatApplication.Domain.Boats.Interfaces;
using BoatApplication.Domain.Common.Exceptions;
using BoatApplication.Domain.Common.Extentions;
using BoatApplication.Domain.Common.Interfaces.Services;
using BoatApplication.Domain.Common.Models;
using BoatApplication.Domain.Identity.Interfaces;
using System.Reflection;

namespace BoatApplication.Application.Boats.Behaviours
{
    public class BoatAuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUser _user;
        private readonly IBoatService _boatService;

        public BoatAuthorizationBehaviour(
            IUser user,
            IBoatService boatService
            )

        {
            _user = user;
            _boatService = boatService;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not IBoatRequest)
            {
                return await next();
            }
            var authorizeAttributes = request.GetType().GetCustomAttributes<BoatAuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                if (!_user.IsUser())
                {
                    throw new UnauthorizedAccessException();
                }
                if (!(await ValidatePolicies(authorizeAttributes, _user, request)))
                {
                    throw new ForbiddenAccessException(new Error(Constants.RequestErrors.ForbbidenAccess));
                }
            }

            return await next();
        }

        private async Task<bool> ValidatePolicies(
            IEnumerable<BoatAuthorizeAttribute> authorizeAttributes,
            IUser user,
            TRequest request)
        {
            bool isAuthorized = false;
            foreach (var policy in authorizeAttributes.GetBoatPolicies())
            {
                switch (policy)
                {
                    case nameof(BoatPolicies.IsOwner):
                        isAuthorized = await ValideIsOwner(user, request);
                        break;
                    default:
                        break;
                }
                if (isAuthorized)
                    break;
            }
            return isAuthorized;
        }

        private async Task<bool> ValideIsOwner(IUser user, TRequest request)
        {
            if (request is ITargetBoatRequest targetBoatRequest)
            {
                bool isOwner = await _boatService.IsBoatOwner(user, targetBoatRequest.Id);
                return isOwner;
            }
            return false;
        }
    }
}
