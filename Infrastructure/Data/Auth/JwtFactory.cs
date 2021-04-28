using Core.Dto;
using Core.Interfaces.Gateways.Repositories;
using Core.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Data.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtIssuerOptions _jwtOptions;
        public JwtFactory(IUserRepository userRepository, IOptions<JwtIssuerOptions> options)
        {
            _userRepository = userRepository;
            _jwtOptions = options.Value;
        }
        public Token GenerateEncodedToken(Guid userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null) throw new Exception($"User with Id:{userId} was not found");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            //Add roles claims if the system supports roles

            // Generate the JWT security token and encode it
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.NotBefore,
                _jwtOptions.Expiration,
                SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new Token(userId, encodedJwt, (int)_jwtOptions.ValidFor.TotalSeconds);
        }
        private SigningCredentials SigningCredentials => new SigningCredentials(Common.Extensions.CustomLinqExtensions.GetSymmetricSecurityKey(_jwtOptions.SecretKey), SecurityAlgorithms.HmacSha256);

        public static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero) throw new ArgumentException("Must be a non-zero TimeSpan", nameof(JwtIssuerOptions.ValidFor));
        }
    }
}
