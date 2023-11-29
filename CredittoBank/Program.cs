
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CredittoBank
{
    class Program
    {
        private static string creditCardNumber;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter credit card number: ");
            string creditCardNumber = Console.ReadLine();

            Console.WriteLine("Enter expiration date (MM/YY): ");
            string expirationDate = Console.ReadLine();

            Console.WriteLine("Enter CVV: ");
            string cvv = Console.ReadLine();

            Console.WriteLine("Enter amount to pay: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            // Validate payment details
            bool isValidPayment = ValidatePaymentDetails(creditCardNumber, expirationDate, cvv);

            if (!isValidPayment)
            {
                Console.WriteLine("Invalid payment details. Please check your information and try again.");
                return;
            }

            // Simulate payment processing
            bool paymentSuccessful = ProcessPayment(creditCardNumber, expirationDate, cvv, amount);

            if (paymentSuccessful)
            {
                Console.WriteLine("Payment successful! Thank you for your purchase.");
            }
            else
            {
                Console.WriteLine("Payment failed. Please check your payment details and try again.");
            }
        }

        static bool ValidatePaymentDetails(string creditCardNumber, string expirationDate, string cvv)
        {
            // Validate credit card number using the Luhn algorithm
            if (!IsCreditCardNumberValid(creditCardNumber))
            {
                Console.WriteLine("Invalid credit card number.");
                return false;
            }

            // Validate expiration date format and check if it's in the future
            if (!IsExpirationDateValid(expirationDate))
            {
                Console.WriteLine("Invalid expiration date.");
                return false;
            }

            // Validate CVV length
            if (!IsCvvValid(cvv, creditCardNumber))
            {
                Console.WriteLine("Invalid CVV.");
                return false;
            }

            return true;
        }

        static bool IsCreditCardNumberValid(string creditCardNumber)
        {
            //throw new NotImplementedException();
            if (string.IsNullOrWhiteSpace(creditCardNumber))
            {
                return false;
            }
            // Remove any spaces or non-numeric characters
            creditCardNumber = new string(creditCardNumber.Where(char.IsDigit).ToArray());

            int sum = 0;
            bool isSecondDigit = false;
            // Iterate through the credit card number digits in reverse order
            for (int i = creditCardNumber.Length - 1; i >= 0; i--)
            {
                int digit = creditCardNumber[i] - '0';

                if (isSecondDigit)
                {
                    // Double every second digit
                    digit *= 2;

                    // If the doubling resulted in a two-digit number, add the digits
                    if (digit > 9)
                    {
                        digit = digit % 10 + 1;
                    }
                }
                // Add the current digit to the sum
                sum += digit;
                // Toggle the flag for the next iteration
                isSecondDigit = !isSecondDigit;
            }
            // The credit card number is valid if the sum is a multiple of 10
            return sum % 10 == 0;
        }

        static bool IsExpirationDateValid(string expirationDate)
        {
            // Implement expiration date validation logic here
            // Check format (MM/YY) and ensure it's in the future
            //throw new NotImplementedException();
            if (string.IsNullOrWhiteSpace(expirationDate))
            {
                return false;
            }

            // Check the format (MM/YY)
            if (!Regex.IsMatch(expirationDate, @"^\d{2}/\d{2}$"))
            {
                return false;
            }

            // Extract month and year from the input
            string[] dateParts = expirationDate.Split('/');

            if (dateParts.Length != 2)
            {
                return false;
            }

            int month, year;

            // Ensure month and year are valid integers
            if (!int.TryParse(dateParts[0], out month) || !int.TryParse(dateParts[1], out year))
            {
                return false;
            }

            // Ensure the date is in the future
            var currentDate = DateTime.Now;
            var inputDate = new DateTime(2000 + year, month, 1).AddMonths(1).AddDays(-1); // Set day to last day of the month

            return inputDate > currentDate;
        }

        static bool IsCvvValid(string cvv, string creditCardNumber)
        {
            // Implement CVV length validation logic here
            // Check if the length matches the expected length for the card type
            //throw new NotImplementedException();

            if (string.IsNullOrWhiteSpace(cvv))
            {
                return false;
            }

            // Remove any spaces or non-numeric characters
            cvv = new string(cvv.Where(char.IsDigit).ToArray());

            // Determine the expected length based on the card type
            int expectedLength;
            if (!IsAmericanExpress(creditCardNumber))
            {
                expectedLength = 3; // Most other cards have CVV with 3 digits
            }
            else
            {
                expectedLength = 4; // American Express CVV has 4 digits
            }

            // Check if the length matches the expected length
            return cvv.Length == expectedLength;
        }

        private static bool IsAmericanExpress(string creditCardNumber)
        {
            //throw new NotImplementedException();
            // Remove any spaces or non-numeric characters
            creditCardNumber = new string(creditCardNumber.Where(char.IsDigit).ToArray());

            // Check if the card number matches the pattern of American Express
            return Regex.IsMatch(creditCardNumber, @"^3[47][0-9]{13}$");
        }

        static bool ProcessPayment(string creditCardNumber, string expirationDate, string cvv, decimal amount)
        {
            // In a real-world scenario, you would integrate with a payment gateway here.
            // This is just a simulation, so we'll consider any payment with a valid credit card number as successful.

            // Simulate a successful payment
            return true;
        }
    }
}
























//using System;

//namespace CredittoBank
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Enter credit card number: ");
//            string creditCardNumber = Console.ReadLine();

//            Console.WriteLine("Enter expiration date (MM/YY): ");
//            string expirationDate = Console.ReadLine();

//            Console.WriteLine("Enter CVV: ");
//            string cvv = Console.ReadLine();

//            Console.WriteLine("Enter amount to pay: ");
//            decimal amount = Convert.ToDecimal(Console.ReadLine());

//            // Simulate payment processing
//            bool paymentSuccessful = ProcessPayment(creditCardNumber, expirationDate, cvv, amount);

//            if (paymentSuccessful)
//            {
//                Console.WriteLine("Payment successful! Thank you for your purchase.");
//            }
//            else
//            {
//                Console.WriteLine("Payment failed. Please check your payment details and try again.");
//            }
//        }

//        static bool ProcessPayment(string creditCardNumber, string expirationDate, string cvv, decimal amount)
//        {
//            // In a real-world scenario, you would integrate with a payment gateway here.
//            // This is just a simulation, so we'll consider any payment with a valid credit card number as successful.

//            // Perform some basic validation (you may want to use a library for real-world scenarios)
//            if (string.IsNullOrWhiteSpace(creditCardNumber) || string.IsNullOrWhiteSpace(expirationDate) || string.IsNullOrWhiteSpace(cvv))
//            {
//                Console.WriteLine("Invalid payment details.");
//                return false;
//            }

//            // Additional validation can be added here (e.g., check card expiration date, CVV length, etc.)

//            // Simulate a successful payment
//            return true;
//        }
//    }
//}
