using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Map
{
    class RefreshTokenMap : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenMap()
        {
            ToTable("RefreshToken");
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasColumnName("Id");//.HasColumnType("NVarCAHR");
            Property(t => t.Subject)
                .IsRequired()
                .HasColumnName("Subject")
                .HasMaxLength(50);
            //.HasColumnType("NVARCHAR");
            Property(t => t.ClientId)
                .IsRequired()
                .HasColumnName("ClientId")
                .HasMaxLength(50);
            //.HasColumnType("NVARCHAR");
            Property(t => t.IssuedUtc).IsRequired().HasColumnName("IssuedUtc");
            Property(t => t.ExpiresUtc).IsRequired().HasColumnName("ExpiresUtc");//.HasColumnType("BIT");
            Property(t => t.ProtectedTicket)
                .IsRequired()
                .HasColumnName("ProtectedTicket");
            //.HasColumnType("NVARCHAR");



        }
    }
}