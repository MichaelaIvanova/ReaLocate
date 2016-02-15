namespace ReaLocate.Services.Data
{
    using ReaLocate.Data.Models;

    public interface IUsersService
    {
        User GetUserDetails(string id);

        void Update(User user);
    }
}