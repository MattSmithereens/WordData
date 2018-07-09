using Microsoft.AspNetCore.Mvc;
using WorldDataProject.Models;

namespace WorldDataProject.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
