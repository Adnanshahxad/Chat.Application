using System.Threading.Tasks;
using Domain.Configs;
using Microsoft.Extensions.Options;
using RestSharp;
using Services.Interface;

namespace Services.Helper;

public class BotServiceHttpHelper : IBotServiceHttpHelper
{
    public BotServiceHttpHelper(IOptions<WebAppSettings> settings)
    {
        RestClient = new RestClient(settings.Value.ChatBotServiceUrl);
    }

    private IRestClient RestClient { get; }

    public async Task<bool> StockCodeAsync(string stockSymbol)
    {
        var request = CreateRequest($"/Stock/{stockSymbol}");
        request.Method = Method.POST;
        var response = await RestClient.ExecuteAsync<bool>(request);

        return response.Data;
    }

    private static IRestRequest CreateRequest(string resource)
    {
        return new RestRequest(resource).AddHeader("Accept", "application/json");
    }
}