using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
