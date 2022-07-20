using Domain.Interfaces;
using Domain.Models;
using Moq;
using Services;

namespace Chat.Application.Web.Service.Tests;

[TestFixture]
public class AccountServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(Description = "Should successfully logged-in and return IsSucceeded to true")]
    public async Task When_ProvideValidUser_ReturnIsSucceededTrue()
    {
        var loginModel = new LoginViewModel
        {
            Email = "abc@123",
            Password = "123"
        };

        //mocking chat repo
        var mockAccountRepo = new Mock<IAccountRepository>();
        mockAccountRepo.Setup(x => x.LoginAsync(loginModel, CancellationToken.None))
            .ReturnsAsync(new ResultModel("Succeeded", "abc@123"));

        var accountService = new AccountService(mockAccountRepo.Object);
        var result = await accountService.LoginAsync(loginModel);
        Assert.That(result.IsSucceeded, Is.EqualTo(true));
        Assert.That(result.Message, Is.EqualTo("Succeeded"));
    }
}