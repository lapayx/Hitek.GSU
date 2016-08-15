using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Database.Model;
using Hitek.GSU.Logic.Database.Map;

namespace Hitek.GSU.Logic.Database
{
    public partial class Entities : Hitek.GSU.Models.ApplicationDbContext
    {
        public Entities()
            : base("EntityContext")
        {
        }

      //  public virtual DbSet<Account> Account { get; set; } 
        public virtual DbSet<TestSubject> TestSubject { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestQuestion> TestQuestion { get; set; }
        public virtual DbSet<TestAnswer> TestAnswer { get; set; } 
        public virtual DbSet<TestHistory> TestHistory { get; set; }

        public virtual DbSet<WorkTest> WorkTest { get; set; }


        //    public virtual DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            /*
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new UserRoleIntPkMap());
            modelBuilder.Configurations.Add(new UserLoginIntPkMap());
            modelBuilder.Configurations.Add(new UserClaimIntPkMap());
            modelBuilder.Configurations.Add(new RoleIntPkMap());*/
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Configurations.Add(new RoleLongPkMap());
            modelBuilder.Configurations.Add(new UserLoginLongPkMap());
            modelBuilder.Configurations.Add(new UserClaimLongPkMap());
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new UserRoleLongPkMap());

            base.OnModelCreating(modelBuilder);
           
            
        }
    }
}