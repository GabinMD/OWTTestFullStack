using BoatApplication.Domain.Common.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Infrastructure.Identity
{
    public static class SignInResultExtentions
    {
        public static Result ToApplicationResult(this SignInResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.GetErrors());
        }

        public static IEnumerable<(string code, string description)> GetErrors(this SignInResult result)
        {
            var errors = new List<(string, string)>();
            if (result.IsLockedOut)
                errors.Add(Constants.SignInErrors.LockedOut);
            if (result.IsNotAllowed)
                errors.Add(Constants.SignInErrors.NotAllowed);
            if (result.RequiresTwoFactor)
                errors.Add(Constants.SignInErrors.RequiresTwoFactor);
            if (errors.Count == 0)
                errors.Add(Constants.SignInErrors.InvalidCredentials);
            return errors;
        }
    }
}
