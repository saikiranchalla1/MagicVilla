using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    /*[Route("api/villa")]*/
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas()
        {
            return new List<Villa>
            {
                new Villa {Id = 1, Name = "Villa 1"},
                new Villa {Id = 2, Name = "Villa 2"},
            };
        }

        [HttpGet]
        public IEnumerable<VillaDTO> GetVillasDTO()
        {
            return new List<VillaDTO>
            {
                new VillaDTO {Id = 1, Name = "Villa 1"},
                new VillaDTO {Id = 2, Name = "Villa 2"},
            };
        }

        [HttpGet]
        public IEnumerable<VillaDTO> GetVillasStore()
        {
            return VillaStore.villas;
        }

        [HttpGet("{id:int}")]
        public VillaDTO GetVilla(int id)
        {
            return VillaStore.villas.FirstOrDefault(u => u.Id == id);
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillasStoreWithReturnStatusCode()
        {
            return Ok(VillaStore.villas);
        }

        [HttpGet("{id:int}")]
        public ActionResult<VillaDTO> GetVillaWithReturnStatusCode(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villas.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa) ;
        }
    }
}
