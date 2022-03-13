namespace WebEminari.Web.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected string GetUserId() => this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        protected string GetUserName() => this.User.Identity?.Name;
    }
}
