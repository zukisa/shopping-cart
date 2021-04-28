using Core.Dto;
using Core.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Core.Tests.Services
{
    public  class JwtFactoryTests
    {
        [Fact]
        public void Can_Generate_Token()
        {
            //Arrange
            Guid Id = new Guid("EF74C43C-D136-4217-A39C-93AEB0BC908E");
            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(_ => _.GenerateEncodedToken(It.IsAny<Guid>()))
                .Returns(new Token(Id, "", 0));

            //Act
            var result = mockJwtFactory.Object.GenerateEncodedToken(Id);


            //Assert 
            Assert.Equal(Id, result.Id);

        }
    }
}
