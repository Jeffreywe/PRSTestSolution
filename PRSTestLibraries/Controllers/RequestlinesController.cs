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
        /// <summary>
        /// reads the request by the requestid we insert in the parameter and finding it using .find, and then we create the syntax to put into the request.Total var
        /// from r1 is fred, in _con.Rlines is returning all lines, then we are joining to products since PK and FK on clause joins together
        /// where sets condition that brings back rows where requests thats passed matches the requestid put into parameter of calculate method
        /// select returns some of the columns not all of them by identifying with new statement, take rl.Quatity and p.Price and returns inside new column named linetotal
        /// wrapping that in parans lets us use the .Sum method to sum up the line totals we mathed out in the query syntax
        /// the sum gets placed into the request total in the beginning with =, and then save changes saves to the database
        /// </summary>
        /// <param name="requestId"></param>
        private void RecalculateRequestTotal(int requestId) { // this recalculates the products and requests amount and stores it into the one request for the userids related
            var request = _context.Requests.Find(requestId); // finds the requestid and gives the data realted to it

            request.Total = (from rl in _context.RequestLines // get this data and store it in request var. // from fred in ReqLin
                             join p in _context.Products //join fred in products
                             on rl.ProductId equals p.Id // connect the Ids
                             where rl.RequestId == requestId //Where the request id matches teh requestid in the reccalctot parameter
                             select new {
                                 LineTotal = rl.Quantity * p.Price // takes the price and quantity and stores it in alias line total
                             }).Sum(x => x.LineTotal); // sums the linetotal alias and stores it into request.Total
            _context.SaveChanges();
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
            if (requestline is null) {
                throw new ArgumentNullException("Requestline");
            }
            if(requestline.Id != 0) {
                throw new ArgumentException("Requestline.Id must be zero!");
            }
            _context.RequestLines.Add(requestline);
            _context.SaveChanges();
            RecalculateRequestTotal(requestline.RequestId); // gets this and returns it after the savechanes go thru
            return requestline;
        }

        public void Change(RequestLine requestline) {
            _context.SaveChanges();
            RecalculateRequestTotal(requestline.RequestId);
        }

        public void Remove(int id) {
            var requestline = _context.RequestLines.Find(id);
            if (requestline is not null) {
                throw new Exception("Requestline not found!");
            }
            _context.RequestLines.Remove(requestline);
            _context.SaveChanges();
            RecalculateRequestTotal(requestline.RequestId);
        }
    }
}
