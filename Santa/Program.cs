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
            int noOfWeeks = prices.Length <= 7 ? 1 : prices.Length / 7;
         
            totalNumberOfDaysSantaCanEat = 0;
            totalbudgetSpent = 0;

            prices.ToList().ForEach(x => listOfSelectedDishBySanta.Add(FindCheapestDishOfDay(x)));

            CalculateTotalDaysForFirstWeek(budget, listOfSelectedDishBySanta);

            CalculateTotalDaysForRestWeeks(prices, budget, listOfSelectedDishBySanta);   
           
            return totalNumberOfDaysSantaCanEat;
        }

        private void CalculateTotalDaysForRestWeeks(string[] prices, int budget, List<char> listOfSelectedDishBySanta)
        {
            for (int currentDay = 8; currentDay <= prices.Length && CheckBudget(budget); currentDay++)
            {
                UpdateBudget(prices, budget, listOfSelectedDishBySanta, currentDay);
            }
        }

        private void CalculateTotalDaysForFirstWeek(int budget, List<char> listOfSelectedDishBySanta)
        {
            listOfSelectedDishBySanta.Select((x, i) => new { Index = i, Value = x }).Where(x => x.Index < 7).ToList().ForEach(selectedDishPrice =>
            {
                totalbudgetSpent += GetPriceOfLetter(selectedDishPrice.Value);
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


        private void UpdateBudget(string[] prices, int budget, List<char> listOfSelectedDishBySanta, int currentDay)
        {
            List<int> priceOfDishForAllWeek = new List<int>();
            List<int> sumOfDishes = new List<int>();

            int dayInFirstWeek = (currentDay - 1) % 7;

            int indexOfCheapestDishSelected = prices[dayInFirstWeek].IndexOf(listOfSelectedDishBySanta[dayInFirstWeek]);

            UpdateListOfTotalAmountToSpendOnDishForADay(prices, currentDay, sumOfDishes, dayInFirstWeek);

            if (sumOfDishes.IndexOf(sumOfDishes.Min()) != indexOfCheapestDishSelected)
            {
                ChangeDishAsPerMinTotalAmountToSpendOnDishForADay(prices, budget, listOfSelectedDishBySanta, currentDay, sumOfDishes, dayInFirstWeek);
            }
            else 
            {
                UpdateBudgetForADay(prices, budget, listOfSelectedDishBySanta, currentDay, indexOfCheapestDishSelected);
            }
        }

        private void UpdateBudgetForADay(string[] prices, int budget, List<char> listOfSelectedDishBySanta, int currentDay, int indexOfCheapestDishSelected)
        {
            totalbudgetSpent += GetPriceOfLetter(prices[currentDay - 1][indexOfCheapestDishSelected]);
            listOfSelectedDishBySanta[currentDay - 1] = prices[currentDay - 1][indexOfCheapestDishSelected];
            if (CheckBudget(budget))
                totalNumberOfDaysSantaCanEat++;
        }

        private void ChangeDishAsPerMinTotalAmountToSpendOnDishForADay(string[] prices, int budget, List<char> listOfSelectedDishBySanta, int currentDay, List<int> sumOfDishes, int dayInFirstWeek)
        {
            for (int dayOfWeek = dayInFirstWeek; dayOfWeek < currentDay - 1; dayOfWeek += 7)
                totalbudgetSpent -= GetPriceOfLetter(listOfSelectedDishBySanta[dayOfWeek]);

            totalbudgetSpent += sumOfDishes.Min();

            if (CheckBudget(budget))
            {
                totalNumberOfDaysSantaCanEat++;

                UpdateListOfDishSelected(prices, listOfSelectedDishBySanta, currentDay, sumOfDishes, dayInFirstWeek);
            }
        }

        private void UpdateListOfDishSelected(string[] prices, List<char> listOfSelectedDishBySanta, int currentDay, List<int> sumOfDishes, int dayInFirstWeek)
        {
            listOfSelectedDishBySanta[dayInFirstWeek] = prices[dayInFirstWeek][sumOfDishes.IndexOf(sumOfDishes.Min())];

            for (int dayOfWeek = dayInFirstWeek; dayOfWeek < currentDay; dayOfWeek += 7)
                listOfSelectedDishBySanta[dayOfWeek] = prices[dayOfWeek][sumOfDishes.IndexOf(sumOfDishes.Min())];
        }

        private void UpdateListOfTotalAmountToSpendOnDishForADay(string[] prices, int currentDay, List<int> sumOfDishes, int dayInFirstWeek)
        {
            for (int i = 0; i < prices[dayInFirstWeek].Length; i++)
            {
                int sum = 0;
                for (int dayOfWeek = dayInFirstWeek; dayOfWeek < currentDay; dayOfWeek += 7)
                {
                    sum += GetPriceOfLetter(prices[dayOfWeek][i]);
                }

                sumOfDishes.Add(sum);
            }
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

        public int GetPriceOfLetter(char letter)
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