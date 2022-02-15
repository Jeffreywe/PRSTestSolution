using PRSLibrary.Controllers;
using PRSLibrary.Models;
using System;
using System.Linq;

namespace TestPRSLibrary {
    class Program {

        static void Print(Product product) { // useful technique, create methods to do the work for you instead of having to write the code out over and over
            Console.WriteLine($"{product.Id,-5} {product.PartNbr,-12} {product.Name,-12} {product.Price,10:c} {product.Vendor.Name,-15}"); 
        }

        static void Main(string[] args) {

            var context = new PRSDbContext(); // creates context variable from PRSDbContext

            var prodCtrl = new ProductsController(context);

            var products = prodCtrl.GetAll();

            foreach(var p in products) {
                Print(p);
            }

            var product = prodCtrl.GetByPk(2);

            if(product is not null) {
                Print(product); // takes the variable and passes it into our Print method
            }














            //USERS
            //            var userCtrl = new UsersController(context); //takes our context from userscontroller class and puts it into userctrl

            //            //creates a new user
            ///*            var newUser = new User() {
            //                Id = 0, Username = "xx", Password = "xx",
            //                Firstname = "User", Lastname = "XX",
            //                Phone = "211", Email = "xx@user.com",
            //                IsReviewer = false, IsAdmin = false
            //            };
            //            userCtrl.Create(newUser); // calls our Create method in our controller
            //*/

            //            //reads users in the list
            //            var user3 = userCtrl.GetByPk(3);
            //            if(user3 is null) {
            //                Console.WriteLine("User not found!");
            //            } else {
            //                Console.WriteLine($"User3: {user3.Firstname} {user3.Lastname}");
            //            }
            //            //updates a user
            //            //user3.Lastname = "User3";
            //            //userCtrl.Change(user3);


            //            //reads users in lists
            //            var user33 = userCtrl.GetByPk(33);
            //            if (user33 is null) {
            //                Console.WriteLine("User not found!");
            //            } else {
            //                Console.WriteLine($"User33: {user33.Firstname} {user33.Lastname}");
            //            }

            //            //removes a user, Id has to be valid
            //            userCtrl.Remove(4);

            //            var users = userCtrl.GetAll(); // calls the GetAll method in our controller

            //            foreach(var user in users) {
            //                Console.WriteLine($"{user.Id} {user.Firstname} {user.Lastname}" );
            //            }
        }
    }
}
