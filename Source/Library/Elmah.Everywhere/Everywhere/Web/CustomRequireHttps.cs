﻿using System.Web.Mvc;


namespace Elmah.Everywhere.Web
{
    public sealed class CustomRequireHttpsAttribute : RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.Url != null && filterContext.HttpContext.Request.Url.Host.Contains("localhost") == false)
            {
                base.HandleNonHttpsRequest(filterContext);
            }
        }
    }
}
