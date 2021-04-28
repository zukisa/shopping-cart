using System;

namespace Core.Dto
{
    public sealed class Token
    {
        public Guid Id { get; }
        public string AuthToken { get; }
        public int ExpiresIn { get; }

        public Token(Guid id, string authToken, int expiresIn)
        {
            Id = id;
            AuthToken = authToken;
            ExpiresIn = expiresIn;
        }
    }
}
