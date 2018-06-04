using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SBM.MVC5.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
           public static string DBType;
           public static string IsLocal;

        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //    Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());
        //}

        public ApplicationDbContext(string conn, string islocal)
            : base(conn, throwIfV1Schema: false)
        {
            if (conn.ToLower() == "SQLConnection".ToLower())
            {
                if(islocal == "true")
                    Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
                else
                    Database.SetInitializer<ApplicationDbContext>(null);
            }
            else
                Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            DBType = System.Web.Configuration.WebConfigurationManager.AppSettings["DBType"].ToString().ToLower();
            IsLocal = System.Web.Configuration.WebConfigurationManager.AppSettings["IsLocal"].ToString().ToLower();
            if (DBType == "mysql")
            {
                return new ApplicationDbContext("MySqlConnection", IsLocal);
            }
            return new ApplicationDbContext("SQLConnection", IsLocal);
        }
    }
}