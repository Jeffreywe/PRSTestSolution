using PRSLibrary.Controllers;
using PRSLibrary.Models;
using System;
using System.Linq;

namespace TestPRSLibrary {
    class Program {
        static void Main(string[] args) {

            var context = new PRSDbContext(); // creates context variable from PRSDbContext

            var userCtrl = new UsersController(context); //takes our context from userscontroller class and puts it into userctrl

            //creates a new user
/*            var newUser = new User() {
                Id = 0, Username = "xx", Password = "xx",
                Firstname = "User", Lastname = "XX",
                Phone = "211", Email = "xx@user.com",
                IsReviewer = false, IsAdmin = false
            };
            userCtrl.Create(newUser); // calls our Create method in our controller
*/
            //reads users in the list
            var user3 = userCtrl.GetByPk(3);
            if(user3 is null) {
                Console.WriteLine("User not found!");
            } else {
                Console.WriteLine($"User3: {user3.Firstname} {user3.Lastname}");
            }
            var user33 = userCtrl.GetByPk(33);
            if (user33 is null) {
                Console.WriteLine("User not found!");
            } else {
                Console.WriteLine($"User33: {user33.Firstname} {user33.Lastname}");
            }

            var users = userCtrl.GetAll(); // calls the GetAll method in our controller
            
            foreach(var user in users) {
                Console.WriteLine($"{user.Firstname} {user.Lastname}" );
            }
        }
    }
}
