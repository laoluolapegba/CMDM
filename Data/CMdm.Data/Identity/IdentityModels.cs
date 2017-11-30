using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using CMdm.Data.Initializers;

namespace CMdm.Data.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;

        }
        //public string NameIdentifier { get; set; }
        //public string Firstname { get; set; }
        //public string Lastname { get; set; }
        //public bool AdminConfirmed { get; set; }
        //public string TeamLeader { get; set; }
        //public virtual ICollection<File> Files { get; set; }
    }
    //public class ApplicationRole : IdentityRole
    //{

    //    public ApplicationRole() : base() { }

    //    public ApplicationRole(string name) : base(name) { }

    //    public string Description { get; set; }

    //}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //static ApplicationDbContext()
        //{
        //    Database.SetInitializer(new SQLServerInitializer());
        //}

        public ApplicationDbContext() 
            : base("AppDbContext")
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}