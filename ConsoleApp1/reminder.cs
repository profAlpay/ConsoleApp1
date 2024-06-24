using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Reminder
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

    // Bir sonraki ödeme tarihine kadar kalan günleri hesaplayın
    public static object CalculateDaysRemaining(string dayOfMonth)
    {
        var today = DateTime.Today;
        var currentYear = today.Year;
        var currentMonth = today.Month;

        try
        {
            var nextPaymentDate = new DateTime(currentYear, currentMonth, int.Parse(dayOfMonth));
            if (nextPaymentDate < today)
            {
                // Eğer tarih bu ay için geçmişteyse, bir sonraki ay için hesaplayın
                nextPaymentDate = currentMonth == 12 ?
                    new DateTime(currentYear + 1, 1, int.Parse(dayOfMonth)) :
                    new DateTime(currentYear, currentMonth + 1, int.Parse(dayOfMonth));
            }
            var daysRemaining = (nextPaymentDate - today).Days;
            return daysRemaining;
        }
        catch (Exception)
        {
            return "Invalid Date";
        }
    }

    // Abonelikleri işleyin
    public static List<List<object>> ProcessSubscriptions(List<List<string>> subscriptions)
    {
        var results = new List<List<object>>();
        foreach (var subscription in subscriptions)
        {
            var username = subscription[0];
            for (int i = 1; i < subscription.Count; i += 2)
            {
                if (i + 1 < subscription.Count)
                {
                    var subscriptionName = subscription[i];
                    var paymentDay = subscription[i + 1];
                    var daysRemaining = CalculateDaysRemaining(paymentDay);
                    results.Add(new List<object> { username, subscriptionName, paymentDay, daysRemaining });
                }
            }
        }
        return results;
    }

    // Sonuçları tablo formatında yazdırın
    public static void PrintResults(List<List<object>> results)
    {
        Console.WriteLine("| Username | Subscription Name | Payment Day | Days Remaining |");
        Console.WriteLine("|----------|-------------------|-------------|----------------|");
        foreach (var result in results)
        {
            Console.WriteLine($"| {result[0],-8} | {result[1],-17} | {result[2],-11} | {result[3],-14} |");
        }
    }

    // Ana fonksiyon
    public static void Main(string[] args)
    {
        var filePath = "subscriptions.csv";
        var subscriptions = ReadSubscriptions(filePath);
        var results = ProcessSubscriptions(subscriptions);
        PrintResults(results);
    }
}
