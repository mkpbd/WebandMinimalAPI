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
        private readonly ApplicationDbContext _context;
        public VillaAPIController(ApplicationDbContext db)
        {
                _context = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
           // return VillaStore.VillaList;
           return Ok(_context.Villas.ToList());
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
            //var singleVilla = VillaStore.VillaList.Where(x => x.Id == id).FirstOrDefault();
            //if (singleVilla == null)
            //{
            //    return NotFound();
            //}

            //return Ok(singleVilla);

            var getDataFromDbVilla = _context.Villas.FirstOrDefault(x => x.Id == id);
            if (getDataFromDbVilla == null) return NoContent();
            return Ok(getDataFromDbVilla);

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

            //var lastId = VillaStore.VillaList.LastOrDefault();
            //villa.Id = lastId.Id > 0 ? lastId.Id + 1 : 1;

            //VillaStore.VillaList.Add(villa);
            //return Ok(villa);

            Villa v1 = new Villa()
            {
                Name = villa.Name,
                Address = villa.Address,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now

            };

            _context.Villas.Add(v1);
            _context.SaveChanges();
            return Ok(v1);
        }


        [HttpPut("{id:int}")]
        public ActionResult UpdateVilla(int id, [FromBody] VillaDto villa)
        {
            if(villa == null || id == villa.Id) return BadRequest();

            //var vil = VillaStore.VillaList.Where(x => x.Id == id).FirstOrDefault();
            //vil.Name = villa.Name;
            //vil.Address = villa.Address;

            var vil = _context.Villas.Where(x => x.Id == id).FirstOrDefault();
            vil.Name = villa.Name;
            vil.Address = villa.Address;
            vil.UpdatedDate = DateTime.Now;
            _context.Villas.Update(vil);
            _context.SaveChanges();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public ActionResult DeleteVilla(int id)
        {
            if (id <= 0) return BadRequest();

            //var vil = VillaStore.VillaList.Where(x => x.Id == id).FirstOrDefault();

            //VillaStore.VillaList.Remove(vil);

            var villDb = _context.Villas.Where(x => x.Id == id).FirstOrDefault();
            if(villDb == null) return NotFound();
            _context.Remove(villDb);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
