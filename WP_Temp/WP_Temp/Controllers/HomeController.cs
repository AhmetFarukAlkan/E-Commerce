using WP_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using WP_Temp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        WpNewContext db = new WpNewContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public Image GetFirstImageByProductId(int productId)
        //{
        //    using (var dbContext = new EcommerceContext())
        //    {
        //        Image firstImage = dbContext.Images
        //            .FirstOrDefault(image => image.ProductId == productId);

        //        return firstImage;
        //    }
        //}
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ProductDetails(int id)
        { 
            Product product= db.Products.Where(x=>x.Id==id).Include(x=>x.Images).FirstOrDefault();
            
            return View(product);
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }


        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/


        //public IConfiguration configuration { get; set; }   

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddAuthentication(option =>
        //    {

        //        option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        //    })
        //        .AddCookie(options => {
        //            options.LoginPath = "/accoun/google-login";
        //        })
        //        .AddGoogle(options =>
        //        {
        //            options.ClientId = "793483917668-6t9c2m892kdl7t1t9m26lr7hb8qodv7t.apps.googleusercontent.com";
        //            options.ClientSecret = "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";
        //        })
        //         ;

        //    services.AddControllersWithViews();
        //}



    }
}