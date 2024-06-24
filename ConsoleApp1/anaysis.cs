using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

public class Analysis
{
    public void TotalPrice(string userId)
    {
        // CSV dosyalarını okuyun
        var subscriptionsData = File.ReadAllLines("subscriptions.csv");

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };

        List<Price> priceList;
        using (var reader = new StreamReader("price.csv"))
        using (var csv = new CsvReader(reader, config))
        {
            priceList = new List<Price>(csv.GetRecords<Price>());
        }

        // Kullanıcının aboneliklerini alın
        List<string> userSubscriptions = new List<string>();
        foreach (var line in subscriptionsData)
        {
            if (line.StartsWith(userId))
            {
                var parts = line.Trim().Split(',');
                for (int i = 1; i < parts.Length; i += 2)
                {
                    userSubscriptions.Add(parts[i]);
                }
                break;
            }
        }

        // Kullanıcının abonelik fiyatlarını toplayın
        decimal totalCost = 0;
        foreach (var subscription in userSubscriptions)
        {
            var priceRecord = priceList.Find(p => p.SubscriptionName == subscription);
            if (priceRecord != null)
            {
                decimal price = priceRecord.Pricef;
                Console.WriteLine(price);
                totalCost += price;
            }
        }

        Console.WriteLine($"user toplam abonelik ücreti: ${totalCost:F2}");
    }
}

public class Price
{
    public string SubscriptionName { get; set; }
    public decimal Pricef { get; set; }
}
