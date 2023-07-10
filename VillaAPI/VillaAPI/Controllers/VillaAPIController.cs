using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.Models.Dto;

namespace VillaAPI.Controllers
{
    [Route("/api/v1/[controller]")]
    //[Route("/api/v1/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            return VillaStore.VillaList;
        }

        //[Route("{id:int}", Name = "GetVillbyId")]
        [HttpGet("{id:int}", Name = "GetVillbyId")]

        [ProducesResponseType(200, Type = typeof(VillaDto))]
        [ProducesResponseType(400, Type = typeof(VillaDto))]
        [ProducesResponseType(404, Type = typeof(VillaDto))]
        [ProducesResponseType(500, Type = typeof(VillaDto))]
        public ActionResult<VillaDto> GetVillaById(int id)
        {
            if (id == 0) return BadRequest();
            var singleVilla = VillaStore.VillaList.Where(x => x.Id == id).FirstOrDefault();
            if (singleVilla == null)
            {
                return NotFound();
            }

            return Ok(singleVilla);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<VillaDto> CreatedVilla([FromBody] VillaDto villa)
        {

            if (villa == null) return BadRequest();
            if (villa.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

            var lastId = VillaStore.VillaList.LastOrDefault();
            villa.Id = lastId.Id > 0 ? lastId.Id + 1 : 1;

            VillaStore.VillaList.Add(villa);
            return Ok(villa);
        }


    }
}
