using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Application
{
    internal static partial class Constants
    {
        internal static class Application
        {
            public static string Name = "BoatApplication";
        }
        internal static class RequestErrors
        {
            public static (string code, string description) ForbbidenAccess = ("ForbbidenAccess", "Forbbiden access.");
            public static (string code, string description) NotFound = ("NotFound", "Ressource not found.");
            public static (string code, string description) Conflict = ("Conflict", "Conflict.");
            public static (string code, string description) BadRequest = ("BadRequest", "Bad request.");
        }
    }
}
