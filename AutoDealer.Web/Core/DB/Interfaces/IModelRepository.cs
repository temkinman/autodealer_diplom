using AutoDealer.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IModelRepository : IBaseRepository<Model>
    {
        IQueryable<Model> Models { get; }
    }
}
