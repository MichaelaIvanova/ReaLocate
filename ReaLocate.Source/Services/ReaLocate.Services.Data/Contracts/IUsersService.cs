namespace ReaLocate.Services.Data
{
    using ReaLocate.Data.Models;
    using System.Linq;
    public interface IUsersService
    {
        User GetUserDetailsById(string id);

        void Update(User user);

        string EncodeId(int id);

        IQueryable<User> GetAll();

        void Delete(User user);

        void Add(User user);
    }
}