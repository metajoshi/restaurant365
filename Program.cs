using System;
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
            var numbersToAdd = input.Split(',')
                                .Select(
                                    number => {
                                        try
                                        {
                                            return Convert.ToInt32(number);
                                        } catch(Exception e)
                                        {
                                            return 0;
                                        }
                                    })
                                .ToArray();
            if (numbersToAdd.Length > 2)
                throw new FormatException("Please provide only 2 numbers separated by a comma.");
            

            return numbersToAdd;
        }

        protected static int Add(int[] numbersToAdd)
        {
            return numbersToAdd.Sum();
        }
    }
}
