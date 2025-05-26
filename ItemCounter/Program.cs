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
                Console.WriteLine("2. Count numbers");
                Console.WriteLine("3. Count characters");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option (1-4): ");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        CountStrings();
                        break;
                    case "2":
                        CountNumbers();
                        break;
                    case "3":
                        CountCharacters();
                        break;
                    case "4":
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
        
        static void CountNumbers()
        {
            Console.WriteLine("\nEnter numbers separated by spaces:");
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
                Console.WriteLine("\nCounting numbers:");
                CountOccurrences(numbers);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid number format. Please enter valid integers.");
            }
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
