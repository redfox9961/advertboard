using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess.EntityFramework
{
    /// <summary>
    /// RoleStore для IdentityRole
    /// </summary>
    public class AppRoleStore : RoleStore<IdentityRole>
    {
        public AppRoleStore(DbContext context) : base(context)
        {
        }
    }
}
