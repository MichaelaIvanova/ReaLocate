namespace ReaLocate.Services.Data
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ReaLocate.Data.Models;
    using ReaLocate.Data.Common;
    public class UsersRolesService : IUsersRolesService
    {
        private readonly IRepository<ExtendedUserRole> roles;

        public UsersRolesService(IRepository<ExtendedUserRole> roles)
        {
            this.roles = roles;
        }

        public ExtendedUserRole GetRoleByName(string name)
        {
            return this.roles.All().Where(r => r.Description == name).First();
        }
    }
}
