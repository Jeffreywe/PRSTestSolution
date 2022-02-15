using Microsoft.EntityFrameworkCore;
using PRSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSLibrary.Controllers {
    public class RequestsController {

        private readonly PRSDbContext _context;

        public RequestsController(PRSDbContext context) {
            _context = context;
        }

        public IEnumerable<Request> GetAll() {
            return _context.Requests
                                .Include(x => x.User)
                                .ToList();
        }
        public Request GetByPk(int id) {
            return _context.Requests
                                .Include(x => x.User)
                                .SingleOrDefault(x => x.UserId == id);
        }

        public Request Create(Request request) {
            if(request is null) {
                throw new ArgumentNullException("Request");
            }
            if (request.Id != 0) {
                throw new ArgumentException("Request.Id must be zero!");
            }
            _context.Requests.Add(request);
            _context.SaveChanges();
            return request;
        }

        public void Change(Request user) {
            _context.SaveChanges();
        }

        public void Remove(int id) {
            var request = _context.Requests.Find(id);
            if(request is null) {
                throw new Exception("Request not found!");
            }
            _context.Requests.Remove(request);
            _context.SaveChanges();
        }
    }
}
