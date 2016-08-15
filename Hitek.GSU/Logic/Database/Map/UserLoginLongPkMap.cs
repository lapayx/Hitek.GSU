using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Map
{
    class UserLoginLongPkMap : EntityTypeConfiguration<UserLoginLongPk>
    {
        public UserLoginLongPkMap()
        {
            ToTable("AspNetUserLogins");
            HasKey(t => new { t.LoginProvider, t.ProviderKey });

            Property(t => t.UserId).IsRequired().HasColumnName("UserId").HasColumnType("BIGINT");
            Property(t => t.ProviderKey).IsRequired().HasColumnName("ProviderKey").HasMaxLength(128).HasColumnType("NVARCHAR");
            Property(t => t.LoginProvider).IsRequired().HasColumnName("LoginProvider").HasMaxLength(128).HasColumnType("NVARCHAR");

        }
    }
}