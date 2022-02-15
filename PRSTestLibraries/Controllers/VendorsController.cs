using PRSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSLibrary.Controllers {
    public class VendorsController { // try using the unit testing program to test database
        
        private readonly PRSDbContext _context; 

        public VendorsController(PRSDbContext context) {
            this._context = context; 
        }

        public IEnumerable<Vendor> GetAll() {
            return _context.Vendors.ToList(); 
        }
        public Vendor GetByPk(int id) {
            return _context.Vendors.Find(id);
        }

        public Vendor Create(Vendor vendor) {
            _context.Vendors.Add(vendor);
            _context.SaveChanges();
            return vendor;
        }
        
        public void Change(Vendor vendor) {
            _context.SaveChanges();
        }

        public void Remove(int id) {
            var vendor = _context.Vendors.Find(id);
            if (vendor is not null) {  
                _context.Vendors.Remove(vendor); 
                _context.SaveChanges();                
            }
        }
    }
}

