using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Map
{
    class UserRoleLongPkMap : EntityTypeConfiguration<UserRoleLongPk>
    {
        public UserRoleLongPkMap()
        {
            ToTable("AspNetUserRoles");
            HasKey(t => new { t.UserId, t.RoleId });

            Property(t => t.UserId).IsRequired().HasColumnName("UserId").HasColumnType("BIGINT");
            Property(t => t.RoleId).IsRequired().HasColumnName("RoleId").HasColumnType("BIGINT");


        }
    }
}