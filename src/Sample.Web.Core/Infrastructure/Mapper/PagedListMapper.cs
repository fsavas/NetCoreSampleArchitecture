using AutoMapper;
using Sample.Core;
using System.Collections.Generic;

namespace Sample.Web.Infrastructure.Mapper
{
    public class PagedListMapper<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);

            return new PagedList<TDestination>(collection, source.PageIndex, source.PageSize, source.TotalCount);
        }
    }
}