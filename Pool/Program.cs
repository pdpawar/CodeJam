using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;

namespace CodeJam
{
    class Pool
    {
        int numberOfSwipe = 0;

        List<int> ruleOne = new List<int>(){1,6,8,11,13};
        List<int> ruleTwo = new List<int>(){0,3,5,7,9,10,12};

        List<int> ballsForRuleOne = new List<int>();
        List<int> ballsForRuleTwo = new List<int>();

       
        int RackMoves(int[] triangle)
        {
            reset();

            ValidateRuleOne(triangle);

            SetBallsByRules(triangle);

            int ruleTwoSwipCount = validateRuleTwo();
            int ruleThreeSwipCount = validateRuleThree();
            numberOfSwipe += (ruleTwoSwipCount + ruleThreeSwipCount) / 2 + (ruleTwoSwipCount + ruleThreeSwipCount) % 2;

            return numberOfSwipe;
        }

        private void reset()
        {
            ballsForRuleOne.Clear();
            ballsForRuleTwo.Clear();
            numberOfSwipe = 0;
        }

        private void SetBallsByRules(int[] triangle)
        {
            foreach (int ruleOnePosition in ruleOne)
            {
                ballsForRuleOne.Add(triangle[ruleOnePosition]);
            }

            foreach (int ruleTwoPosition in ruleTwo)
            {
                ballsForRuleTwo.Add(triangle[ruleTwoPosition]);
            }
        }

        bool IsSolid(int ball)
        {   
            return (ball > 8 ? true : false);
        }

        bool IsStripes(int ball)
        {
            return !IsSolid(ball);
        }

        private void ValidateRuleOne(int[] triangle)
        {            
            if (triangle[4] != 8)
            {
                int i = Array.IndexOf(triangle, 8);
                int temp = triangle[4];
                triangle[i] = temp;
                triangle[4] = 8;
                numberOfSwipe++;
            }
        }
       
        private int validateRuleTwo()
        {
            int solidBallCount=0;
            foreach (int ball in ballsForRuleOne)
            {
                if(IsSolid(ball)) solidBallCount++;
            }

            return (solidBallCount > (ballsForRuleOne.Count - solidBallCount)) ? (ballsForRuleOne.Count - solidBallCount) : solidBallCount;
        }

        private int validateRuleThree()
        {
            int stripesBallCount = 0;
            foreach (int ball in ballsForRuleTwo)
            {
                if (IsStripes(ball)) stripesBallCount++;
            }
            return (stripesBallCount > (ballsForRuleTwo.Count - stripesBallCount)) ? (ballsForRuleTwo.Count - stripesBallCount) : stripesBallCount;
        }


        #region Testing code Do not change
        public static void Main(String[] args)
        {
            Pool pool = new Pool();
            String input = Console.ReadLine();
            do
            {
                int[] triangle = Array.ConvertAll<string, int>(input.Split(','), delegate(string s) { return Int32.Parse(s); });
                Console.WriteLine(pool.RackMoves(triangle));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion

    }
}