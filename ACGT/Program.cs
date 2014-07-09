using System;
using System.Linq;
using System.Collections.Generic;

namespace CodeJam
{
    class ACGT
    {
        int MinChanges(int maxPeriod, string[] acgt)
        {
            String concatinatedString = "";
            List<int> countPerPeriod = new List<int>();
           
            Array.ForEach(acgt, x => concatinatedString += x);

            for (int i = 1; i <= maxPeriod; i++)
            {
                String periodicSet = concatinatedString.Substring(0, i);
                int periodCounter = 0;

                periodCounter = GetNumberOfReplaceCount(concatinatedString, periodicSet, i, periodCounter);

                countPerPeriod.Add(periodCounter);
            }

            countPerPeriod.Sort();

            return countPerPeriod[0];
        }

        private  int GetNumberOfReplaceCount(String concatinatedString, String periodicSet, int i, int periodCounter)
        {
            for (int j = 0; j < concatinatedString.Length && i < concatinatedString.Length; j++)
            {
                int positionToCheck = j + i;

                periodCounter = UpdateCountByPeriodSet(concatinatedString, periodicSet, i, periodCounter, positionToCheck);
            }
            return periodCounter;
        }

        private int UpdateCountByPeriodSet(String concatinatedString, String periodicSet, int i, int periodCounter, int positionToCheck)
        {
            if (positionToCheck < concatinatedString.Length)
            {
                if (!(periodicSet[positionToCheck % i] == concatinatedString[positionToCheck]))
                {
                    periodCounter++;
                }
            }
            return periodCounter;
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            ACGT aCGT = new ACGT();
            String input = Console.ReadLine();
            do
            {
                var inputParts = input.Split('|');
                int maxPeriod = int.Parse(inputParts[0]);
                string[] acgt = inputParts[1].Split(',');
                Console.WriteLine(aCGT.MinChanges(maxPeriod, acgt));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}