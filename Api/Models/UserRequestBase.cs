namespace Api.Models
{
    /// <summary>
    /// User Request base model
    /// </summary>
    public abstract class UserRequestBase
    {
        /// <summary>
        /// Email Address for user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password for user
        /// </summary>
        public string Password { get; set; }
    }
}
