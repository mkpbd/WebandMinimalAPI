using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace CompanyEmployee
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            try
            {
                CreateMap<Employee, EmployeeDto>();
                CreateMap<Company, CompanyDto>()
                    .ForCtorParam("FullAddress",
                    opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country))
                    );
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
