using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Hitek.GSU.Models
{



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, RoleLongPk, long,
        UserLoginLongPk, UserRoleLongPk, UserClaimLongPk>
    {
        public ApplicationDbContext()
            : base("EntityContext")
        {
        }
        public ApplicationDbContext(string stringConect)
            : base(stringConect)
        {
        }
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