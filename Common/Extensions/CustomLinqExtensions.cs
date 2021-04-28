using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Common.Extensions
{
    public static class CustomLinqExtensions
    {
        /// <summary>
        /// Returns true if the value is null or empty or blank else false
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string value) => string.IsNullOrEmpty(value?.Trim());
        /// <summary>
        /// Generate Secret Key
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    }
}
