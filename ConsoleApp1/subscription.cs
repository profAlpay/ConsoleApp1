// Framework: .NET
// Technology stack: C#

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

public  class Subscriptions
{
    private string username;
    private List<Subscription> subscriptions;

    public Subscriptions(string username = null)
    {
        this.username = username;
        this.subscriptions = LoadSubscriptions();
    }

    private List<Subscription> LoadSubscriptions()
    {
        List<Subscription> subscriptions = new List<Subscription>();
        if (File.Exists("subscriptions.csv"))
        {
            using (var reader = new StreamReader("subscriptions.csv"))
            {
                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture))
                {
                    subscriptions = csv.GetRecords<Subscription>().ToList();
                }

            }
        
        }
        return subscriptions;
    }

    public void Add(string username, string subscriptionName, string paymentDay)
    {
        subscriptions.Add(new Subscription
        {
            Username = username,
            SubscriptionName = subscriptionName,
            PaymentDay = paymentDay
        });
        SaveSubscriptions();
    }

    public void Update(string username, string oldSubscriptionName, string newSubscriptionName, string newPaymentDay)
    {
        var subscription = subscriptions.FirstOrDefault(s => s.Username == username && s.SubscriptionName == oldSubscriptionName);
        if (subscription != null)
        {
            subscription.SubscriptionName = newSubscriptionName;
            subscription.PaymentDay = newPaymentDay;
            SaveSubscriptions();
        }
        else
        {
            Console.WriteLine($"Subscription '{oldSubscriptionName}' for user '{username}' not found.");
        }
    }

    public void Delete(string username, string subscriptionName)
    {
        var subscription = subscriptions.FirstOrDefault(s => s.Username == username && s.SubscriptionName == subscriptionName);
        if (subscription != null)
        {
            subscriptions.Remove(subscription);
            SaveSubscriptions();
        }
        else
        {
            Console.WriteLine($"Subscription '{subscriptionName}' for user '{username}' not found.");
        }
    }

    public bool Search(string username, string subscriptionName)
    {
        return subscriptions.Any(s => s.Username == username && s.SubscriptionName == subscriptionName);
    }

    public void SaveSubscriptions()
    {
        using (var writer = new StreamWriter("subscriptions.csv"))
        {
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(subscriptions);
            }
        }
      
    }

    public void Display()
    {
        if (subscriptions.Any())
        {
            var headers = new string[] { "Username", "Subscription Name", "Payment Day" };
            var rows = subscriptions
                .Where(s => username == null || s.Username == username)
                .Select(s => new string[] { s.Username, s.SubscriptionName, s.PaymentDay })
                .ToList();
            if (rows.Any())
            {
                Console.WriteLine(tabulate(rows, headers, "grid"));
            }
            else
            {
                Console.WriteLine($"No subscriptions found for user '{username}'.");
            }
        }
        else
        {
            Console.WriteLine("No subscriptions found.");
        }
    }

    private string tabulate(List<string[]> rows, string[] headers, string tablefmt)
    {
        // Implement tabulate logic here
        return ""; // Placeholder return
    }
}

public class Subscription
{
    [Name("username")]
    public string Username { get; set; }

    [Name("subscription_name")]
    public string SubscriptionName { get; set; }

    [Name("payment_day")]
    public string PaymentDay { get; set; }
}

//class Program
//{
//    static void Main()
//    {
//        Subscriptions subManager = new Subscriptions();

//        // Add a new subscription
//        subManager.Add("john_doe", "Netflix", "15");

//        // Update an existing subscription
//        subManager.Update("john_doe", "Netflix", "Amazon Prime", "20");

//        // Delete a subscription
//        subManager.Delete("john_doe", "Amazon Prime");

//        // Search for a subscription
//        bool found = subManager.Search("john_doe", "Netflix");
//        Console.WriteLine("Subscription found: " + found);

//        // Display all subscriptions
//        subManager.Display();
//    }
//}