using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyBooks.Data.Models
{
    public class MyBooksDBContext :  DbContext
    {
        public MyBooksDBContext(DbContextOptions options)
              : base(options)
        {
        }


        public DbSet<UserRoles>  UserRoles { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Catalogs> Catalogs { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Catalogs_Books> Catalogs_Books { get; set; }
        public DbSet<Cart_items> Cart_Items { get; set; }
        public DbSet<Shopping_Carts> Shopping_Carts { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Files> Files { get; set; }
    }
}
