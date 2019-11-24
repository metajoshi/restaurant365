using System;
using System.Collections.Generic;
using System.Linq;

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
            string[] separators = { ",", "\\n" };

            var numbersToAdd = input.Split(separators, StringSplitOptions.None)
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
