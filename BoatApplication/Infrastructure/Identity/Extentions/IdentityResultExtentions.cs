using BoatApplication.Domain.Common.Models;
using Microsoft.AspNetCore.Identity;
namespace BoatApplication.Infrastructure.Identity.Extentions
{
    public static class IdentityResultExtentions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => (e.Code, e.Description)));
        }
    }
}
