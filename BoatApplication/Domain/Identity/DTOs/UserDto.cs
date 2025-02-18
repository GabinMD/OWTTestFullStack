using AutoMapper;
using BoatApplication.Domain.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BoatApplication.Domain.Identity.DTOs
{
    public class UserDto : IUser
    {
        public string? Id { get; set; } = string.Empty;

        public string? Name { get; set; } = string.Empty;
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<IUser, UserDto>();
            }
        }
    }
}
