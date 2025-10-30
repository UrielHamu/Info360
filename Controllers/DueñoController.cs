using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Info360.Models;

namespace Info360.Controllers;

public class DueñoController : Controller
{
    private readonly ILogger<DueñoController> _logger;

    public IActionResult HomeDueño(){
        return View();
    }
}