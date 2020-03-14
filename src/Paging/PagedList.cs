using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uzzal.Paging
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        private PagedList(List<T> items, int count, int pageIndex, int itemsPerPage)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)itemsPerPage);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get => PageIndex > 1;
        }

        public bool HasNextPage
        {
            get => PageIndex < TotalPages;
        }

        public static PagedList<T> Build(ICollection<T> source, int pageIndex, int itemsPerPage)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            return new PagedList<T>(items, count, pageIndex, itemsPerPage);
        }

        public static async Task<PagedList<T>> BuildAsync(IQueryable<T> source, int pageIndex, int itemsPerPage)
        {
            var count = await source.CountAsync().ConfigureAwait(false);
            var items = await source.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync().ConfigureAwait(false);
            return new PagedList<T>(items, count, pageIndex, itemsPerPage);
        }

        public PagingContext GetContext(int span = 5)
        {
            return new PagingContext
            {
                PageIndex = PageIndex,
                TotalPages = TotalPages,
                HasNext = HasNextPage,
                HasPrevious = HasPreviousPage,
                PageLinks = new PageLinks(TotalPages, PageIndex, span)
            };
        }
    }

    public class PagingContext
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public PageLinks PageLinks { get; set; }
    }

    public static class Extensions
    {
        public static PagedList<T> ToPagedList<T>(this ICollection<T> source, int pageIndex, int itemsPerPage) 
            => PagedList<T>.Build(source, pageIndex, itemsPerPage);

        public static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int itemsPerPage)
            => PagedList<T>.BuildAsync(source, pageIndex, itemsPerPage);
    }
}
