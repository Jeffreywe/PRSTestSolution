using Microsoft.EntityFrameworkCore;
using System;

namespace PRSLibrary.Models {
    /// <summary>
    /// to rollback migration
    /// package manager, last migration you want to keep, use numbers,
    /// update-database "0" rolls back to last migration
    /// </summary>
    public class PRSDbContext : DbContext {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestLine> RequestLines { get; set; }



        public PRSDbContext() { }

        public PRSDbContext(DbContextOptions<PRSDbContext> options) 
            : base(options) { } // changes options and takes options and passes them to the parent using base

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if(!builder.IsConfigured) { // if not configured before will get configured inside if statement
                builder.UseSqlServer(
                    "server=localhost\\sqlexpress;database=TestPRSDb;trusted_connection=true;"
                    ); // keep sqlexpress, change the database to create more
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            // makes Username in User unique
            builder.Entity<User>(a => {
                a.HasIndex(p => p.Username) // lambda syntax, makes Username from User true
                        .IsUnique(true);
            }); // scope validates variable use outside of syntax

            builder.Entity<Vendor>(
                c => c.HasIndex(d => d.Code)
                        .IsUnique(true));

            builder.Entity<Product>(
                e => e.HasIndex(f => f.PartNbr)
                        .IsUnique(true));

        }
    }
}
