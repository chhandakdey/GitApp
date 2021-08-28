using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitApp.Mvc.Transformers.Interfaces
{
    /// <summary>
    /// A Generic Transformer for Transforming DTOs to ViewModels
    /// </summary>
    public interface ITransformer
    {
        /// <summary>
        /// A Generic Transformer for Transforming DTOs to ViewModels
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        TDestination Transform<TSource, TDestination>(TSource type);
    }
}
