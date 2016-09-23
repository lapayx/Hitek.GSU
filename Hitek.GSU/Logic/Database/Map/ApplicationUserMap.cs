using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Map
{
    class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            ToTable("AspNetUsers");
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasColumnName("Id").HasColumnType("BIGINT");
            Property(t => t.Email).IsOptional().HasColumnName("Email").HasColumnType("NVARCHAR");
            Property(t => t.EmailConfirmed).IsRequired().HasColumnName("EmailConfirmed").HasColumnType("BIT");
            Property(t => t.AccessFailedCount).IsRequired().HasColumnName("AccessFailedCount").HasColumnType("INT");
            Property(t => t.PasswordHash).IsOptional().HasColumnName("PasswordHash").HasColumnType("NVARCHAR");
            Property(t => t.PhoneNumber).IsOptional().HasColumnName("PhoneNumber").HasColumnType("NVARCHAR");
            Property(t => t.PhoneNumberConfirmed).IsRequired().HasColumnName("PhoneNumberConfirmed").HasColumnType("BIT");
            Property(t => t.SecurityStamp).IsOptional().HasColumnName("SecurityStamp").HasColumnType("NVARCHAR");
            Property(t => t.TwoFactorEnabled).IsRequired().HasColumnName("TwoFactorEnabled").HasColumnType("BIT");
            Property(t => t.LockoutEndDateUtc).IsOptional().HasColumnName("LockoutEndDateUtc").HasColumnType("DATETIME");
            Property(t => t.LockoutEnabled).IsRequired().HasColumnName("LockoutEnabled").HasColumnType("BIT");
            Property(t => t.UserName).IsRequired().HasColumnName("UserName").HasMaxLength(255).HasColumnType("NVARCHAR");

            //HasOptional(t => t.).WithMany().Map(m => m.MapKey(""));

            HasMany(t => t.Claims).WithOptional().HasForeignKey(t => t.UserId);
            HasMany(t => t.Logins).WithOptional().HasForeignKey(t => t.UserId);
            HasMany(t => t.Roles).WithOptional().HasForeignKey(t => t.UserId);
            /*
            HasOptional(t => t.Claims).WithMany().Map(m => m.MapKey("id"));
            HasOptional(t => t.Logins).WithMany().Map(m => m.MapKey("id"));
            HasOptional(t => t.Roles).WithMany().Map(m => m.MapKey("id"));*/
            // HasMany(t => t.Claims).Map(x => x.MapLeftKey("user_id"));
            /* Property(t => t.Email).HasColumnName("email").HasColumnType("varchar");Property(t => t.FullWellboreName).IsRequired().HasColumnName("FULL_WELLBORE_NAME");
             Property(t => t.OilAreaName).HasColumnName("OIL_AREA_NAME");
             Property(t => t.IsNowDrilling).HasColumnType("int").HasColumnName("IS_NOW_DRILLING");
             */
            //  HasOptional(x=>x.Claims).WithMany().HasForeignKey(x=>x.)
        }
    }
}