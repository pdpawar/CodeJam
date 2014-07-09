using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Codejam
{
    class FriendScore
    {
        public int HighestScore(string[] friends)
        {
            Dictionary<int, List<int>> friendCountMap = new Dictionary<int, List<int>>();

            for (int p = 0; p < friends.Length; p++)
            {
                friendCountMap.Add(p, new List<int>());

                for (int i = 0; i < friends[p].Length; i++)
                {
                    FindFriends(friends, friendCountMap, p, i);
                }
            }

            return GetMaxFriendFromFriendsMap(friendCountMap, 0);
        }

        private void FindFriends(string[] friends, Dictionary<int, List<int>> friendCountMap, int p, int i)
        {
            if (friends[p][i] == 'Y')
            {
                ValidateAndAddInList(friendCountMap, p, i);

                CheckForCommonFriend(friends, friendCountMap, p, i);
            }
        }

        private int GetMaxFriendFromFriendsMap(Dictionary<int, List<int>> friendCountMap, int maxCount)
        {
            friendCountMap.Keys.ToList().ForEach(x =>
            {
                if (maxCount < friendCountMap[x].Count)
                {
                    maxCount = friendCountMap[x].Count;
                };
            });
            return maxCount;
        }

        private void CheckForCommonFriend(string[] friends, Dictionary<int, List<int>> friendCountMap, int p, int i)
        {
            string secondFriend = friends[i];

            for (int j = 0; j < secondFriend.Length; j++)
            {
                if (j != friends.ToList().IndexOf(friends[p]))
                {
                    if (secondFriend[j] == 'Y')
                    {
                        ValidateAndAddInList(friendCountMap, p, j);
                    }
                }
            }
        }

        private void ValidateAndAddInList(Dictionary<int, List<int>> friendCountMap, int p, int j)
        {
            if (!friendCountMap[p].Contains(j))
                friendCountMap[p].Add(j);
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            FriendScore friendScore = new FriendScore();
            do
            {
                string[] values = input.Split(',');
                int validationOp = friendScore.HighestScore(values);
                Console.WriteLine(validationOp);
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}