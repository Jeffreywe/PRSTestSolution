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
        //STATUS METHODS

        //returns collection of requests like get all
        public IEnumerable<Request> GetRequestsInReview(int userId) {
                var requests = _context.Requests
                                            .Where(x => x.Status == "REVIEW"
                                                && x.UserId != userId)
                                            .ToList();
            return requests;
        }

        //sets status of a reques to REJECTED
        public void SetRejected(Request request) {
            request.Status = "REJECTED";
            Change(request);
        }
        //sets status of a Request to Approved
        public void SetApproved(Request request) {
            request.Status = "APPROVED";
            Change(request); 
        }
        //sets status of a Request to Approved, or Review
        public void SetReview(Request request) {
            if(request.Total <= 50) {
                request.Status = "APPROVED";
            } else {
                request.Status = "REVIEW";
            }
            Change(request); // calls the add to database method to reduce writing code
        }

        //basic methods, basic minimum 5
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
