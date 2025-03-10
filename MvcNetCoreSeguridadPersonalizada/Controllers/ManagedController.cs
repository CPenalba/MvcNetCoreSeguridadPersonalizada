using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MvcNetCoreSeguridadPersonalizada.Controllers
{
    public class ManagedController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username.ToLower() == "admin" && password.ToLower() == "admin")
            {
                //AUNQUE NOSOTROS NO LO VEAMOS, SE GENERA UNA COOKIE CIFRADA QUE ES PARA SABER SI EL USUARIO 
                //ESTA VALIDADO EN SESSION CON ESTE EQUIPO
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                //UN CLAIM ES INFORMACION DEL USUARIO
                Claim claimsUserName = new Claim(ClaimTypes.Name, username);
                Claim claimRole = new Claim(ClaimTypes.Role, "USUARIO");
                identity.AddClaim(claimsUserName);
                identity.AddClaim(claimRole);
                //CREAMOS EL USUARIO PRINCIPAL CON ESTA IDENTIDAD
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(15)
                    });
                //LLEVAMOS AL USUARIO A PERFIL UNA VEZ QUE SE HA VALIDADO
                return RedirectToAction("Perfil", "Usuarios");
            }
            else
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();

            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
