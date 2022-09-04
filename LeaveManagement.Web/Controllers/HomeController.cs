using System.Diagnostics;
using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Web.Controllers
{
   public class HomeController : Controller
   {
      private readonly ILogger<HomeController> _logger;

      public HomeController(ILogger<HomeController> logger) => _logger = logger;

      public IActionResult Index() => View();

      [Authorize]
      public IActionResult Privacy() => View();

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
         var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

         if (exceptionHandlerPathFeature != null)
         {
            Exception ex = exceptionHandlerPathFeature.Error;
            _logger.LogError(ex, $"Error encountered by user: {User?.Identity?.Name} | Request Id: {requestId}");
         }
         return View(new ErrorViewModel { RequestId = requestId });
      }
   }
}