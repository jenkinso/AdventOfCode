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

        public static int[,] GetArrayOfBitsFromFile(string filepath)
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

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    bitArray[i, j] = Convert.ToInt32(lines[i][j].ToString());
                }
            }

            return bitArray;
        }

        public static int GetMostCommonBitInPosition(int[,] bitArray, int position)
        {
            int numRows = bitArray.GetLength(0);

            int sumOfBits = 0;

            for (int row = 0; row < numRows; row++)
            {
                sumOfBits += bitArray[row, position];
            }

            int mostCommonBit = (sumOfBits >= numRows - sumOfBits) ? 1 : 0;

            return mostCommonBit;
        }

        public static int GetLeastCommonBitInPosition(int[,] bitArray, int position)
        {
            int mostCommonBit = GetMostCommonBitInPosition(bitArray, position);

            return (mostCommonBit == 1) ? 0 : 1;
        }
    }
}
