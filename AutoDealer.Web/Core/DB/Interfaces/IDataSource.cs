using AutoDealer.Web.Models;
using System;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IDataSource
    {
        IQueryable<Color> Colors { get; set; }
        IQueryable<Model> Models { get; set; }
        IQueryable<Company> Companies { get; set; }
        IQueryable<Engine> Engines { get; set; }
        IQueryable<EngineType> EngineTypes { get; set; }
        IQueryable<Transmission> Transmissions { get; set; }
        void Update(IServiceProvider provider);
    }
}
