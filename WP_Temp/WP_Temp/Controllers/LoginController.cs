using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WP_Temp.Models;
using Microsoft.AspNetCore.Http;


namespace WP_Temp.Controllers
{
    public class LoginController : Controller
    {
        WpNewContext db = new WpNewContext();

       

        //public LoginController(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        [HttpPost]
        [Route("Register")]
        public ActionResult Register(string name, string email, string password)
        {
            // Save the login information to the database

            if(db.Users.FirstOrDefault(u => u.EMail == (email)) != null)
            {
                ViewBag.ErrorMessage = "A user with this email number already exists";

                return View("~/Views/Product/Login.cshtml"); // /login sayfasına yönlendirme yapılıyor
            }

            SaveRegistrationInfoToDatabase(name, email, password);


            //Console.WriteLine(name);
            //Console.WriteLine(email);
            //Console.WriteLine(password);

            // Redirect or perform any other actions
            return RedirectToAction("Index", "Home");
        }

        private void SaveRegistrationInfoToDatabase(string name, string email, string password)
        {
            // Code to save the login information to the database using EF Core

            var user = new User
            {
                NameSurname = name,
                Password = password,
                EMail = email
            };

            db.Users.Add(user);
            db.SaveChanges();
            
        }

        [HttpPost]
        [Route("login")]
        public ActionResult LoginUser(string email, string password)
        {
            var user = ControlInfoToDatabase(email, password);

            // Save the login information to the database
            if (user == null)
            {
                //Console.WriteLine("/login");


                // ModelState geçerli değilse, hata mesajlarını elde edin ve JavaScript kodu olarak döndürün
                
                ViewBag.ErrorMessage = "There is no such user registered!";

                return View("~/Views/Product/Login.cshtml"); // /login sayfasına yönlendirme yapılıyor
                //RedirectToRoute("/login");
                //View("/login");

            }



            //Session["Username"] = user.Password;

            Console.WriteLine(user.Id);

            var options = new CookieOptions
            {
                // Set other options like expiration, domain, path, etc., as needed
                Expires = DateTime.Now.AddDays(7) // Cookie expires in 7 days
            };

            // Store the user's email in a cookie
            Response.Cookies.Append("UserEmail", user.EMail, options);
            Response.Cookies.Append("UserName", user.NameSurname, options);

            //string NameSurname = user.NameSurname;
            //string EMail = user.EMail;

            //httpcontext.session.setstring("userıd", user.ıd.tostring());
            //httpcontext.session.setstring("username", user.namesurname);
            //httpcontext.session.setstring("useremail", user.email);

            return RedirectToAction("Index", "Home");


            // Redirect or perform any other actions
        }

        private User ControlInfoToDatabase(string email, string password)
        {

            //var user = db.Users.FirstOrDefault(u => u.EMail.Equals(email) && u.Password.Equals(password));

           return db.Users.FirstOrDefault(u => u.EMail==(email) && u.Password == (password));

        }


        //[HttpPost]
        [Route("logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("UserEmail");

            // Redirect to the desired page
            return RedirectToAction("Index", "Home");
        }


    }
}
