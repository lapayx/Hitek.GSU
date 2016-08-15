using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            
           /* modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new UserRoleLongPkMap());
            modelBuilder.Configurations.Add(new UserLoginLongPkMap());
            modelBuilder.Configurations.Add(new UserClaimLongPkMap());
            modelBuilder.Configurations.Add(new RoleLongPkMap());*/

        }
    }
}