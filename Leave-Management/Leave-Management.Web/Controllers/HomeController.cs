using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Leave_Management.Web.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Leave_Management.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if(exceptionHandlerPathFeature != null)
        {
            Exception exception = exceptionHandlerPathFeature.Error;
            _logger.LogError(exception, $"Error Encountered by User: {this.User?.Identity?.Name} | RequestId: {requestId}");
        }
        return View(new ErrorViewModel { RequestId = requestId });
    }
}
