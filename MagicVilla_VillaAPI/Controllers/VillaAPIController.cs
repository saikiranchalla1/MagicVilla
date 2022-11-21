﻿using MagicVilla_VillaAPI.Data;
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

        [HttpGet("{id:int}", Name = "GetVilla")]
        /*[ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]*/
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(VillaDTO))]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
        {
            // If [ApiController] attribute is not added to the class, the
            // validations can be performed using the code below
            // Also, note that the below code when used with [ApiController] is hit
            // ony when the model is valid

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (VillaStore.villas.FirstOrDefault(u => u.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa Already Exists!");
                return BadRequest(ModelState);
            }

            if (villa == null)
            {
                return BadRequest();
            }

            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villa.Id = VillaStore.villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            VillaStore.villas.Add(villa);

            // return Ok(villa);
            return CreatedAtRoute("GetVilla", new {id = villa.Id}, villa);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
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

            VillaStore.villas.Remove(villa);

            return NoContent();

        }
    }
}
