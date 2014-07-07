using System;
using System.Collections.Generic;
using System.Text;

namespace Codejam
{
    class Dating
    {
        /**
         * to maintain upper/lower case letters based on HOTTEST priority
         */ 
        List<Char> upperCaseLetterList= new List<Char>();
        List<Char> lowerCaseLetterList = new List<Char>();
        List<Char> originalString = new List<Char>();

        /**
         * to maintain start index and character after every pair selection
         */ 
        int startIndex = 0;
        char startChar;

        /**
         * to get index of chooser character 
         */ 
        private int GetIndexOfChooser(String updatedCircle,  int k)
        {
            int chooserIndex = startIndex + k;
            chooserIndex = (chooserIndex) % updatedCircle.Length;
            return chooserIndex;
        }

        /**
         * to update remove characters after pair is selected
         */ 
        private string removeCharacters(int chooserIndex, int chosenIndex)
        {
            originalString.RemoveAt(chooserIndex);
            originalString.RemoveAt(chosenIndex==0? (0) : (chosenIndex - 1));
            return convertToString(originalString);
        }

        /**
         * to convert updated list to string after each pair is selected
         */
        private string convertToString(List<char> originalString)
        {
            string updatedString = "";
            foreach (Char character in originalString)
            {
                updatedString += character;
            }
            return updatedString;
        }

        /**
         * to maintain list of upper case and lower case letters 
         */
        private void GetUpperCaseAndLowerCaseList(string circle)
        {
            foreach (Char alphabet in circle)
            {
                if (Char.IsUpper(alphabet))
                {
                    upperCaseLetterList.Add(alphabet);
                    upperCaseLetterList.Sort();
                }
                else
                {
                    lowerCaseLetterList.Add(alphabet);
                    lowerCaseLetterList.Sort();
                }
                originalString.Add(alphabet);
            }
        }

        private void generateDates(ref String circle, ref string dates, int chooserIndex)
        {
            startChar = (chooserIndex == circle.Length - 1 ? circle[0] : circle[chooserIndex + 1]);
          
            if (Char.IsLower(circle, chooserIndex))
            {
                chooseMale(ref circle, ref dates, chooserIndex);
            }
            else
            {
                chooseFemale(ref circle, ref dates, chooserIndex);           
            }
        }

        private void chooseFemale(ref String circle, ref string dates, int chooserIndex)
        {
            dates += circle[chooserIndex] + "" + lowerCaseLetterList[0] + " ";
            upperCaseLetterList.Remove(circle[chooserIndex]);
            circle = removeCharacters(chooserIndex, circle.IndexOf(lowerCaseLetterList[0]));
            lowerCaseLetterList.RemoveAt(0);
        }

        private void chooseMale(ref String circle, ref string dates, int chooserIndex)
        {
            dates += circle[chooserIndex] + "" + upperCaseLetterList[0] + " ";
            lowerCaseLetterList.Remove(circle[chooserIndex]);
            circle = removeCharacters(chooserIndex, circle.IndexOf(upperCaseLetterList[0]));
            upperCaseLetterList.RemoveAt(0);
        }

        private bool validatePriorityList()
        {
            if (upperCaseLetterList.Count < 1 || lowerCaseLetterList.Count < 1)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        String Dates(String circle, int k)
        {
            string dates = string.Empty;

            GetUpperCaseAndLowerCaseList(circle);

            if (validatePriorityList())
            {
                startIndex = 0;
                while (circle.Length > 1)
                {
                    int chooserIndex = GetIndexOfChooser(circle, k - 1);
                    generateDates(ref circle, ref dates, chooserIndex);
                    startIndex = circle.IndexOf(startChar);
                }
            }
                    

            return dates;
        }
        
        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            Dating dating = new Dating();
           
            do
            {
                string[] values = input.Split(',');
                Console.WriteLine(dating.Dates(values[0], Int32.Parse(values[1])));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}