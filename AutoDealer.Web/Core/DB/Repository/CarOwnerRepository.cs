using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class CarOwnerRepository : ICarOwnerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CarOwnerRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IQueryable<CarOwner> CarOwners => _dbContext.CarOwners;

        public bool Create(CarOwner owner)
        {
            _dbContext.Add(owner);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(CarOwner owner)
        {
            _dbContext.Remove(owner);
            return _dbContext.SaveChanges() > 0;
        }

        public CarOwner GetById(int ownerId) => _dbContext.CarOwners.FirstOrDefault(owner => owner.Id == ownerId);

        public bool Update(CarOwner owner)
        {
            _dbContext.Update(owner);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
