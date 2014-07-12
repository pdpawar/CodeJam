using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Codejam
{
    class BinaryCoder
    {
        readonly string invalidDecode = "NONE";
        public String[] Decode(string test)
        {
            List<int> inputElements = test.Select(element => element - '0').ToList();

            List<String> decodedStrings = new List<string>();

            decodedStrings.Add(GetDecodeStringBySizeOfInput(inputElements, 0));
            decodedStrings.Add(GetDecodeStringBySizeOfInput(inputElements, 1));

            return decodedStrings.ToArray();
        }

        private string GetDecodeStringByFirstElement(List<int> inputElements, int initialvalueToConsider)
        {
            List<int> outputElements = new List<int>();
            outputElements.Add(initialvalueToConsider);

            for (int i = 1; i < inputElements.Count; i++)
            {
                int sum = 0;
                outputElements.Select((element, index) => new { Value = element, Index = index }).Where(element => element.Index >= ((i - 2) > -1 ? (i - 2) : (i - 1)) && element.Index < i).ToList().ForEach(element => sum = sum + element.Value);
                outputElements.Add(inputElements[i - 1] - sum);
            }

            return ValidateLastElementOfDecodedOutPut(inputElements, outputElements) ? ValidateDecodedOutput(outputElements) : invalidDecode;
        }

        private string GetDecodeStringBySizeOfInput(List<int> inputElements, int initialvalueToConsider)
        {
            if (inputElements.Count() > 1 && inputElements.Count() >= inputElements[0])
            {
                return GetDecodeStringByFirstElement(inputElements, initialvalueToConsider);
            }
            else
            {
                if (inputElements[0] <= initialvalueToConsider)
                {
                    return (inputElements[inputElements.Count() - 1] - initialvalueToConsider) >= 0 ? inputElements[0] + "" : invalidDecode;
                }
                else return invalidDecode;
            }
        }

        private string ValidateDecodedOutput(List<int> outputElements)
        {
            String decodeOutput = "";

            return outputElements.Select((element, index) => new { Value = element, Index = index }).Where(element => element.Value < 0 || element.Value > 1).ToList().Count() > 0 ? invalidDecode : GetValidDecodeString(outputElements, decodeOutput);
        }

        private bool ValidateLastElementOfDecodedOutPut(List<int> inputElements, List<int> outputElements)
        {
            return inputElements[inputElements.Count() - 1] == (outputElements[inputElements.Count() - 1] + outputElements[inputElements.Count() - 2]);
        }

        private string GetValidDecodeString(List<int> outputElements, String decodeOutput)
        {
            Array.ForEach(outputElements.ConvertAll(element => "" + element).ToArray(), convertedElement => decodeOutput += convertedElement);
            return decodeOutput;
        }

        #region Testing code Do not change
        public static void Main(String[] args)
        {
            String input = Console.ReadLine();
            BinaryCoder coder = new BinaryCoder();
            do
            {
                String[] validationOp = coder.Decode(input);
                Console.WriteLine("{0},{1}", validationOp[0], validationOp[1]);
                input = Console.ReadLine();
            } while (input != "-1");
        }
        #endregion
    }
}