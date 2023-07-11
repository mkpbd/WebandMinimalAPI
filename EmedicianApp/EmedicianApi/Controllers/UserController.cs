
using EmedicianApi.Data;
using EmedicianApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmedicianApi.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public UserController( ApplicationDbContext context)
        {
            _context = context;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
      
        [HttpGet("/singeUser/{id:int}")]
        public ActionResult<User> Get(int id)
        {
            if( id <= 0 ) return BadRequest();
            User user = _context.Users.SingleOrDefault(x => x.Id == id);
            if( user == null ) return NotFound();
            return Ok(user);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllUser")]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var Users = _context.Users.ToList();
            if(Users == null) return NotFound();
            return Ok(Users);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("UserRegister")]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            var getLastUser = _context.Users.OrderByDescending(x=> x.Id).FirstOrDefault();
           return  Ok(getLastUser);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("UpdateUser/{id:int}")]
        public ActionResult<User> UpdateUser(int id, [FromBody] User user)
        {
            if (id <= 0) return BadRequest();
            if(user.Id <=0) return BadRequest();
            User sUser = _context.Users.FirstOrDefault(x => x.Id == id);
            if(sUser == null) return NotFound();
            sUser.FirstName = user.FirstName;
            sUser.LastName = user.LastName;
            sUser.Email = user.Email;
            sUser.Amount = user.Amount;
            sUser.Status = user.Status;
            sUser.Type = user.Type;
            sUser.Updated = user.Updated;
            _context.Users.Update(sUser);
            _context.SaveChanges();
            return Ok(sUser);

        }
    }
}
