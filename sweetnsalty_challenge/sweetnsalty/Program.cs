using System;

namespace sweetnsalty
{
    class Program
    {

        static string sweet = "sweet";
        static string salty = "salty";
        static string sweetnsalty = "sweetnsalty";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Sweet n Salty");
            //Initializing needed variables
            var min = 0;
            var max = 1000;
            var wordsPerLine = 10;
            LoopPrintAndCount(min, max, wordsPerLine);
        }

        public static void LoopPrintAndCount(int min, int max, int wordsPerLine)
        {
            // initializing counters
            var firstNumber = 3;
            var secondNumber = 5;
            var sweetCount = 0;
            var saltyCount = 0;
            var sweetnsaltyCount = 0;
            for (int value = min; value < max; value++)
            {
                if (value % firstNumber == 0 && value % secondNumber == 0)
                {
                    Console.Write(sweetnsalty + " ");
                    sweetnsaltyCount++;
                }
                else if (value % firstNumber == 0)
                {
                    Console.Write(sweet + " ");
                    sweetCount++;
                }
                else if (value % secondNumber == 0)
                {
                    Console.Write(salty + " ");
                    saltyCount++;
                }
                else
                {
                    Console.Write(value + " ");
                }
                if (value > 0 && value % wordsPerLine == 0)
                {
                    Console.Write("\n");
                }
            }
            PrintResultCounts(sweetCount, saltyCount, sweetnsaltyCount);
        }

        public static void PrintResultCounts(int sweetCount, int saltyCount, int sweetnsaltyCount)
        {
            Console.WriteLine("the number of " + sweet + "s: " + sweetCount);
            Console.WriteLine("the number of " + salty + "s: " + saltyCount);
            Console.WriteLine("the number of " + sweetnsalty + "s: " + sweetnsaltyCount);
        }

        // public static int GetNumberInput()
        // {
        //     bool success = false;
        //     int num = 0;
        //     while (!success)
        //     {
        //         Console.WriteLine("please enter a number");
        //         success = Int32.TryParse(Console.ReadLine(), out num);
        //     }
        //     return num;
        // }


    }
}
