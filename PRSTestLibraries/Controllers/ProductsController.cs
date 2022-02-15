using Microsoft.EntityFrameworkCore;
using PRSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSLibrary.Controllers {
    public class ProductsController {

        private readonly PRSDbContext _context;

        public ProductsController(PRSDbContext context) {
            _context = context;
        }
        
        public IEnumerable<Product> GetAll() { // this change only applies for the read operations to pull vendor name from vendors class
            return _context.Products.Include(x => x.Vendor).ToList(); // Vendor from the instance that creates the FK in product class
        }

        public Product GetByPk(int id) {
            return _context.Products.Include(x => x.Vendor)
                                .SingleOrDefault(x => x.Id == id); // another way to get the same result as .Find, but useable with .Include
        }

        public Product Create(Product product) {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product; 
        }

        public void Change(Product product) {
            _context.SaveChanges();
        }

        public void Remove(int id) {
            var product = _context.Products.Find(id);
            if(product is not null) {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
