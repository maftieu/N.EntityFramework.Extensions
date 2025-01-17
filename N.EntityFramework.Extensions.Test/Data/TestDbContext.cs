﻿using N.EntityFramework.Extensions.Test.Data;
using System;
using System.Data.Entity;


namespace N.EntityFramework.Extensions.Test.Data
{
    public class TestDbContext : DbContext
    {
        private static readonly string _connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog = N.EntityFramework.Extensions.Test.Data.TestDbContext; Integrated Security = True; MultipleActiveResultSets=True";
        public virtual DbSet<Order> Orders { get; set;  }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductWithComplexKey> ProductsWithComplexKey { get; set; }
        public virtual DbSet<TpcPerson> TpcPeople { get; set; }
        public virtual DbSet<TphPerson> TphPeople { get; set; }
        public virtual DbSet<TphCustomer> TphCustomers { get; set; }
        public virtual DbSet<TphVendor> TphVendors { get; set; }

        public TestDbContext() : base(_connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TestDbContext, TestDbConfiguration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        	//modelBuilder.Entity<TphPerson>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<TpcCustomer>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("TpcCustomer");
            });
            modelBuilder.Entity<TpcVendor>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("TpcVendor");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
