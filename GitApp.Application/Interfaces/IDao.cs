using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.Interfaces
{
    /// <summary>
    /// DAO Interface. Concrete implementation of DAO is available in Infra layer
    /// </summary>
    /// <typeparam name="In"></typeparam>
    /// <typeparam name="Out"></typeparam>
    public interface IDao<In,Out>
    {
        Task<Out> GetAllAsync(In model);
    }
}
