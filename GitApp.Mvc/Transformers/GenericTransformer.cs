using GitApp.Mvc.Transformers.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitApp.Mvc.Transformers
{
    /// <summary>
    /// A Generic Transformer for Transforming DTOs to ViewModels
    /// </summary>
    public class GenericTransformer : ITransformer
    {
        /// <summary>
        /// A Generic Transformer for Transforming DTOs to ViewModels
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public TDestination Transform<TSource, TDestination>(TSource type)
        {
            return type.Adapt<TDestination>();
        }
    }
}
