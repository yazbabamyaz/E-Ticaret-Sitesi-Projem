using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class AppDbContext:DbContext
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EshopDb;Trusted_Connection=true");
            //optionsBuilder.UseLazyLoadingProxies();//Microsoft.EntityFrameworkCore.Proxies 
            //kütüphaneyi yüklemek lazım sonra include gerek kalmaz.hangsii daha iyi ??
        }



        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{
        //    //program.cs de ayar yapcaz.Hangi constringe bağlanacağını
        //}

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Sales> Sales { get; set; }
    }
}
