using System;
using System.Collections.Generic;
using System.Linq;

namespace ItemCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Item Counter ===");
                Console.WriteLine("1. Count words/strings");
                Console.WriteLine("2. Count integers");
                Console.WriteLine("3. Count doubles");
                Console.WriteLine("4. Count characters");
                Console.WriteLine("5. Count booleans");
                Console.WriteLine("6. Count dates");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option (1-7): ");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        CountStrings();
                        break;
                    case "2":
                        CountIntegers();
                        break;
                    case "3":
                        CountDoubles();
                        break;
                    case "4":
                        CountCharacters();
                        break;
                    case "5":
                        CountBooleans();
                        break;
                    case "6":
                        CountDates();
                        break;
                    case "7":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        
        static void CountStrings()
        {
            Console.WriteLine("\nEnter words/strings separated by spaces:");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }
            
            List<string> items = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            Console.WriteLine("\nCounting strings:");
            CountOccurrences(items);
        }
        
        static void CountIntegers()
        {
            Console.WriteLine("\nEnter integers separated by spaces:");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }
            
            try
            {
                List<int> numbers = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                       .Select(int.Parse)
                                       .ToList();
                Console.WriteLine("\nCounting integers:");
                CountOccurrences(numbers);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid integer format. Please enter valid integers.");
            }
        }
        
        static void CountDoubles()
        {
            Console.WriteLine("\nEnter decimal numbers separated by spaces:");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }
            
            try
            {
                List<double> doubles = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(double.Parse)
                                          .ToList();
                Console.WriteLine("\nCounting doubles:");
                CountOccurrences(doubles);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid decimal format. Please enter valid decimal numbers.");
            }
        }
        
        static void CountBooleans()
        {
            Console.WriteLine("\nEnter boolean values separated by spaces (true/false, yes/no, 1/0):");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }
            
            try
            {
                List<bool> booleans = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(ParseBoolean)
                                         .ToList();
                Console.WriteLine("\nCounting booleans:");
                CountOccurrences(booleans);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid boolean format: {ex.Message}");
            }
        }
        
        static void CountDates()
        {
            Console.WriteLine("\nEnter dates separated by spaces (format: MM/dd/yyyy or yyyy-MM-dd):");
            Console.WriteLine("Example: 01/15/2024 2024-12-25 03/10/2023");
            string input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }
            
            try
            {
                List<DateTime> dates = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(DateTime.Parse)
                                          .ToList();
                Console.WriteLine("\nCounting dates:");
                CountOccurrences(dates);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format. Please use MM/dd/yyyy or yyyy-MM-dd format.");
            }
        }
        
        static bool ParseBoolean(string value)
        {
            string lower = value.ToLower().Trim();
            return lower switch
            {
                "true" or "yes" or "1" => true,
                "false" or "no" or "0" => false,
                _ => throw new FormatException($"'{value}' is not a valid boolean value. Use true/false, yes/no, or 1/0.")
            };
        }
        
        static void CountCharacters()
        {
            Console.WriteLine("\nEnter text (each character will be counted):");
            string input = Console.ReadLine();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }
            
            List<char> characters = input.ToList();
            Console.WriteLine("\nCounting characters:");
            CountOccurrences(characters);
        }
        
        public static void CountOccurrences<T>(List<T> items) where T : notnull
        {
            if (items == null || items.Count == 0)
            {
                Console.WriteLine("The list is empty or null.");
                return;
            }
            
            var counts = items.GroupBy(item => item)
                             .Select(group => new { Item = group.Key, Count = group.Count() });
            
            foreach (var count in counts)
            {
                Console.WriteLine($"{count.Item}: {count.Count} occurrence(s)");
            }
        }
    }
}
