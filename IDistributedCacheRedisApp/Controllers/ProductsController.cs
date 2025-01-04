using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheRedisApp.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributedCache;

        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async IActionResult Index()
        {
            DistributedCacheEntryOptions options = new();

            options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            _distributedCache.SetString("name", "batu",options);
            await _distributedCache.SetStringAsync("surname", "ileri");
            return View();
        }

        public IActionResult Show()
        {
            string name = _distributedCache.GetString("name");

            ViewBag.Name = name;
            return View();
        }

        public IActionResult Remove()
        {
            _distributedCache?.Remove("name");
            return View();
        }
    }
}
