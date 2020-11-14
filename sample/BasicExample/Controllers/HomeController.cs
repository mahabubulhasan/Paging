using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BasicExample.Models;
using Uzzal.Paging;

#nullable enable

namespace BasicExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            var list = GetExampleList();
            var pagedList1 = PagedList<string>.Build(list, page ?? 1, 10);
            var pagedList2 = list.ToPagedList<string>(page ?? 1, 10);

            return View(pagedList2);
        }

        private List<string> GetExampleList()
        {
            var list = new List<string>();
            for(int i=0; i< 14; i++)
            {
                list.Add($"Item: {i}");
            }
            return list;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
