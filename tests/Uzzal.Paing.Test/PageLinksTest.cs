using System;
using System.Collections.Generic;
using Uzzal.Paging;
using Xunit;
using Xunit.Abstractions;

namespace Uzzal.Paing.Test
{
    public class PageLinksTest
    {
        private readonly ITestOutputHelper output;

        public PageLinksTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        
        [Theory]
        [MemberData(nameof(PageLinksData))]
        public void TestPageLinks(int total, int current, int span, HashSet<int> expected)
        {
            var pagelinks = new PageLinks(total, current, span);
            var actual = pagelinks.GetSpanLinks();

            foreach(var i in actual)
            {
                output.WriteLine(i.ToString());
            }

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(RangeData))]
        public void TestGetRange(int total, int current, int span, (int, int) expected)
        {
            var pagelinks = new PageLinks(total, current, span);
            var actual = pagelinks.GetRange();
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> PageLinksData =>
        new List<object[]>
        {
            new object[] { 10, 1, 5, new HashSet<int> { 1, 2, 3, 4, 5, 10} },
            new object[] { 10, 10, 5, new HashSet<int> { 1, 6, 7, 8, 9, 10} },
            new object[] { 10, 5, 5, new HashSet<int> { 1, 3, 4, 5, 6, 7, 10} },
            new object[] { 3, 1, 5, new HashSet<int> { 1, 2, 3} },
            new object[] { 6, 1, 5, new HashSet<int> { 1, 2, 3, 4, 5, 6} },
            new object[] { 14, 1, 5, new HashSet<int> { 1, 2, 3, 4, 5, 14} },
        };

        public static IEnumerable<object[]> RangeData =>
        new List<object[]>
        {
            new object[] { 10, 1, 5, (1, 5) },
            new object[] { 10, 5, 5, (3, 7) },
            new object[] { 10, 7, 5, (5, 9) },
            new object[] { 10, 9, 5, (6, 10) },
            new object[] { 5, 1, 5, (1, 5) },
            new object[] { 5, 4, 3, (3, 5) },
            new object[] { 14, 1, 5, (1, 5) },
        };
    }
}
