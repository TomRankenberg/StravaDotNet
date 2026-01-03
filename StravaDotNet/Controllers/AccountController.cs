using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace StravaDotNet.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            var props = new AuthenticationProperties { RedirectUri = returnUrl };
            // "Auth0" is the scheme name used by Auth0.AspNetCore.Authentication
            return Challenge(props, "Auth0");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var callbackUrl = Url.Content("~/");
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties { RedirectUri = callbackUrl });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(callbackUrl);
        }
    }
}