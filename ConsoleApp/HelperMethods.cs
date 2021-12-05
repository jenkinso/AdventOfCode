using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    public static class HelperMethods
    {
        public static int[] GetArrayOfNumbersFromFile(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            List<int> numbersList = new List<int>();

            int number;
            for (int i = 0; i < lines.Length; i++)
            {
                bool succeeded = int.TryParse(lines[i], out number);

                if (succeeded)
                {
                    numbersList.Add(number);
                }
            }

            return numbersList.ToArray();
        }

        public static int[,] GetArrayOfBinaryNumbersFromFile(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            int maxNumBits = 0;

            foreach (string line in lines)
            {
                if (line.Length > maxNumBits)
                {
                    maxNumBits = line.Length;
                }
            }

            int[,] bitArray = new int[lines.Length, maxNumBits];

            for (int row = 0; row < lines.Length; row++)
            {
                for (int bitPosition = 0; bitPosition < lines[row].Length; bitPosition++)
                {
                    bitArray[row, bitPosition] = Convert.ToInt32(lines[row][bitPosition].ToString());
                }
            }

            return bitArray;
        }

        public static int GetMostCommonBitInPosition(int[,] arrayOfBinaryNums, List<int> rowIndicesList, int position)
        {
            int sumOfBits = 0;

            for (int row = 0; row < rowIndicesList.Count; row++)
            {
                sumOfBits += arrayOfBinaryNums[rowIndicesList[row], position];
            }

            int mostCommonBit = (sumOfBits >= rowIndicesList.Count - sumOfBits) ? 1 : 0;

            return mostCommonBit;
        }

        public static int GetLeastCommonBitInPosition(int[,] arrayOfBinaryNums, List<int> rowIndicesList, int position)
        {            
            int mostCommonBit = GetMostCommonBitInPosition(arrayOfBinaryNums, rowIndicesList, position);

            int leastCommonBit = (mostCommonBit == 1) ? 0 : 1;

            return leastCommonBit;
        }

        public static double GetDecimalNumberFromBinaryBitArray(int[] binaryBitArray)
        {
            int numBits = binaryBitArray.Length;
            double decimalNumber = 0;

            for (int bitPosition = 0; bitPosition < numBits; bitPosition++)
            {
                int exponent = numBits - bitPosition - 1;
                decimalNumber += binaryBitArray[bitPosition] * Math.Pow(2, exponent);
            }

            return decimalNumber;
        }
    }
}
