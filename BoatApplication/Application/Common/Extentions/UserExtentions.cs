using BoatApplication.Domain.Common.Interfaces;
using BoatApplication.Domain.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Common.Extentions
{
    public static class UserExtentions
    {
        public static bool IsUser(this IUser user)
        {
            return user?.Id != null;
        }
    }
}
