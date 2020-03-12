using System;
using System.Collections.Generic;

namespace Paging
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
            spanLinks.Add(1);
            for (int i = start; i <= limit; i++)
            {
                spanLinks.Add(i);
            }
            spanLinks.Add(Total);
            return spanLinks;
        }
    }
}
