using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class EngineRepository : IEngineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EngineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IQueryable<Engine> Engines => _dbContext.Engines.Include(eng => eng.Type);

        public bool Create(Engine engine)
        {
            _dbContext.Add(engine);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Engine engine)
        {
            _dbContext.Remove(engine);
            return _dbContext.SaveChanges() > 0;
        }

        public Engine GetById(int engineId) => _dbContext.Engines.FirstOrDefault(eng => eng.Id == engineId);

        public bool Update(Engine engine)
        {
            _dbContext.Update(engine);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
