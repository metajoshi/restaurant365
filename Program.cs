using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var numbersToAdd = Sanitize(input);

            Console.WriteLine(Add(numbersToAdd));
            Console.Read();
        }

        private static int[] Sanitize(string input)
        {
            List<int> disAllowedNegativeNumbers = new List<int>();
            List<string> separators = new List<string>{ ",", "\\n" };

            List<string> delimiterRegex = new List<string> { @"(//(.)\\n)", @"(//\[(.+)\]\\n)" };

            foreach (var regexString in delimiterRegex)
            {
                Match foundCustomDelimiter = new Regex(regexString).Match(input);
                if (foundCustomDelimiter.Success)
                {
                    input = input.Split(foundCustomDelimiter.Groups[1].Value, StringSplitOptions.None)[1]; // Split off the custom delimiter piece from input
                    separators.Add(foundCustomDelimiter.Groups[2].Value); // Add custom delimiter to separators
                }
            }

            var numbersToAdd = input.Split(separators.ToArray(), StringSplitOptions.None)
                                .Select(
                                    number => {
                                        try
                                        {
                                            var convertedNumber = Convert.ToInt32(number);
                                            if (convertedNumber < 0)
                                                disAllowedNegativeNumbers.Add(convertedNumber);
                                            if (convertedNumber > 1000)
                                                return 0;
                                            return convertedNumber;
                                        } catch(Exception e)
                                        {
                                            return 0;
                                        }
                                    })
                                .ToArray();

            if (disAllowedNegativeNumbers.Count > 0)
                throw new ArgumentException($"Negative numbers are not allowed: {string.Join(',', disAllowedNegativeNumbers)}");

            return numbersToAdd;
        }

        protected static int Add(int[] numbersToAdd)
        {
            return numbersToAdd.Sum();
        }
    }
}
