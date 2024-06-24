using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

public static class Programf
{
    static Dictionary<string, string> LoadUsers(string filename = "users.csv")
    {
        var users = new Dictionary<string, string>();
        if (File.Exists(filename))
        {
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        users[parts[0]] = parts[1];
                    }
                }
            }
        }
        return users;
    }

    static void SaveUser(string username, string password, string filename = "users.csv")
    {
        string path = "C:\\Users\\KEREM\\source\\repos\\ConsoleApp1\\ConsoleApp1\\";
        

        using (var writer = new StreamWriter(path + filename, true))
        {
            writer.WriteLine($"{username},{password}");
        }
    }

    static bool SignIn(Dictionary<string, string> users)
    {
        Console.Write("Enter your username: ");
        var username = Console.ReadLine();
        Console.Write("Enter your password: ");
        var password = Console.ReadLine();

        if (users.ContainsKey(username) && users[username] == password)
        {
            Console.WriteLine("Sign in successful!");
            return true;
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
            return false;
        }
    }

    static void SignUp(Dictionary<string, string> users)
    {
        Console.Write("Choose a username: ");
        var username = Console.ReadLine();
        if (users.ContainsKey(username))
        {
            Console.WriteLine("Username already exists. Please choose a different username.");
            return;
        }

        Console.Write("Choose a password: ");
        var password = Console.ReadLine();
        users[username] = password;
        SaveUser(username, password);
        Console.WriteLine("Sign up successful!");
    }

    static void DisplayMenu()
    {
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Display");
            Console.WriteLine("2. Settings");
            Console.WriteLine("3. Exit");

            var choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1":
                    DisplaySubmenu();
                    break;
                case "2":
                    SettingsSubmenu();
                    break;
                case "3":
                    Console.WriteLine("Exiting the menu.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please choose 1, 2, or 3.");
                    break;
            }
        }
    }
    /// <summary>
    /// sufuhfukdh
    /// </summary>
    static void DisplaySubmenu() //ana menu
    {
        while (true)
        {
            Console.WriteLine("\nDisplay Menu:");
            Console.WriteLine("1. Subscription");
            Console.WriteLine("2. Payment");
            Console.WriteLine("3. Reminder");
            Console.WriteLine("4. Expanse Analysis");
            Console.WriteLine("5. Back to Main Menu");

            var choice = Console.ReadLine().Trim();
            var p = new Payment();
            var r = new Reminder();
            var a = new Analysis();
            var s = new Subscriptions();//servis class
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Subscription selected.");
                    s.Display();
                    break;
                case "2":
                    Console.WriteLine("Payment selected.");

                    break;
                case "3":
                    Console.WriteLine("Reminder selected.");

                    break;
                case "4":
                    Console.WriteLine("Expanse Analysis selected.");

                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please choose 1, 2, 3, 4, or 5.");
                    break;
            }
        }
    }

    static void SettingsSubmenu()
    {
        while (true)
        {
            Console.WriteLine("\nSettings Menu:");
            Console.WriteLine("1. Subscription");
            Console.WriteLine("2. Search");
            Console.WriteLine("3. Back to Main Menu");

            var choice = Console.ReadLine().Trim();
            var s = new Subscriptions();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Subscription selected.");
                   
                    s.Display();
                    break;
                case "2":
                    Console.WriteLine("Search selected.");
                 //s.Search();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please choose 1, 2, or 3.");
                    break;
            }
        }
    }

    public static void Main(string[] args)
    {
        var users = LoadUsers();
        while (true)
        {
            Console.WriteLine("Do you want to sign in, sign up, or exit? (sign in/sign up/exit): ");
            var choice = Console.ReadLine().Trim().ToLower();

            switch (choice)
            {
                case "sign in":
                    if (SignIn(users))
                    {
                        DisplayMenu();
                    }
                    break;
                case "sign up":
                    SignUp(users);
                    break;
                case "exit":
                    Console.WriteLine("Exiting the program.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please choose 'sign in', 'sign up', or 'exit'.");
                    break;
            }
        }
    }
}
