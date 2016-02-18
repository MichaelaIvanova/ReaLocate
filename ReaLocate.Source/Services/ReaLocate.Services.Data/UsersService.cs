namespace ReaLocate.Services.Data
{
    using System;
    using ReaLocate.Data.Common;
    using ReaLocate.Data.Models;
    using Web;
    using System.Linq;
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IIdentifierProvider identifierProvider;

        public UsersService(
            IRepository<User> usersRepo, IIdentifierProvider identifierProvider)
        {
            this.users = usersRepo;
            this.identifierProvider = identifierProvider;
        }

        public string EncodeId(int id)
        {

            var stringId = this.identifierProvider.EncodeId(id);

            return stringId;
        }

        public User GetUserDetailsById(string id)
        {
            return this.users.GetById(id);
        }

        public void Update(User user)
        {
            this.users.Update(user);

            this.users.SaveChanges();
        }

        public IQueryable<User> GetAll()
        {
            return this.users
                 .All()
                 .OrderByDescending(c => c.FirstName);
        }
    }
}
