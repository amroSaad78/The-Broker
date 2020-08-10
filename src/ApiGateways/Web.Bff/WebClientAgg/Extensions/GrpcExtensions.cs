using System;
using System.Collections.Generic;

namespace WebClientAgg.Extensions
{
    public static class GrpcExtensions
    {
        public static IEnumerable<TResult> MapToModel<TSource, TResult>(this IEnumerable<TSource> sourceList, Func<TSource, TResult> maper)
        {
            foreach (TSource source in sourceList)
            {
                MapTryResult<TSource, TResult> Value;                
                Value = new MapTryResult<TSource, TResult>(source, maper(source));
                yield return Value.Result;
            }
        }
    }

    public class MapTryResult<TSource, TResult>
    {
        internal MapTryResult(TSource source, TResult result)
        {
            Source = source;
            Result = result;            
        }

        public TSource Source { get; private set; }
        public TResult Result { get; private set; }        
    }
}
