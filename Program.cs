using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public static class Calculator
    {
        public static int PerformAddition(string input, bool allowNegativeNumbers, bool limitTwoArguments, bool numberLessThanThousand)
        {
            const string SINGLE_SEPARATOR_REGEX = @"(//(.)\\n)", MULTI_SEPARATOR_REGEX = @"(//(?:\[(.*?)\])+?\\n)";
            List<string> customSeparatorRegexes = new List<string> { SINGLE_SEPARATOR_REGEX, MULTI_SEPARATOR_REGEX };
            string separatorString, argumentString;

            SplitSeparatorsFromArguments(input, customSeparatorRegexes, out separatorString, out argumentString);
            var separators = ExtractSeparatorsFromInput(separatorString, customSeparatorRegexes);
            var arguments = ExtractArgumentsFromInput(argumentString, allowNegativeNumbers, limitTwoArguments, numberLessThanThousand, separators);
            return Add(arguments);
        }

        static void SplitSeparatorsFromArguments(string input, IEnumerable<string> customSeparatorRegexes, out string separatorString, out string argumentString)
        {
            string localSeparators = "", localArguments = input;
            customSeparatorRegexes.ToList().ForEach(regexString =>
            {
                List<Match> matches = new Regex(regexString).Matches(input).ToList();
                if (matches.Count > 0 && matches[0].Groups.Count > 0)
                {
                    localArguments = input.Split(matches[0].Groups[1].Value, StringSplitOptions.None)[1];
                    localSeparators = input.Replace(localArguments, "");
                }
            });

            separatorString = localSeparators;
            argumentString = localArguments;
        }

        static List<string> ExtractSeparatorsFromInput(string input, IEnumerable<string> customSeparatorRegexes)
        {
            List<string> defaultSeparators = new List<string> { ",", "\\n" };

            customSeparatorRegexes.ToList().ForEach(regexString =>
            {
                List<Match> matches = new Regex(regexString).Matches(input).ToList();
                if (matches.Count > 0 && matches[0].Groups.Count > 0)
                {
                    matches.ForEach(match =>
                    {
                        if (match.Success)
                        {
                            foreach (var capture in match.Groups[2].Captures.ToList())
                            {
                                defaultSeparators.Add(capture.Value); // Add custom separator to separator list
                            }
                        }
                    });
                }
            });

            return defaultSeparators;
        }

        static int[] ExtractArgumentsFromInput(string input, bool allowNegativeNumbers, bool limitTwoArguments, bool numberLessThanThousand, IEnumerable<string> separators)
        {
            List<int> negativeNumbersInInput = new List<int>();
            var numbersToAdd = input.Split(separators.ToArray(), StringSplitOptions.None)
                                .Select(
                                    number => {
                                        try
                                        {
                                            var convertedNumber = Convert.ToInt32(number);
                                            if (allowNegativeNumbers && convertedNumber < 0)
                                                negativeNumbersInInput.Add(convertedNumber);
                                            if (numberLessThanThousand && convertedNumber > 1000)
                                                return 0;
                                            return convertedNumber;
                                        }
                                        catch (Exception e)
                                        {
                                            return 0;
                                        }
                                    })
                                .ToArray();

            if (limitTwoArguments && numbersToAdd.Length > 2)
                throw new ArgumentException("Only 2 arguments are allowed");

            if (negativeNumbersInInput.Count > 0)
                throw new ArgumentException($"Negative numbers are not allowed: {string.Join(',', negativeNumbersInInput)}");

            return numbersToAdd;
        }

        static int Add(int[] arguments)
        {
            return arguments.Sum();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            bool allowNegativeNumbers = false, limitTwoArguments = false, numberLessThanThousand = false;
            var result = Calculator.PerformAddition(input, allowNegativeNumbers, limitTwoArguments, numberLessThanThousand);
            Console.WriteLine(result);
            Console.Read();
        }


    }
}
