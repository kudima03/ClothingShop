using Infrastructure.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(Policy = PolicyName.Customer)]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View("Index");
    }
}
