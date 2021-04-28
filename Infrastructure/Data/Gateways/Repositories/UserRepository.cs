using Core.Entities;
using Core.Interfaces.Gateways.Repositories;
using Infrastructure.Data.EF;
using Infrastructure.Data.Gateways.Repositories.Specifications;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Data.Gateways.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ShoppingCartDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckPassword(User user, string password)
        {
            return BC.Verify(password, user.Password);
        }

        public User CreateUser(User user)
        {
           User newUser = Add(new User(user.Email, BC.HashPassword(user.Password)));
            return new User(newUser.Email, string.Empty, id: newUser.Id);
        }

        public bool ExistsByEmail(string email)
        {
            User user = GetSingleBySpec(new UserSpecification(_ => _.Email.ToLower() == email.ToLower()));
            return user != null;
        }

        public User FindUserByEmail(string email)
        {
            return GetSingleBySpec(new UserSpecification(_ => _.Email.ToLower() == email.ToLower()));
        }

        public User UpdateUser(User user)
        {
            User userToUpdate = new User(user.Email, BC.HashPassword(user.Password), user.Id);
            Update(userToUpdate, user.Id);
            return new User(user.Email, string.Empty, user.Id);
        }
    }
}
