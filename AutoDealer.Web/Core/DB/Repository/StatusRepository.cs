using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StatusRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IQueryable<Status> Statuses => _dbContext.Statuses;

        public bool Create(Status status)
        {
            _dbContext.Add(status);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Status status)
        {
            _dbContext.Remove(status);
            return _dbContext.SaveChanges() > 0;
        }

        public Status GetById(int statusId) => _dbContext.Statuses.FirstOrDefault(s => s.Id == statusId);

        public bool Update(Status status)
        {
            _dbContext.Update(status);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
