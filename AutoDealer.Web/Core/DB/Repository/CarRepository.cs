using AutoDealer.Web.Data;
using AutoDealer.Web.Filters;
using AutoDealer.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoDealer.Web.Core.DB.Interfaces;
using AutoDealer.Web.Enums;

namespace AutoDealer.Web.Core.DB.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CarRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        
        public IQueryable<Car> Cars => _dbContext.Cars
                                                .Include(c => c.Settings)
                                                .Include(c => c.Color)
                                                .Include(c => c.Model)
                                                .Include(c => c.Company)
                                                .Include(c => c.Status)
                                                .Include(c => c.Engine)
                                                .Include(c => c.Transmission);
        
        public bool Create(Car car)
        {
            _dbContext.Cars.Add(car);
            return _dbContext.SaveChanges() > 0;
        }

        public Car GetById(int carId) => Cars.FirstOrDefault(c => c.Id == carId);

        public bool Update(Car car)
        {
            _dbContext.Update(car);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(Car car)
        {
            _dbContext.Remove(car);
            return _dbContext.SaveChanges() > 0;
        }

        public IQueryable<Car> GetAllCarsByAdvSettings(AdvSettings settings)
        {
            return _dbContext.Cars.Where(car => car.Settings.Equals(settings));
        }

        public IQueryable<Car> GetAllCarsByCompany(Company company)
        {
            return _dbContext.Cars.Where(car => car.Company.Equals(company));
        }

        public IQueryable<Car> GetAllCarsByModel(Model model)
        {
            return _dbContext.Cars.Where(car => car.Model.Title == model.Title);
        }

        private CarFilter SetFilterIfHasNullProperty(CarFilter filter)
        {
            filter.ProduceDateFrom ??= int.MinValue;
            filter.ProduceDateTo ??= int.MaxValue;

            filter.KilometreFrom ??= 0;
            filter.KilometreTo ??= int.MaxValue;

            filter.PriceFrom ??= 0;
            filter.PriceTo ??= int.MaxValue;

            return filter;
        }

        public IQueryable<Car> GetCarsByFilter(CarFilter filter, int status)
        {
            //List<Car> filtered = new ();
            
            filter = SetFilterIfHasNullProperty(filter);

            //foreach (Car car in Cars)
            //{
            //    if (
            //        IsCarDateInRange(car, filter)
            //        && IsCarKilometreInRange(car, filter)
            //        && IsCarPriceInRange(car, filter)
            //        )
            //    {
            //        filtered.Add(car);
            //    }
            //}

            //Where(car => IsCarDateInRange(car, filter)
            //                                && IsCarKilometreInRange(car, filter)
            //                                && IsCarPriceInRange(car, filter))

            //if (filtered.Count == 0) return filtered;

            //IEnumerable<PropertyInfo> filterSettings = GetAllTrueCarSettingsProperties(filter.Settings);

            //List<Car> filteredByCompany = filtered.Where(car => filter.CompanyId != null ? car.Company?.Id == filter.CompanyId : true).ToList();
            //List<Car> filteredByModel = filteredByCompany.Where(car => filter.ModelId != null ? car.Model?.Id == filter.ModelId : true).ToList();
            //List<Car> filteredByColor = filteredByModel.Where(car => filter.ColorId != null ? car.Color?.Id == filter.ColorId : true).ToList();
            //List<Car> filteredByEngine = filteredByColor.Where(car => filter.EngineTypeId != null ? car.Engine?.Id == filter.EngineTypeId : true).ToList();
            //List<Car> filteredByTransmission = filteredByEngine.Where(car => filter.TransmissionId != null ? car.Transmission?.Id == filter.TransmissionId : true).ToList();
            //List<Car> filteredBySettings = filtered.Where(car => isIncludeInCarSettings(filterSettings, GetAllTrueCarSettingsProperties(car.Settings))).ToList();

            IQueryable<Car> result = Cars
                                .Where(car => car.Status.Id == status)
                                .Where(car => car.ProduceDate >= filter.ProduceDateFrom && car.ProduceDate <= filter.ProduceDateTo)
                                .Where(car => car.Kilometre >= filter.KilometreFrom && car.Kilometre <= filter.KilometreTo)
                                .Where(car => car.Price >= filter.PriceFrom && car.Price <= filter.PriceTo)
                                .Where(car => filter.CompanyId != 0 ? car.Company.Id == filter.CompanyId : true)
                                .Where(car => filter.ModelId != 0 ? car.Model.Id == filter.ModelId : true)
                                .Where(car => filter.ColorId != 0 ? car.Color.Id == filter.ColorId : true)
                                .Where(car => filter.EngineTypeId != 0 ? car.Engine.Id == filter.EngineTypeId : true)
                                .Where(car => filter.TransmissionId != 0 ? car.Transmission.Id == filter.TransmissionId : true)
                                .Where(car => filter.Settings.Abs == true ? filter.Settings.Abs == car.Settings.Abs : true)
                                .Where(car => filter.Settings.Esp == true ? filter.Settings.Esp == car.Settings.Esp : true)
                                .Where(car => filter.Settings.ParkSensors == true ? filter.Settings.ParkSensors == car.Settings.ParkSensors : true)
                                .Where(car => filter.Settings.Camera == true ? filter.Settings.Camera == car.Settings.Camera : true)
                                .Where(car => filter.Settings.Cruiz == true ? filter.Settings.Cruiz == car.Settings.Cruiz : true)
                                .Where(car => filter.Settings.AirCondition == true ? filter.Settings.AirCondition == car.Settings.AirCondition : true)
                                .Where(car => filter.Settings.ClimatControl == true ? filter.Settings.ClimatControl == car.Settings.ClimatControl : true)
                                .Where(car => filter.Settings.Navigation == true ? filter.Settings.Navigation == car.Settings.Navigation : true)
                                .Where(car => filter.Settings.Bluetooth == true ? filter.Settings.Bluetooth == car.Settings.Bluetooth : true);

            //.Where(car => filter.CompanyId != null && filter.CompanyId != 0 ? car.Company.Id == filter.CompanyId : true)
            //                    .Where(car => filter.ModelId != null && filter.ModelId != 0 ? car.Model.Id == filter.ModelId : true)
            //                    .Where(car => filter.ColorId != null && filter.ColorId != 0 ? car.Color.Id == filter.ColorId : true)
            //                    .Where(car => filter.EngineTypeId != null && filter.EngineTypeId != 0 ? car.Engine.Id == filter.EngineTypeId : true)
            //                    .Where(car => filter.TransmissionId != null && filter.TransmissionId != 0 ? car.Transmission.Id == filter.TransmissionId : true)
            //                    .Where(car => filter.Settings.Abs == true ? filter.Settings.Abs == car.Settings.Abs : true)
            //                    .Where(car => filter.Settings.Esp == true ? filter.Settings.Esp == car.Settings.Esp : true)
            //                    .Where(car => filter.Settings.ParkSensors == true ? filter.Settings.ParkSensors == car.Settings.ParkSensors : true)
            //                    .Where(car => filter.Settings.Camera == true ? filter.Settings.Camera == car.Settings.Camera : true)
            //                    .Where(car => filter.Settings.Cruiz == true ? filter.Settings.Cruiz == car.Settings.Cruiz : true)
            //                    .Where(car => filter.Settings.AirCondition == true ? filter.Settings.AirCondition == car.Settings.AirCondition : true)
            //                    .Where(car => filter.Settings.ClimatControl == true ? filter.Settings.ClimatControl == car.Settings.ClimatControl : true)
            //                    .Where(car => filter.Settings.Navigation == true ? filter.Settings.Navigation == car.Settings.Navigation : true)
            //                    .Where(car => filter.Settings.Bluetooth == true ? filter.Settings.Bluetooth == car.Settings.Bluetooth : true);

            return result;
        }
    }
}
