using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Codejam
{
    class TriFibonacci
    {
        public int Complete(int[] test)
        {
            int missingElementPosition = test.ToList().IndexOf(-1);

            GetMissingElement(test, missingElementPosition);
            
            if (!validate(test.ToList()))
            {
                return -1;
            }

            return test[missingElementPosition] > 0? test[missingElementPosition] :- 1;
        }

        private void GetMissingElement(int[] test, int missingElementPosition)
        {
            if (missingElementPosition >= 3)
            {
                test[missingElementPosition] = test[missingElementPosition - 1] + test[missingElementPosition - 2] + test[missingElementPosition - 3];
            }
            else
            {
                test[missingElementPosition] = test[3] - (test[0] + test[1] + test[2] + 1);
            }
        }

        private bool validate(List<int> fibonacciSeries)
        {
            for (int i = fibonacciSeries.Count-1; i >= 3; i--)
            {
                int expectedElement = fibonacciSeries[i - 1] + fibonacciSeries[i - 2] + fibonacciSeries[i - 3];
                if(expectedElement != fibonacciSeries[i])
                {
                    return false;
                }
            }
          return true;
        }

      #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            TriFibonacci triFibonacci = new TriFibonacci();
            do
            {
				string[] values = input.Split(',');
                int[] numbers = Array.ConvertAll<string, int>(values, delegate(string s) { return Int32.Parse(s); });
                int result = triFibonacci.Complete(numbers);
                Console.WriteLine(result);
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}