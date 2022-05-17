using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AutoDealer.Web.Core.Infrastructure
{
    public class DataSource : IDataSource
    {
        private IServiceProvider _provider;

        public IQueryable<Color> Colors { get; set; }
        public IQueryable<Model> Models { get; set; }
        public IQueryable<Company> Companies { get; set; }
        public IQueryable<Engine> Engines { get; set; }
        public IQueryable<EngineType> EngineTypes { get; set; }
        public IQueryable<Transmission> Transmissions { get; set; }

        public void Update(IServiceProvider provider = null)
        {
            IServiceProvider pr = provider ?? _provider;

            Colors = pr?.GetService<IColorRepository>().Colors.OrderBy(color => color.Title);
            Models = pr?.GetService<IModelRepository>().Models.OrderBy(model => model.Title);
            Companies = pr?.GetService<ICompanyRepository>().Companies.OrderBy(company => company.Title);
            Engines = pr?.GetService<IEngineRepository>().Engines.OrderBy(eng => eng.Type.Title);
            Transmissions = pr?.GetService<ITransmissionRepository>().Transmissions.OrderBy(tran => tran.TransmissionType);
            EngineTypes = pr?.GetService<IEngineTypeRepository>().EngineTypes.OrderBy(engineType => engineType.Title);

            if (provider != null)
                _provider = provider;
        }
    }
}
