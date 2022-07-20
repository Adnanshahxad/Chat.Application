using Domain.Interfaces;
using Domain.Models;
using Moq;
using Services;
using Services.Interface;

namespace Chat.Application.Web.Service.Tests;

[TestFixture]
public class ChatHubServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(Description = "Should send the stock symbol value bot service and returns true")]
    public async Task When_StockCommandExist_SendSymbol_ReturnTrue()
    {
        var messageModel = new MessageViewModel
        {
            UserName = "TestUser",
            Message = "/stock=AAPL.US",
            DateTime = DateTime.Now.ToShortDateString()
        };

        //mocking chat repo
        var mockChatRepo = new Mock<IChatRepository>();
        mockChatRepo.Setup(x => x.SaveMessageAsync(messageModel)).ReturnsAsync(true);

        //mocking bot http helper
        var mockBotHttpHelper = new Mock<IBotServiceHttpHelper>();
        mockBotHttpHelper.Setup(x => x.StockCodeAsync(It.IsAny<string>())).ReturnsAsync(true);

        var chatService = new ChatHubService(mockChatRepo.Object, null, mockBotHttpHelper.Object);
        var result = await chatService.SendMessageAsync(messageModel);
        Assert.That(result, Is.EqualTo(true));
    }
}