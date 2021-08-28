using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.Transformers.Interfaces
{
    public interface ITransformer
    {
        TDestination Transform<TSource, TDestination>(TSource type);
    }
}
