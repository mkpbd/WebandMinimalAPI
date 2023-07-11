## Web API Project

API Request And Response

![1688996100242](image/readme/1688996100242.png)

How its work

![1688996134058](image/readme/1688996134058.png)

![1688996165519](image/readme/1688996165519.png)

![1688996175410](image/readme/1688996175410.png)

![1688996194118](image/readme/1688996194118.png)

![1688996215669](image/readme/1688996215669.png)

![1688996234801](image/readme/1688996234801.png)

![1688996251657](image/readme/1688996251657.png)

![1688996265426](image/readme/1688996265426.png)

![1688996277868](image/readme/1688996277868.png)

![1688996294338](image/readme/1688996294338.png)

 More Requst Options

![1688996326162](image/readme/1688996326162.png)

More request Metadata

![1688996357803](image/readme/1688996357803.png)![1688996375846](image/readme/1688996375846.png)![1688996390002](image/readme/1688996390002.png)

![1688996424433](image/readme/1688996424433.png)

![1688996440794](image/readme/1688996440794.png)

![1688996453159](image/readme/1688996453159.png)

![1688996463557](image/readme/1688996463557.png)

Install Web API

Create A Controller

***[ApiController]***  this attribute by  Model validation Request data and other includes    [More inforamation](https://docs.microsoft.com/aspnet/core/web-api/#apicontroller-attribute)

***ControllerBase***  A base class for an MVC controller without view support. [Get More Inforormation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-7.0)

***[Route("/api/v1/[controller]")]***    Attribute Routing in ASP.NET Web API 2   [Get More information](https://learn.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2)

```csharp
    [Route("/api/v1/[controller]")]
    //[Route("/api/v1/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    { 
    }

```

Create Controller with  HTTP GET Method

```csharp
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

    }
}

```

Create HttpGetMethod with   Status Code  and route Name

```csharp
      //[Route("{id:int}", Name = "GetVillbyId")]
        [HttpGet("{id:int}" , Name = "GetVillbyId")]
        [ProducesResponseType(200, Type = typeof(VillaDto))]
        [ProducesResponseType(400, Type = typeof(VillaDto))]
        [ProducesResponseType(404, Type = typeof(VillaDto))]
        [ProducesResponseType(500, Type = typeof(VillaDto))]
        public ActionResult<VillaDto> GetVillaById(int id)
        {
            if (id == 0) return BadRequest();
            var singleVilla = VillaStore.VillaList.Where(x => x.Id == id).FirstOrDefault();
            if(singleVilla == null)
            {
                return NotFound();
            }
            return Ok(singleVilla);
        }
```

Create Villa   HttpPost  API  With  Status Code

```csharp
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
```

Create ApI Update Villa HttpPut

```csharp
[HttpPut("{id:int}")]
        public ActionResult UpdateVilla(int id, [FromBody] VillaDto villa)
        {
            if(villa == null || id == villa.Id) return BadRequest();

            var vil = VillaStore.VillaList.Where(x => x.Id == id).FirstOrDefault();
            vil.Name = villa.Name;
            vil.Address = villa.Address;
            return NoContent();
        }
```

Create Api DeleteVilla By Id  httpDelete

```csharp
 [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public ActionResult DeleteVilla(int id)
        {
            if (id <= 0) return BadRequest();
            var vil = VillaStore.VillaList.Where(x => x.Id == id).FirstOrDefault();
            VillaStore.VillaList.Remove(vil);
            return NoContent();
        }
```

Install ***Microsoft.EntityFrameworkCore***  With other dependency For Getting data Database

1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.SqlServer
3. Microsoft.EntityFrameworkCore.Design

Configuration Program css Files

```csharp
    builder.Services.AddDbContext<ApplicationDbContext>(option => {
             option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            });
```

Create  a ApplicationDbContext Class and get Connections

```csharp
using Microsoft.EntityFrameworkCore;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Villa>().HasData(
            //    new Villa()
            //    {
            //        Id = 1,
            //        Address = "Abc",
            //        Name = "Name1",
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now,
            //    }

            //    ); 

        }
        public DbSet<Villa> Villas { get; set; }

    }
}

```

Configure in appsettings.json files

```json
  "ConnectionStrings": {
    "DefaultSQLConnection": "Server=SERVER\\MSSQLSERVER01;Database=WebApI;Trusted_Connection=True; MultipleActiveResultSets=true"
  },
```

Full Api  Controller

```csharp
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
            if(villa == null || id != villa.Id) return BadRequest();

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

```
