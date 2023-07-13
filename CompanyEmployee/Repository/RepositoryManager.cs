using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private ApplicationDBContext _dbContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;

        public RepositoryManager(ApplicationDBContext applicationDBContext)
        {
            _dbContext = applicationDBContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null) _companyRepository = new CompanyRepository(_dbContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null) _employeeRepository = new EmployeeRepository(_dbContext);
                return _employeeRepository;
            }
        }
        public void Save() => _dbContext.SaveChanges();
        public Task SaveAsync() => _dbContext.SaveChangesAsync();
    }
}
