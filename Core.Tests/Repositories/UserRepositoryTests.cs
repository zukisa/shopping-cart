using Core.Interfaces.Gateways.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Core.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public void User_Exists()
        {
            //Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(_ => _.ExistsByEmail(It.IsAny<string>()))
                .Returns(true);

            //Act
            var userExists = mockRepo.Object.ExistsByEmail("test@gmail.com");


            //Assert
            Assert.True(userExists);
        }
        [Theory]
        [InlineData("test@gmail")]
        [InlineData("tt@gmail.com")]
        public void User_Does_Not_Exists(string email)
        {
            //Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(_ => _.ExistsByEmail(It.IsAny<string>()))
                .Returns(false);

            //Act
            var userExists = mockRepo.Object.ExistsByEmail(email);

            //Assert
            Assert.False(userExists);
        }
    }
}
