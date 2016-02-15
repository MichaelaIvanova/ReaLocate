namespace ReaLocate.Services.Data
{
    using ReaLocate.Data.Common;
    using ReaLocate.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;

        public UsersService(
            IRepository<User> usersRepo)
        {
            this.users = usersRepo;
        }

        public User GetUserDetails(string id)
        {
            return this.users.GetById(id);
        }
    }
}
