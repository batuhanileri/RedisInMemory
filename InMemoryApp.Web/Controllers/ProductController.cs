using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //1.yol
            if(String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            {
                _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }
            //2.yol
            if(!_memoryCache.TryGetValue("zaman",out string zamancache))
            {
               _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }


            return View();
        }

        public IActionResult Show()
        {

            _memoryCache.Remove("zaman");

            ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
