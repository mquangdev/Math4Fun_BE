using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    public class ChapterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
