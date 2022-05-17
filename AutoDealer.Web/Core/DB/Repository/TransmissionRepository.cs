using AutoDealer.Web.Data;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class TransmissionRepository : ITransmissionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransmissionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IQueryable<Transmission> Transmissions => _dbContext.Transmissions;

        public bool Create(Transmission transmission)
        {
            _dbContext.Add(transmission);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Transmission transmission)
        {
            _dbContext.Remove(transmission);
            return _dbContext.SaveChanges() > 0;
        }

        public Transmission GetById(int transmissionId) => _dbContext.Transmissions.FirstOrDefault(t => t.Id == transmissionId);

        public bool Update(Transmission transmission)
        {
            _dbContext.Update(transmission);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
