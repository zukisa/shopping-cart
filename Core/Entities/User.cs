using Core.Entities.Base;
using System;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public User() { }
        public User(string email, string password, Guid? id = null)
        {
            Email = email;
            Password = password;
            if(id != null)
            {
                Id = (Guid)id;
            }
        }
    }
}
