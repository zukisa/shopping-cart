using Core.Dto;
using System;

namespace Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Token GenerateEncodedToken(Guid userId);
    }
}
