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
        [MemberData(nameof(Data))]
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

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 10, 1, 5, new HashSet<int> { 1, 2, 3, 10} },
            new object[] { 10, 10, 5, new HashSet<int> { 1, 8, 9, 10} },
            new object[] { 10, 5, 5, new HashSet<int> { 1, 3, 4, 5, 6, 7, 10} },
            new object[] { 3, 1, 5, new HashSet<int> { 1, 2, 3} },
            new object[] { 6, 1, 5, new HashSet<int> { 1, 2, 3, 6} },
        };
    }
}
