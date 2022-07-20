using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Chat.Application.Bot.Service.Helper;

public static class StockFileHelper
{
    private static IEnumerable<Stock> ReadCsvData(string fileName)
    {
        try
        {
            using var reader = new StreamReader(fileName);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Stock>().ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to load file{fileName}", ex);
        }
    }

    public static string GetStockPrice(string fileName, string symbol)
    {
        var data = ReadCsvData(fileName);
        var symbolData = data.FirstOrDefault(x => x.Symbol == symbol);

        return symbolData?.Open;
    }

    public class Stock
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Close { get; set; }
        public string Volume { get; set; }
    }
}