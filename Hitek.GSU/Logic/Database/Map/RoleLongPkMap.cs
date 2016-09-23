using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Map
{
    class RoleLongPkMap : EntityTypeConfiguration<RoleLongPk>
    {
        public RoleLongPkMap()
        {
            ToTable("[AspNetRoles]");
            HasKey(t => t.Id);

            Property(t => t.Id).IsRequired().HasColumnName("Id").HasColumnType("BIGINT"); 
            Property(t => t.Name).IsRequired().HasColumnName("Name").HasMaxLength(255).HasColumnType("NVARCHAR");

            //HasOptional(t => t.Users).WithMany().HasForeignKey(x => x.);
            HasMany(t => t.Users).WithRequired().HasForeignKey(t => t.RoleId);// WithOptional(t => t.UserId);
        }
    }
}