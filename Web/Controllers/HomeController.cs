using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class HomeController : Controller
{
    [HttpGet]
    [Authorize(Policy = PolicyName.Customer, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Index()
    {
        return View("Index");
    }
}
