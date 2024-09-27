﻿using Microsoft.EntityFrameworkCore;
using Bulky.Models;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { ID = 1, Name="Action",DisplayOrder=1 },
                new Category { ID = 2, Name="Science",DisplayOrder=2 },
                new Category { ID = 3, Name="History",DisplayOrder=3 }

                );




            }
    
    }
}
