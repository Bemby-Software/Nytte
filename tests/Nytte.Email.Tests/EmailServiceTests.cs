using Moq.AutoMock;
using NUnit.Framework;
using Nytte.Email.Abstractions;
using Nytte.Testing;
using Shouldly;

namespace Nytte.Email.Tests
{
    public class EmailServiceTests : ServiceUnderTest<IEmailService, EmailService>
    {
        public override void Setup()
        {
            
        }

        [TestCase("test@gmail.com")]
        [TestCase("test@test.karoo.co.uk")]
        [TestCase("test@sub.domain.ext")]
        [TestCase("email@subdomain.example.com")]
        [TestCase("email@[123.123.123.123]")]
        [TestCase("\"email\"@example.com")]
        [TestCase("1234567890@example.com")]
        [TestCase("email@example-one.com")]
        [TestCase("_______@example.com")]
        [TestCase("email@example.name")]
        [TestCase("email@example.museum")]
        [TestCase("email@example.co.jp")]
        [TestCase("firstname-lastname@example.com")]
        public void IsEmailValid_True(string email)
        {
            //Arrange
            var serviceUnderTest = CreateSut();
            
            //Act
            var isEmailValid = serviceUnderTest.IsValidEmailAddress(email);
            
            //Assert
            isEmailValid.ShouldBe(true);
        }

        [TestCase("plainaddress")]
        [TestCase("#@%^%#$@#$@#.com")]
        [TestCase("@example.com")]
        [TestCase("email.example.com")]
        [TestCase("email@example @example.com")]
        [TestCase("email@123.123.123.123")]
        [TestCase(" ")]
        [TestCase(null)]
        public void IsEmailValid_False(string email)
        {
            //Arrange
            var serviceUnderTest = CreateSut();
            
            //Act
            var isEmailValid = serviceUnderTest.IsValidEmailAddress(email);
            
            //Assert
            isEmailValid.ShouldBe(false);
        }
    }
}