using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{


  

    public class Payment
    {
        private CardDetails cardDetails;

        public void EnterCardDetails()
        {
            Console.Write("Enter the name on the card (only letters and spaces): ");
            string name = Console.ReadLine();

            Console.Write("Enter the card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter the expiry date (MM/YY, e.g., 11/24): ");
            string expiryDate = Console.ReadLine();

            Console.Write("Enter the CVV (3 digits): ");
            string cvv = Console.ReadLine();

            cardDetails = new CardDetails(name, cardNumber, expiryDate, cvv);

            if (ValidateCardDetails())
            {
                Console.WriteLine("Payment successful!");
            }
            else
            {
                Console.WriteLine("Payment failed! Invalid card details.");
            }
        }

        private bool ValidateCardDetails()
        {
            return ValidateName(cardDetails.Name) &&
                   ValidateCardNumber(cardDetails.CardNumber) &&
                   ValidateExpiryDate(cardDetails.ExpiryDate) &&
                   ValidateCVV(cardDetails.CVV);
        }

        private bool ValidateName(string name)
        {
            // Check if name contains only letters and spaces
            return !string.IsNullOrWhiteSpace(name) && name.All(c => char.IsLetter(c) || c == ' ');
        }

        private bool ValidateCardNumber(string cardNumber)
        {
            // Check if card number is exactly 16 digits in the format 4444 4444 4444 4444
            return System.Text.RegularExpressions.Regex.IsMatch(cardNumber, @"^\d{4} \d{4} \d{4} \d{4}$");
        }

        private bool ValidateExpiryDate(string expiryDate)
        {
            // Check if expiry date is in MM/YY format, e.g., 11/24
            return System.Text.RegularExpressions.Regex.IsMatch(expiryDate, @"^(0[1-9]|1[0-2])/\d{2}$");
        }

        private bool ValidateCVV(string cvv)
        {
            // Check if CVV is exactly 3 digits
            return System.Text.RegularExpressions.Regex.IsMatch(cvv, @"^\d{3}$");
        }

        // Inner class to hold card details
        private class CardDetails
        {
            public string Name { get; }
            public string CardNumber { get; }
            public string ExpiryDate { get; }
            public string CVV { get; }

            public CardDetails(string name, string cardNumber, string expiryDate, string cvv)
            {
                Name = name;
                CardNumber = cardNumber;
                ExpiryDate = expiryDate;
                CVV = cvv;
            }
        }

        // Example usage:
        public static void Main(string[] args)
        {
            Payment payment = new Payment();
            payment.EnterCardDetails();
        }
    }

}
