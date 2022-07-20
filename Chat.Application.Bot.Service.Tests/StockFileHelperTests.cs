using Chat.Application.Bot.Service.Helper;

namespace Chat.Application.Bot.Service.Tests;

[TestFixture]
public class StockFileHelperTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase(Description = "Should successfully logged-in and return IsSucceeded to true")]
    public void When_ProvideFilePath_ShouldRead_ReturnSharePrice()
    {
        const string filePath = @"AppData\stock.csv";
        var symbol = "AAPL.US";
        var price = StockFileHelper.GetStockPrice(filePath, symbol);
        Assert.That(price, Is.EqualTo("137.46"));
    }
}