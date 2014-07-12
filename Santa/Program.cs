using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Codejam
{
    class SantaDaDhaba
    {
        int upperLetterBasePrice = 10;
        int lowerLetterBasePrice = 36;
        int totalbudgetSpent;
        int totalNumberOfDaysSantaCanEat;

        int MaxDays(string[] prices, int budget)
        {
            List<char> listOfSelectedDishBySanta = new List<char>();
            List<int> listOfSelectedDishIndexBySanta = new List<int>();
            int noOfWeeks = prices.Length <= 7 ? 1 : prices.Length / 7;
         
            totalNumberOfDaysSantaCanEat = 0;
            totalbudgetSpent = 0;
           
            prices.ToList().ForEach(x => listOfSelectedDishIndexBySanta.Add(FindIndexOfCheapestDishOfDay(x)));
  
            CalculateTotalDaysForFirstWeek(budget, listOfSelectedDishIndexBySanta, prices);
           
            CalculateTotalDaysForRestWeeks(prices, budget, listOfSelectedDishIndexBySanta); 
           
            return totalNumberOfDaysSantaCanEat;
        }

        private void CalculateTotalDaysForRestWeeks(string[] prices, int budget, List<int> listOfSelectedDishIndexBySanta)
        {
            for (int currentDay = 8; currentDay <= prices.Length && CheckBudget(budget); currentDay++)
            {
                UpdateBudget(prices, budget, listOfSelectedDishIndexBySanta, currentDay);
            }
        }

    
        private void CalculateTotalDaysForFirstWeek(int budget, List<int> listOfSelectedDishIndexBySanta, string[] prices)
        {
            listOfSelectedDishIndexBySanta.Select((x, i) => new { Index = i, Value = x }).Where(x => x.Index < 7).ToList().ForEach(selectedDishPriceIndex =>
            {
                totalbudgetSpent += GetPriceOfLetter(prices[selectedDishPriceIndex.Index][selectedDishPriceIndex.Value]);
                if (CheckBudget(budget))
                {
                    totalNumberOfDaysSantaCanEat++;
                }
            });
        }

        private bool CheckBudget(int budget)
        {
            return totalbudgetSpent <= budget;
        }

        private void UpdateBudget(string[] prices, int budget, List<int> listOfSelectedDishIndexBySanta, int currentDay)
        { 
            List<int> sumOfDishes = new List<int>();

            int dayInFirstWeek = (currentDay - 1) % 7;

            int indexOfCheapestDishSelected = listOfSelectedDishIndexBySanta[dayInFirstWeek];

            UpdateListOfTotalAmountToSpendOnDishForADay(prices, currentDay, sumOfDishes, dayInFirstWeek);

            if (sumOfDishes.IndexOf(sumOfDishes.Min()) != indexOfCheapestDishSelected)
            {
                ChangeDishAsPerMinTotalAmountToSpendOnDishForADay(prices, budget, listOfSelectedDishIndexBySanta, currentDay, sumOfDishes, dayInFirstWeek);
            }
            else
            {
                UpdateBudgetForADay(prices, budget, listOfSelectedDishIndexBySanta, currentDay, indexOfCheapestDishSelected);
            }
        }

        private void UpdateBudgetForADay(string[] prices, int budget, List<int> listOfSelectedDishIndexBySanta, int currentDay, int indexOfCheapestDishSelected)
        {
            totalbudgetSpent += GetPriceOfLetter(prices[currentDay - 1][indexOfCheapestDishSelected]);
            listOfSelectedDishIndexBySanta[currentDay - 1] = indexOfCheapestDishSelected;
            if (CheckBudget(budget))
                totalNumberOfDaysSantaCanEat++;
        }

        private void ChangeDishAsPerMinTotalAmountToSpendOnDishForADay(string[] prices, int budget, List<int> listOfSelectedDishIndexBySanta, int currentDay, List<int> sumOfDishes, int dayInFirstWeek)
        {
            for (int dayOfWeek = dayInFirstWeek; dayOfWeek < currentDay - 1; dayOfWeek += 7)
                totalbudgetSpent -= GetPriceOfLetter(prices[dayOfWeek][listOfSelectedDishIndexBySanta[dayOfWeek]]);

            totalbudgetSpent += sumOfDishes.Min();

            if (CheckBudget(budget))
            {
                totalNumberOfDaysSantaCanEat++;

                UpdateListOfDishSelected(prices, listOfSelectedDishIndexBySanta, currentDay, sumOfDishes, dayInFirstWeek);
            }
        }

        private void UpdateListOfDishSelected(string[] prices, List<int> listOfSelectedDishIndexBySanta, int currentDay, List<int> sumOfDishes, int dayInFirstWeek)
        {
            listOfSelectedDishIndexBySanta[dayInFirstWeek] = sumOfDishes.IndexOf(sumOfDishes.Min());

            for (int dayOfWeek = dayInFirstWeek; dayOfWeek < currentDay; dayOfWeek += 7)
                listOfSelectedDishIndexBySanta[dayOfWeek] = sumOfDishes.IndexOf(sumOfDishes.Min());
        }

        private void UpdateListOfTotalAmountToSpendOnDishForADay(string[] prices, int currentDay, List<int> sumOfDishes, int dayInFirstWeek)
        {
            int sum;
            for (int i = 0; i < prices[dayInFirstWeek].Length; i++)
            {
                sum = 0;
                for (int dayOfWeek = dayInFirstWeek; dayOfWeek < currentDay; dayOfWeek += 7)
                {
                    sum += GetPriceOfLetter(prices[dayOfWeek][i]);
                }

                sumOfDishes.Add(sum);
            }
        }

        int FindIndexOfCheapestDishOfDay(string priceValue)
        {
            int cheapestDishIndex = 0;

            for (int i = 1; i < priceValue.Length; i++)
            {
                if (GetPriceOfLetter(priceValue[i]) < GetPriceOfLetter(priceValue[cheapestDishIndex]))
                {
                    cheapestDishIndex = i;
                }
            }

            return cheapestDishIndex;
        }

        char FindCheapestDishOfDay(string priceValue)
        {
            char cheapestDish = priceValue[0];

            for (int i = 1; i < priceValue.Length; i++)
            {
                if (GetPriceOfLetter(priceValue[i]) < GetPriceOfLetter(cheapestDish))
                {
                    cheapestDish = priceValue[i];
                }
            }

            return cheapestDish;
        }


        int GetPriceForUpperLetter(char upperLetter)
        {
            int diffByAsciiValue = upperLetter - 'A';

            return upperLetterBasePrice + diffByAsciiValue;
        }

        int GetPriceForLowerLetter(char lowerLetter)
        {
            int diffByAsciiValue = lowerLetter - 'a';

            return lowerLetterBasePrice + diffByAsciiValue;
        }

        private int GetPriceOfLetter(char letter)
        {
            if (Char.IsNumber(letter))
                return Int32.Parse(letter + "");
            else
                return char.IsUpper(letter) ? GetPriceForUpperLetter(letter) : GetPriceForLowerLetter(letter);
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            SantaDaDhaba dhaba = new SantaDaDhaba();

            do
            {
                var inputParts = input.Split('|');
                string[] prices = inputParts[0].Split(',');
                Console.WriteLine(dhaba.MaxDays(prices, Int32.Parse(inputParts[1])));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}