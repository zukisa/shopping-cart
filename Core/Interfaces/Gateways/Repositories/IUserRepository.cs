using Core.Entities;

namespace Core.Interfaces.Gateways.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool ExistsByEmail(string email);
        bool CheckPassword(User user, string password);
        User CreateUser(User user);
        User UpdateUser(User user);
        User FindUserByEmail(string email);
    }
}
