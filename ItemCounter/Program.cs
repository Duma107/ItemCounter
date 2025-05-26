using System;
using System.Collections.Generic;
using System.Linq;

namespace ItemCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> fruits = new List<string> { "apple", "banana", "apple", "orange", "banana", "apple", "grape" };
            Console.WriteLine("Counting fruits:");
            CountOccurrences(fruits);

            List<int> numbers = new List<int> { 1, 2, 3, 2, 1, 4, 5, 1, 2, 6 };
            Console.WriteLine("\nCounting numbers:");
            CountOccurrences(numbers);

            List<char> characters = new List<char> { 'a', 'b', 'a', 'c', 'd', 'a', 'b', 'e' };
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