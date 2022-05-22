using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class ModelRepository : IModelRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ModelRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IQueryable<Model> Models => _dbContext.Models.Include(m => m.Company);

        public bool Create(Model model)
        {
            _dbContext.Add(model);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Model model)
        {
            _dbContext.Remove(model);
            return _dbContext.SaveChanges() > 0;
        }

        public Model GetById(int modelId) => _dbContext.Models.Include(m => m.Company).FirstOrDefault(m => m.Id == modelId);


        public bool Update(Model model)
        {
            _dbContext.Update(model);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
