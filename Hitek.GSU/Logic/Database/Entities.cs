using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database
{
    public partial class Entities : ApplicationDbContext
    {
        public Entities()
            : base("Model1")
        {
        }

      //  public virtual DbSet<Account> Account { get; set; } 
        public virtual DbSet<TestSubject> TestSubject { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestQuestion> TestQuestion { get; set; }
        public virtual DbSet<TestAnswer> TestAnswer { get; set; } 
        public virtual DbSet<TestHistory> TestHistory { get; set; }
       

    //    public virtual DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new UserRoleIntPkMap());
            modelBuilder.Configurations.Add(new UserLoginIntPkMap());
            modelBuilder.Configurations.Add(new UserClaimIntPkMap());
            modelBuilder.Configurations.Add(new RoleIntPkMap());*/

        }
    }
}