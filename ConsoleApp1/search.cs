using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Search
{
    // CSV dosyasını okuyun ve verileri saklayın
    public static List<List<string>> ReadSubscriptions(string filePath)
    {
        var subscriptions = new List<List<string>>();
        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var row = new List<string>(line.Split(','));
                subscriptions.Add(row);
            }
        }
        return subscriptions;
    }

    // Abonelikleri işleyin ve sonuçları döndürün
    public static List<List<string>> ProcessSubscriptions(List<List<string>> subscriptions)
    {
        var results = new List<List<string>>();
        foreach (var subscription in subscriptions)
        {
            var username = subscription[0];
            for (int i = 1; i < subscription.Count; i += 2)
            {
                if (i + 1 < subscription.Count)
                {
                    var subscriptionName = subscription[i];
                    var paymentDay = subscription[i + 1];
                    results.Add(new List<string> { username, subscriptionName, paymentDay });
                }
            }
        }
        return results;
    }

    // Sonuçları tablo formatında yazdırın
    public static void PrintResults(List<List<string>> results)
    {
        Console.WriteLine("| Username | Subscription Name | Payment Day |");
        Console.WriteLine("|----------|-------------------|-------------|");
        foreach (var result in results)
        {
            Console.WriteLine($"| {result[0],-8} | {result[1],-17} | {result[2],-11} |");
        }
    }

    // Anahtar kelimeye göre abonelikleri arayın
    public static List<List<string>> SearchByKeyword(List<List<string>> subscriptions, string keyword)
    {
        var results = new List<List<string>>();
        foreach (var subscription in subscriptions)
        {
            var username = subscription[0];
            for (int i = 1; i < subscription.Count; i += 2)
            {
                if (i + 1 < subscription.Count)
                {
                    var subscriptionName = subscription[i];
                    var paymentDay = subscription[i + 1];
                    if (subscriptionName.ToLower().Contains(keyword.ToLower()))
                    {
                        results.Add(new List<string> { username, subscriptionName, paymentDay });
                    }
                }
            }
        }
        return results;
    }

    // Arama sonuçlarını tablo formatında yazdırın
    public static void PrintSearchResults(List<List<string>> results)
    {
        Console.WriteLine("| Username | Subscription Name | Payment Day |");
        Console.WriteLine("|----------|-------------------|-------------|");
        foreach (var result in results)
        {
            Console.WriteLine($"| {result[0],-8} | {result[1],-17} | {result[2],-11} |");
        }
    }

    // Ana fonksiyon
    public static void Main(string[] args)
    {
        var filePath = "subscriptions.csv";
        var subscriptions = ReadSubscriptions(filePath);

        // Tüm abonelikleri işleyin ve sonuçları yazdırın
        Console.WriteLine("All Subscriptions:");
        var results = ProcessSubscriptions(subscriptions);
        PrintResults(results);

        // Kullanıcıdan anahtar kelime araması yapmasını isteyin
        Console.Write("\nEnter 'keyword' to search for subscriptions by keyword: ");
        var searchType = Console.ReadLine().Trim().ToLower();
        if (searchType == "keyword")
        {
            Console.Write("Enter a keyword to search for subscriptions: ");
            var keyword = Console.ReadLine().Trim();
            var keywordResults = SearchByKeyword(subscriptions, keyword);

            // Arama sonuçlarını yazdırın
            Console.WriteLine($"\nSubscriptions containing '{keyword}':");
            PrintSearchResults(keywordResults);
        }
        else
        {
            Console.WriteLine("Invalid search type. Please enter 'keyword'.");
        }
    }
}
