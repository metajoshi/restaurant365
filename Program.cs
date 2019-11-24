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
            string[] separators = { ",", "\\n" };
            var numbersToAdd = input.Split(separators, StringSplitOptions.None)
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

            return numbersToAdd;
        }

        protected static int Add(int[] numbersToAdd)
        {
            return numbersToAdd.Sum();
        }
    }
}
