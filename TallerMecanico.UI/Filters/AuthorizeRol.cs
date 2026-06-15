using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace TallerMecanico.UI.Filters
{
    public class AuthorizeRol : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public AuthorizeRol(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string rol = context.HttpContext.Session.GetString("Rol");

            if (rol == null || !Array.Exists(_roles, r => r == rol))
            {
                context.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}