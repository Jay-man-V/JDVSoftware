//-----------------------------------------------------------------------
// <copyright file="CustomAuthoriseAttribute.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;

using Foundation.Interfaces;

namespace Foundation.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomAuthoriseAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IPrincipal principal = actionContext.ControllerContext.RequestContext.Principal;
            String logonName = principal.Identity.Name;
            Boolean hasBasics = principal.Identity != null && principal.Identity.IsAuthenticated;
            AppId applicationId = Core.Core.Instance.ApplicationId;

            base.OnAuthorization(actionContext);
        }
    }
}
