namespace BugTracker.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    //[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    //public class CustomAuthorize : AuthorizeAttribute
    //{
    //    private string[] UserProfilesRequired { get; set; }

    //    public CustomAuthorize(params object[] userProfilesRequired)
    //    {
    //        if (userProfilesRequired.Any(p => p.GetType().BaseType != typeof(Enum)))
    //            throw new ArgumentException("userProfilesRequired");

    //        this.UserProfilesRequired = userProfilesRequired.Select(p => Enum.GetName(p.GetType(), p)).ToArray();
    //    }

    //    public override void OnAuthorization(AuthorizationContext context)
    //    {
    //        bool authorized = false;

    //        foreach (var role in this.UserProfilesRequired)
    //            if (HttpContext.Current.User.IsInRole(role))
    //            {
    //                authorized = true;
    //                break;
    //            }

    //        if (!authorized)
    //        {
    //            var url = new UrlHelper(context.RequestContext);
    //            var logonUrl = url.Action("Http", "Error", new { Id = 401, Area = "" });
    //            context.Result = new RedirectResult(logonUrl);

    //            return;
    //        }
    //    }
    //}
}
