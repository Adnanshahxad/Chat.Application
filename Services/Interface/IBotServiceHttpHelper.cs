using System.Threading.Tasks;

namespace Services.Interface;

public interface IBotServiceHttpHelper
{
    Task<bool> StockCodeAsync(string stockSymbol);
}