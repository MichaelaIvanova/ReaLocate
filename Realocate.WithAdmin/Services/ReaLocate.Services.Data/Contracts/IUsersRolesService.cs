namespace ReaLocate.Services.Data.Contracts
{
    using ReaLocate.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUsersRolesService
    {
        ExtendedUserRole GetRoleByName(string name);

        ExtendedUserRole GetRoleById(string id);
    }
}
