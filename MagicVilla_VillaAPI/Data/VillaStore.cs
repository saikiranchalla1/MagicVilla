﻿using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;

namespace MagicVilla_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villas = new List<VillaDTO>
            {
                new VillaDTO {Id = 1, Name = "Villa 1", Occupancy = 4, Sqft = 100},
                new VillaDTO {Id = 2, Name = "Villa 2", Occupancy = 3, Sqft = 200},
            };
    }
}
