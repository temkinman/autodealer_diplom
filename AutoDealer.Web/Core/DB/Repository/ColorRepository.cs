using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ColorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IQueryable<Color> Colors => _dbContext.Colors;

        public bool Create(Color color)
        {
            _dbContext.Add(color);
            return _dbContext.SaveChanges() > 0;
        }

        public Color GetById(int colorId) => _dbContext.Colors.FirstOrDefault(c => c.Id == colorId);

        public bool Update(Color color)
        {
            _dbContext.Update(color);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Color color)
        {
            _dbContext.Remove(color);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
