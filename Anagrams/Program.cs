using System;
using System.Collections.Generic;
using System.Text;

namespace Codejam
{
    class Anagrams
    {
        /*
         *To maintain the anagrams 
         */
        Dictionary<String, List<String>> anagramSet = new Dictionary<string, List<string>>();

        /*
         * to check whether two strings are of equal lenght
         */
        Boolean ValidateLength(int subjectLength, int targetLength)
        {
            if (subjectLength == targetLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
        * to get number of count of a character from target and subject string
        */
        int GetCharCount(string target, char characterToCheck)
        {
            int charCounter=0;

            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == characterToCheck)
                {
                    charCounter++;
                }
            }
            return charCounter;
        }

        /**
         * to check the content of subject and target string 
         * */
        Boolean ValidateContent(string subject, string target)
        {
            if (ValidateLength(subject.Length, target.Length))
            {

                for (int i = 0; i < subject.Length; i++)
                {
                    if (!(GetCharCount(subject, subject[i]) == GetCharCount(target, subject[i])))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * To check whether an string is already flagged as anagram
         * */
        private bool ValidateFlagedAnagrams(string subject)
        {
            foreach (String word in anagramSet.Keys)
            {
                if (anagramSet[word].Contains(subject))
                {
                    return true;
                }               
            }

            return false;
        }

        int GetMaximumSubset(string[] words)
        {
            for(int j=0; j < words.Length ; j++)
            {
                string subject = words[j];

                if (ValidateFlagedAnagrams(subject))
                {
                    continue;
                }

                anagramSet.Add(subject, new List<string>());

                for (int i = j + 1; i < words.Length ; i++)
                {
                    string target = words[i];

                    if (ValidateContent(subject, target))
                    {
                        List<String> tempAnagramList = anagramSet[subject];
                        tempAnagramList.Add(target);

                        anagramSet.Remove(subject);
                        anagramSet.Add(subject, tempAnagramList);
                    }
                }
            }

            return anagramSet.Count;
        }

      

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            Anagrams anagramSolver = new Anagrams();
            do
            {
                string[] words = input.Split(',');
                Console.WriteLine(anagramSolver.GetMaximumSubset(words));
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion


    }
}

