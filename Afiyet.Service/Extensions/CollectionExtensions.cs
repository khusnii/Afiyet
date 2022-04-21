using Afiyet.Domain.Configurations;
using System.Collections.Generic;
using System.Linq;

namespace Afiyet.Service.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> ToPagedList<T>(this IEnumerable<T> source, PaginationParams @params)
        {
            return @params.PageIndex >= 0 && @params.PageSize > 0 ?
                source.Skip(@params.PageSize * (@params.PageIndex - 1)).Take(@params.PageSize) : source;
        }
    }
}
