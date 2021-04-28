using System;

namespace Core.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
