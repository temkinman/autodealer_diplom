using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IQueryable<Customer> Customers => _dbContext.Customers;

       
        public bool Create(Customer customer)
        {
            _dbContext.Add(customer);
            return _dbContext.SaveChanges() > 0;
        }

        public Customer GetById(int customerId) => _dbContext.Customers.FirstOrDefault(c => c.Id == customerId);

        public bool Update(Customer customer)
        {
            _dbContext.Update(customer);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Customer customer)
        {
            _dbContext.Remove(customer);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
