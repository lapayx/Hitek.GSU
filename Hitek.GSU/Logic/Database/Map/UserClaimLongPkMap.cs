using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Map
{
    class UserClaimLongPkMap : EntityTypeConfiguration<UserClaimLongPk>
    {
        public UserClaimLongPkMap()
        {
            ToTable("AspNetUserClaims");
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasColumnName("Id").HasColumnType("INT"); 
            Property(t => t.ClaimType).IsRequired().HasColumnName("ClaimType").HasColumnType("NVARCHAR");
            Property(t => t.ClaimValue).IsRequired().HasColumnName("ClaimValue").HasColumnType("NVARCHAR");
            Property(t => t.UserId).IsRequired().HasColumnName("UserId").HasColumnType("BIGINT");


        }
    }
}