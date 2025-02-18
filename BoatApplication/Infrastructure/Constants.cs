using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Infrastructure
{
    internal static partial class Constants
    {
        internal static class ApplicationDbContext
        {
            public static string ConnectionString = "BoatDb";
        }
        internal static class IdentityErrors
        {
            public static (string code, string description) UserNotFound = ("UserNotFound", "User not found.");
            public static (string code, string description) UserMissingRole = ("UserMissingRole", "User missing role.");
            public static (string code, string description) UserNotAllowed = ("UserNotAllowed", "User not allowed.");
            public static (string code, string description) CouldNotCreateUser = ("CouldNotCreateUser", "Could not create user.");
        }
        internal static class SignInErrors
        {
            public static (string code, string description) InvalidCredentials = ("InvalidCredentials", "Invalid credentials.");
            public static (string code, string description) RequiresTwoFactor = ("RequiresTwoFactor", "Requires two factor authentication.");
            public static (string code, string description) LockedOut = ("LockedOut", "User account is locked out.");
            public static (string code, string description) NotAllowed = ("NotAllowed", "User is not allowed to sign in.");
        }
        internal static class TokenErrors
        {
            public static (string code, string description) InvalidToken = ("InvalidToken", "Invalid token.");
            public static (string code, string description) ExpiredToken = ("ExpiredToken", "Expired token.");
            public static (string code, string description) InvalidRefreshToken = ("InvalidRefreshToken", "Invalid refresh token.");
            public static (string code, string description) ExpiredRefreshToken = ("ExpiredRefreshToken", "Expired refresh token.");
        }

        internal static class BoatRepositoryErrors
        {
            public static (string code, string description) BoatNotFound = ("BoatNotFound", "Boat not found.");
            public static (string code, string description) BoatNotCreated = ("BoatNotCreated", "Boat not created.");
            public static (string code, string description) BoatNotUpdated = ("BoatNotUpdated", "Boat not updated.");
            public static (string code, string description) BoatNotDeleted = ("BoatNotDeleted", "Boat not deleted.");
            public static (string code, string description) BoatsNotPurged = ("BoatsNotPurged", "Boats not purged.");
        }
    }
}
