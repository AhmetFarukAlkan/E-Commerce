using WP_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using WP_Temp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {

        WpNewContext db = new WpNewContext();


        //[HttpGet]
        [Route("/login")]
        public IActionResult login()
        {
            return View();
        }

        public IActionResult katalog()
        {
            //ViewData["Message"] = "Your application description page.";
            return View();
        }


        public IActionResult orders()
        {
            //ViewData["Message"] = "Your application description page.";
            return View();
        }



        //WpNewContext db = new WpNewContext();

        [HttpGet]
        [Route("product/catalog")]
        [Route("product/catalog/filter/category={category}")]
        public async Task<IActionResult> catalog(string category)
        {
            //var values = db.Products.ToList();
            //return View(values);
            using (var context = new WpNewContext()) // YourDbContext, projenizdeki DbContext türevidir
            {
                var model = await context.Products.Include(x => x.Images).ToListAsync();
                
                if (!string.IsNullOrEmpty(category))
                {
                    if (category.Equals("laptop")) category = "Laptop";
                    else if (category.Equals("TV")) category = "Televizyon";
                    else if (category.Equals("smart-phone")) category = "Smart Phone";

                    Console.WriteLine(category);

                    return View(model.FindAll(p => p.Kategori.Equals(category)));
                }
                


                return View(model);
            }

        }


        [HttpGet]
        [Route("Product/Get/{id}")]
        public async Task<IActionResult> product_page(int id)
        {

            //var values = db.Products.ToList();
            //return View(values);
            using (var context = new WpNewContext()) // YourDbContext, projenizdeki DbContext türevidir
            {
                var model = await context.Products.Include(x => x.Images).ToListAsync();

                //var product = db.Products.Find(id);


                return View(model.Find(p => p.Id == id));
            }

            //ViewData["Message"] = "Your application description page.";
            //return View();
        }


        [Route("Chart/user-email={email}")]
        public async Task<IActionResult> chartAsync(string email)
        {

            // var model = db.ShoppingCarts
            //.Include(s => s.User) // İlişkili User verisini de çekmek için
            //.Where(s => s.Id == db.Users.FirstOrDefault(u => u.EMail == (email)).Id)
            //.ToList();


            //var model = db.ShoppingCarts.Include(x => x.ProductId).Where(s => s.UserId == db.Users.FirstOrDefault(u => u.EMail == (email)).Id).ToListAsync();


            //var model = db.Products
            //    .Include(x => x.ShoppingCarts.Where(s => s.UserId == db.Users.FirstOrDefault(u => u.EMail.Equals(email)).Id))
            //    .Include(z => z.Images)
            //    .ToList();


            var model = await db.ShoppingCarts
                .Include(s => s.Product)
                .ThenInclude(p => p.Images)
                .Where(s => s.UserId == db.Users.FirstOrDefault(u => u.EMail == email).Id)
                .ToListAsync();


            //ViewData["Message"] = "Your application description page.";
            return View(model);
        }

        [HttpGet]
        [Route("Product/Get/addChart/Produc={id}&user-email={email}")]
        public ActionResult AddChart(int id, string email)
        {
            // Save the login information to the database

            Console.WriteLine("AddChart");

            SaveProductInfoToDatabase(id, email);


            //Console.WriteLine(name);
            //Console.WriteLine(email);
            //Console.WriteLine(password);

            // Redirect or perform any other actions

            return Redirect("/Product/Get/" + id);
            //return RedirectToAction("Index", "Home");
        }

        private void SaveProductInfoToDatabase(int id, string email)
        {
            // Code to save the login information to the database using EF Core

            if(null == db.ShoppingCarts.FirstOrDefault(u => u.UserId == db.Users.FirstOrDefault(u => u.EMail == email).Id && u.ProductId == id)){
                var shoppingCart = new ShoppingCart
                {
                    ProductId = id,
                    UserId = db.Users.FirstOrDefault(u => u.EMail == email).Id

                };

                db.ShoppingCarts.Add(shoppingCart);
                db.SaveChanges();
            }
        }



        ///deleteChart/Produc=1&user-email=ahmetfarukalkan08 @gmail.com

        [HttpGet]
        [Route("deleteChart/Produc={id}&user-email={email}")]
        public ActionResult deleteChart(int id, string email)
        {
            // Save the login information to the database

            Console.WriteLine("DeleteChart");
            Console.WriteLine(id);
            Console.WriteLine(email);



            var shoppingCart = db.ShoppingCarts.FirstOrDefault(u => u.UserId == db.Users.FirstOrDefault(u => u.EMail == email).Id && u.ProductId == id);

            if (shoppingCart != null)
            {
                // Ürün veritabanında bulunursa, silme işlemini gerçekleştirin
                db.ShoppingCarts.Remove(shoppingCart);
                db.SaveChanges();
            }


            //Console.WriteLine(name);
            //Console.WriteLine(email);
            //Console.WriteLine(password);

            // Redirect or perform any other actions

            return Redirect("/Chart/user-email="+email);
            //return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("confirmOrders/user-email={email}")]
        public ActionResult confirmOrders(string email)
        {
            // Ürünleri veritabanından silmek için tüm kayıtları alın
            var shoppingCarts = db.ShoppingCarts.Where(u => u.UserId == db.Users.FirstOrDefault(u => u.EMail == email).Id).ToList();

            if (shoppingCarts.Count > 0)
            {
                // Tüm kayıtları sil
                db.ShoppingCarts.RemoveRange(shoppingCarts);
                db.SaveChanges();
            }

            // İşlemden sonra yönlendirme yapabilir veya başka eylemler gerçekleştirebilirsiniz
            return Redirect("/Chart/user-email=" + email);
            //return RedirectToAction("Index", "Home");
        }



    }
}
