using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IQueryable<Company> Companies => _dbContext.Companies;

        public bool Create(Company company)
        {
            _dbContext.Add(company);
            return _dbContext.SaveChanges() > 0;
        }

        Company IBaseRepository<Company>.GetById(int companyId) => _dbContext.Companies.FirstOrDefault(comp => comp.Id == companyId);

        public bool Update(Company company)
        {
            _dbContext.Update(company);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Company company)
        {
            _dbContext.Remove(company);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
