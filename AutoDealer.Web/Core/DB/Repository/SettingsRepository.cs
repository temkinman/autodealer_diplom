using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SettingsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<AdvSettings> Settings => _dbContext.AdvSettings;

        public bool Create(AdvSettings settings)
        {
            _dbContext.Add(settings);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(AdvSettings settings)
        {
            _dbContext.Remove(settings);
            return _dbContext.SaveChanges() > 0;
        }

        public AdvSettings GetById(int settingsId) =>
            _dbContext.AdvSettings.FirstOrDefault(set => set.Id == settingsId);

        public bool Update(AdvSettings settings)
        {
            _dbContext.Update(settings);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
