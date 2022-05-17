using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Data;
using AutoDealer.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class EngineTypeRepository : IEngineTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EngineTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IQueryable<EngineType> EngineTypes => _dbContext.EngineTypes;

        public bool Create(EngineType engineType)
        {
            _dbContext.Add(engineType);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(EngineType engineType)
        {
            _dbContext.Remove(engineType);
            return _dbContext.SaveChanges() > 0;
        }

        public EngineType GetById(int engineId) => _dbContext.EngineTypes.FirstOrDefault(eng => eng.Id == engineId);

        public bool Update(EngineType engineType)
        {
            _dbContext.Update(engineType);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
