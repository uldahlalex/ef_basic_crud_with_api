using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products
    {
        get; set;
    }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


public class Product
{
    public string ProductName
    {
        get; set;
    }
    
    public int Id { get; set; }
}