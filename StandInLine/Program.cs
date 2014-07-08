using System;
using System.Collections.Generic;
using System.Text;

namespace Codejam
{
    class StandInLine
    {
        List<int> positionsToFill = new List<int>();
        
        int[] Reconstruct(int[] left)
        {
            List<int> final = new List<int>();
            
            positionsToFill.Clear();

            for (int elementCounter =1 ;elementCounter <=left.Length; elementCounter++)
            {
                positionsToFill.Add(elementCounter);
                final.Add(0);
            }

            for(int soldierHeight = 1; soldierHeight <= left.Length; soldierHeight++)
            {
             int emptyCounter = 1;
             int positionToPut = positionsToFill[left[soldierHeight - 1]];

             for (int index = 0; index <= final.Count-1; index++)
             {
                 if (final[index] == 0)
                 {
                     if (emptyCounter == positionToPut)
                     {
                         final[index] = soldierHeight;
                     }
                     emptyCounter++;

                 }
             }
            }

            return final.ToArray();
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            StandInLine standInLine = new StandInLine();
            do
            {
                int[] left = Array.ConvertAll<string, int>(input.Split(','), delegate(string s) { return Int32.Parse(s); });
                Console.WriteLine(string.Join(",", Array.ConvertAll<int, string>(standInLine.Reconstruct(left), delegate(int s) { return s.ToString(); })));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}
