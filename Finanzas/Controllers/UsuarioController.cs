using Microsoft.AspNetCore.Mvc;

namespace Finanzas.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
