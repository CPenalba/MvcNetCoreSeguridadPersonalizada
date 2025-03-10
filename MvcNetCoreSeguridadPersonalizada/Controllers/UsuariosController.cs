using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSeguridadPersonalizada.Filters;

namespace MvcNetCoreSeguridadPersonalizada.Controllers
{
    public class UsuariosController : Controller
    {
        [AuthorizeUsers]
        public IActionResult Perfil()
        {
            return View();
        }
    }
}
