using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployee.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<CompaniesController> _logger;
        private readonly IMapper _mapper;
        public CompaniesController(IRepositoryManager repository, ILogger<CompaniesController> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges: false);

                //var companiesDto = companies.Select(c => new CompanyDto
                //{
                //    Id = c.Id,
                //    Name = c.Name,
                //    FullAddress = string.Join(' ', c.Address, c.Country)
                //}).ToList();

                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                //var mapList = companies.Select(_mapper.Map<CompanyDto>);

                return Ok(companiesDto);

                //return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.Company.GetCompany(id, trackChanges: false);

            if (company == null)
            {
                _logger.LogError($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company); return Ok(companyDto);
            }
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyForCreationDto object sent from client is null.");

                return BadRequest("CompanyForCreationDto object is null");
            }
            var companyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(companyEntity);

            _repository.Save(); var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return CreatedAtRoute("GetCompany", new { id = companyToReturn.Id }, companyToReturn);
        }
    }
}
