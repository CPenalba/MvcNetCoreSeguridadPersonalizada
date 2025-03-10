using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcNetCoreSeguridadPersonalizada.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter
    {

        //ESTE METODO ES EL QUE NOS PERMITIRA IMPEDIR EL ACCESO A LOS CONTROLLERS/IACTIONRESULT QUE ESTEN DECORADOS CON ESTA CLASE
        //EL FILTER ES EL ENCARGADO DE INTERCEPTAR LAS PETICIONES Y DECIDIR QUE HACER
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //EL USUARIO QUE SE HA VALIDADO EN NUETSRA APP ESTARA DENTRO DE CONTEXT Y EN UNA PROPIEDAD LLAMADA USER
            //UN USUARIO ESTA COMPUESTO POR DOS CARACTERISTICAS:
            //1) IDENTITY: EL NOMBRE DEL USUARIO Y SI ESTA ACTIVO
            //2) PRINCIPAL: SIRVE PARA PREGUNTAR SI EL USER ESTA EN ALGUN ROLE
            var user = context.HttpContext.User;
            //PREGUNTAMOS SI EL USUARIO ESTA AUTENTICADO
            if (user.Identity.IsAuthenticated == false)
            {
                //SI EL USER NO SE HA VALIDADO LO DEBEMOS LLEVAR A NUESTRA PAGINA DE LOGIN
                //CREAMOS UNA RUTA PARA IR A LA DIRECCION DE LOGIN
                RouteValueDictionary rutaLogin = new RouteValueDictionary(new { controller = "Managed", action = "Login" });
                //DIRECCIONAMOS EL FILTRO HACIA LA RUTA DE LOGIN
                context.Result = new RedirectToRouteResult(rutaLogin);
            }
            throw new NotImplementedException();
        }
    }
}
