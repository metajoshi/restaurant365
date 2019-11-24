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

            List<string> delimiterRegexes = new List<string> { @"(//(.)\\n)", @"(//(?:\[(.*?)\])+?\\n)" };
            const int INPUT_GROUP_INDEX = 1, DELIMITER_GROUP_INDEX = 2;
            string splitInputFromDelimiters = input;


            delimiterRegexes.ForEach(regexString =>
            {
                List<Match> matches = new Regex(regexString).Matches(input).ToList();
                if (matches.Count > 0 && matches[0].Groups.Count > 0)
                {
                    splitInputFromDelimiters = input.Split(matches[0].Groups[INPUT_GROUP_INDEX].Value, StringSplitOptions.None)[1]; // Split off the custom delimiter piece from input
                    matches.ForEach(match =>
                    {
                        if (match.Success)
                        {
                            foreach (var capture in match.Groups[DELIMITER_GROUP_INDEX].Captures.ToList())
                            {
                                separators.Add(capture.Value); // Add custom delimiter to separators
                            }
                        }
                    });
                }
            });

            var numbersToAdd = splitInputFromDelimiters.Split(separators.ToArray(), StringSplitOptions.None)
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
