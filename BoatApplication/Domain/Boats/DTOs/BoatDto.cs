using AutoMapper;
using BoatApplication.Domain.Boats.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApplication.Domain.Boats.DTOs
{
    public class BoatDto
    {
        public int Id { get; set; } = 0;
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Boat, BoatDto>();
            }
        }
    }
}
