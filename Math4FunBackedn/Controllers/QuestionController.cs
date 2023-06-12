using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
