using BoatApplication.Domain.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Infrastructure.Identity
{
    public static class AuthorizationResultExtentions
    {
        public static Result ToApplicationResult(this AuthorizationResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Failure.FailureReasons.Select(f => ("Unauthorized", f.Message)));
        }
    }
}
