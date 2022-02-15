using Microsoft.EntityFrameworkCore;
using PRSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSLibrary.Controllers {
    public class RequestlinesController {

        private readonly PRSDbContext _context;

        public RequestlinesController(PRSDbContext context) {
            _context = context;
        }

        public IEnumerable<RequestLine> GetAll() {
            return _context.RequestLines
                                .Include(x => x.Product) // syncs foreign key inside RequestLine to Product
                                .Include(x => x.Request) // syncs foreign key inside RequestLine to Request
                                .ToList();
        }
        public RequestLine GetByPk(int id) {
            return _context.RequestLines
                                .Include(x => x.Product)
                                .Include(x => x.Request)
                                .SingleOrDefault(x => x.Id == id);
        }

        public RequestLine Create(RequestLine requestline) {
            _context.RequestLines.Add(requestline);
            _context.SaveChanges();
            return requestline;
        }

        public void Change(RequestLine requestline) {
            _context.SaveChanges();
        }

        public void Remove(int id) {
            var requestline = _context.RequestLines.Find(id);
            if (requestline is not null) {
                _context.RequestLines.Remove(requestline);
                _context.SaveChanges();
            }
        }
    }
}
