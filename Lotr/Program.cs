using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;

namespace CodeJam
{
    class LOTR
    {
        int GetMinimum(int[] replies)
        {  
            int finalCount=0;
            Dictionary<int, int> hobbitCount = new Dictionary<int, int>();
            List<int> ignoreList = new List<int>();

            SetHobbitDictionary(replies, hobbitCount, ignoreList);

            finalCount = GetFinalHobbitCount(finalCount, hobbitCount);

            return finalCount;
        }

        private static int GetFinalHobbitCount(int finalCount, Dictionary<int, int> hobbitCount)
        {
            foreach (int key in hobbitCount.Keys)
            {
                finalCount = GetCountByHeight(finalCount, hobbitCount, key);
            }
            return finalCount;
        }

        private static int GetCountByHeight(int finalCount, Dictionary<int, int> hobbitCount, int key)
        {
            int noOfHobbit = key + 1;

            int groupCount = hobbitCount[key] / noOfHobbit;

            if (hobbitCount[key] % noOfHobbit != 0)
            {
                finalCount += groupCount * noOfHobbit + noOfHobbit;
            }
            else
            {
                finalCount += groupCount * noOfHobbit;
            }
            return finalCount;
        }

        private static void SetHobbitDictionary(int[] replies, Dictionary<int, int> hobbitCount, List<int> ignoreList)
        {
            for (int i = 0; i < replies.Length; i++)
            {
                int x = replies[i];

                if (ignoreList.Contains(x))
                    continue;

                ignoreList.Add(x);
                int y = 0;
                for (int j = 0; j < replies.Length; j++)
                {
                    if (x == replies[j])
                    {
                        y++;
                    }
                }

                hobbitCount.Add(x, y);
            }
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            LOTR lotr = new LOTR();
            String input = Console.ReadLine();
            do
            {
                int[] replies = Array.ConvertAll<string, int>(input.Split(','), delegate(string s) { return Int32.Parse(s); });
                Console.WriteLine(lotr.GetMinimum(replies));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion

    }
}