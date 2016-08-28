using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Map
{
    class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            ToTable("Client");
            HasKey(t => t.Id);
            Property(t => t.Id).IsRequired().HasColumnName("Id").HasColumnType("NVarCAHR");
            Property(t => t.Secret)
                .IsRequired()
                .HasColumnName("Secret")
                .HasColumnType("NVARCHAR");
            Property(t => t.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(100)
                .HasColumnType("NVARCHAR");
            Property(t => t.ApplicationType).IsRequired().HasColumnName("ApplicationType").HasColumnType("INT");
            Property(t => t.Active).IsRequired().HasColumnName("Active").HasColumnType("BIT");
            Property(t => t.RefreshTokenLifeTime).IsRequired ().HasColumnName("RefreshTokenLifeTime").HasColumnType("INT");
            Property(t => t.AllowedOrigin)
                .IsRequired()
                .HasColumnName("AllowedOrigin")
                .HasMaxLength(100)
                .HasColumnType("NVARCHAR");
           
        }
    }
}