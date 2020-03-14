using System;
using System.Collections.Generic;

namespace Uzzal.Paging
{
    public class PageLinks
    {
        public int Current { get; private set; }
        public int Span { get; private set; }
        public int PreSpan { get; private set; }
        public int PostSpan { get; private set; }
        public int Total { get; set; }

        public PageLinks(int total, int current, int span)
        {
            Total = total;
            Current = current;
            Span = span;
            PreSpan = (int)Math.Ceiling((double)span / 2);
            PostSpan = span - PreSpan;
        }

        public HashSet<int> GetSpanLinks()
        {
            HashSet<int> spanLinks = new HashSet<int>();
            if (Total == 0)
            {
                return spanLinks;
            }

            var (start, limit) = GetRange();

            spanLinks.Add(1);
            for (int i = start; i <= limit; i++)
            {
                spanLinks.Add(i);
            }
            spanLinks.Add(Total);
            return spanLinks;
        }

        public (int, int) GetRange()
        {
            int start = Current - PreSpan + 1;
            int limit = Current + PostSpan;

            if (start < 1)
            {
                start = 1;
            }

            if (limit > Total)
            {
                limit = Total;
            }

            // special first (1, 2, 3, 4, 5...10)
            if (start == 1 && Total >= Span)
            {
                limit = Span;
            }

            // special last (1...6, 7, 8, 9, 10)
            if (Current > (Total - PostSpan))
            {
                start = (Total - Span) + 1;
            }

            return (start, limit);
        }
    }
}
