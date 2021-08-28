using GitApp.Application.Transformers.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.Transformers
{
    public class GenericTransformer: ITransformer
    {
        public TDestination Transform<TSource, TDestination>(TSource type)
        {
            return type.Adapt<TDestination>();
        }
    }
}
