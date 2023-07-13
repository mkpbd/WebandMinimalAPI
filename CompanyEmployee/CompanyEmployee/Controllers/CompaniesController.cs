using AutoMapper;
using CompanyEmployee.ModelBinders;
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
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                //var companies = _repository.Company.GetAllCompanies(trackChanges: false);
                var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges: false);

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
        public async Task<IActionResult> GetCompany(Guid id)
        {
            //var company = _repository.Company.GetCompany(id, trackChanges: false);
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges: false);

            if (company == null)
            {
                _logger.LogError($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
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

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]

        public async Task<IActionResult> GetCompanyCollection(IEnumerable<Guid> ids)
        //public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");

                return BadRequest("Parameter ids is null");
            }

            //var companyEntities = _repository.Company.GetByIds(ids, trackChanges: false);
            var companyEntities = await _repository.Company.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != companyEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");

                return NotFound();
            }

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            return Ok(companiesToReturn);

        }

        [HttpPost("collection")]
        public async Task< IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection == null)
            {
                _logger.LogError("Company collection sent from client is null.");

                return BadRequest("Company collection is null");
            }

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }
            //_repository.Save();
           await _repository.SaveAsync();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("CompanyCollection", new { ids }, companyCollectionToReturn);
        }


        [HttpDelete("{id}")]
        public async Task< IActionResult> DeleteCompany(Guid id)
        {
            //var company = _repository.Company.GetCompany(id, trackChanges: false);
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges: false);

            if (company == null)
            {
                _logger.LogError($"Company with id: {id} doesn't exist in the database.");

                return NotFound();
            }

            _repository.Company.DeleteCompany(company);

            //_repository.Save();
           await _repository.SaveAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyForUpdateDto object sent from client is null.");
                return BadRequest("CompanyForUpdateDto object is null");
            }
            //var companyEntity = _repository.Company.GetCompany(id, trackChanges: true);
            var companyEntity = await _repository.Company.GetCompanyAsync(id, trackChanges: true);

            if (companyEntity == null)
            {
                _logger.LogError($"Company with id: {id} doesn't exist in the database.");

                return NotFound();
            }
            _mapper.Map(company, companyEntity);

            //_repository.Save();
           await _repository.SaveAsync();

            return NoContent();
        }



    }
}
