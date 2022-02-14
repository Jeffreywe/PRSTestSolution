using PRSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSLibrary.Controllers {
    /// <summary>
    /// controller is a class that contains methods
    /// 5 tables, 5 controllers
    /// controllers will be in our capstone project, any client that talks to our database wil ltalk to our controller
    /// controller allows us to add business rules, 
    /// user changes item values, controller keeps the total accurate for the user
    /// use another models folder called controllers for controllers to keep it organized
    /// when you create controllers, you name the database and then the name controller
    /// contexts in the controllers need to be private
    /// toread,
    /// insert,
    /// update,
    /// delete,
    /// minimal methods built inside controllers
    /// </summary>
    public class UsersController {

        private readonly PRSDbContext _context; // readonly means the only thing that can change the value of context is inside the constructor

        public UsersController(PRSDbContext context) {
            this._context = context; // takes this variable and puts it into _context
        }

        // the 2 read methods
        public IEnumerable<User> GetAll() {// means we can pass a list or an array, general interface method, means we can take things and return what we want, flexibility
            return _context.Users.ToList(); // converts to list
        }
        public User GetByPk(int id) {
            return _context.Users.Find(id);
        }

        // maintenance functions
        //insert
        public User Create(User user) {
            if(user is null) {
                throw new ArgumentNullException("user");
            }
            if(user.Id != 0) {
                throw new ArgumentException("User.Id must be zero!");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
