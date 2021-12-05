using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day03BinaryDiagnostic
    {
        public static double Part1(string filepath)
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

            int[] sumOfBits = new int[maxNumBits];
            int[] gammaRateBits = new int[maxNumBits];
            int[] epsilonRateBits = new int[maxNumBits];
            double gammaDecimalNum = 0;
            double epsilonDecimalNum = 0;

            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    sumOfBits[i] += Convert.ToInt32(line[i].ToString());
                }
            }

            for (int i = 0; i < maxNumBits; i++)
            {
                gammaRateBits[i] = (sumOfBits[i] > lines.Length - sumOfBits[i]) ? 1 : 0;
                epsilonRateBits[i] = (gammaRateBits[i] == 1) ? 0 : 1;

                int exponent = maxNumBits - i - 1;
                gammaDecimalNum += gammaRateBits[i] * Math.Pow(2, exponent);
                epsilonDecimalNum += epsilonRateBits[i] * Math.Pow(2, exponent);
            }

            return gammaDecimalNum * epsilonDecimalNum;
        }

        public static double Part2(string filepath)
        {
            int[,] arrayOfBinaryNums = HelperMethods.GetArrayOfBinaryNumbersFromFile(filepath);

            int[] oxygenGeneratorRating = GetOxygenGeneratorRatingBitArray(arrayOfBinaryNums);

            int[] co2ScrubberRating = GetCO2ScrubberRatingBitArray(arrayOfBinaryNums);

            double oxygenRatingDecimal = HelperMethods.GetDecimalNumberFromBinaryBitArray(oxygenGeneratorRating);
            double co2RatingDecimal = HelperMethods.GetDecimalNumberFromBinaryBitArray(co2ScrubberRating);

            return oxygenRatingDecimal * co2RatingDecimal;
        }

        private static int[] GetOxygenGeneratorRatingBitArray(int[,] arrayOfBinaryNums)
        {
            List<int> rowIndicesList = new List<int>();

            for (int rowIndex = 0; rowIndex < arrayOfBinaryNums.GetLength(0); rowIndex++)
            {
                rowIndicesList.Add(rowIndex);
            }

            int numBitsPerNumber = arrayOfBinaryNums.GetLength(1);

            for (int bitPosition = 0; bitPosition < numBitsPerNumber; bitPosition++)
            {
                int mostCommonBit = HelperMethods.GetMostCommonBitInPosition(arrayOfBinaryNums, rowIndicesList, bitPosition);

                int[] rowIndicesArray = rowIndicesList.ToArray();

                for (int rowIndex = 0; rowIndex < rowIndicesArray.Length; rowIndex++)
                {
                    if (arrayOfBinaryNums[rowIndicesArray[rowIndex], bitPosition] != mostCommonBit)
                    {
                        rowIndicesList.Remove(rowIndicesArray[rowIndex]);
                    }
                }

                if (rowIndicesList.Count < 2)
                {
                    break;
                }
            }

            // TODO: Throw an error if rowIndicesList.Count != 1 ?

            int[] oxygenRating = new int[numBitsPerNumber];

            for (int bitPosition = 0; bitPosition < numBitsPerNumber; bitPosition++)
            {
                oxygenRating[bitPosition] = arrayOfBinaryNums[rowIndicesList[0], bitPosition];
            }

            return oxygenRating;
        }

        private static int[] GetCO2ScrubberRatingBitArray(int[,] arrayOfBinaryNums)
        {
            List<int> rowIndicesList = new List<int>();

            for (int rowIndex = 0; rowIndex < arrayOfBinaryNums.GetLength(0); rowIndex++)
            {
                rowIndicesList.Add(rowIndex);
            }

            int numBitsPerNumber = arrayOfBinaryNums.GetLength(1);

            for (int bitPosition = 0; bitPosition < numBitsPerNumber; bitPosition++)
            {
                int leastCommonBit = HelperMethods.GetLeastCommonBitInPosition(arrayOfBinaryNums, rowIndicesList, bitPosition);

                int[] rowIndicesArray = rowIndicesList.ToArray();

                for (int rowIndex = 0; rowIndex < rowIndicesArray.Length; rowIndex++)
                {
                    if (arrayOfBinaryNums[rowIndicesArray[rowIndex], bitPosition] != leastCommonBit)
                    {
                        rowIndicesList.Remove(rowIndicesArray[rowIndex]);
                    }
                }

                if (rowIndicesList.Count < 2)
                {
                    break;
                }
            }

            // TODO: Throw an error if rowIndicesList.Count != 1 ?

            int[] co2ScrubberRating = new int[numBitsPerNumber];

            for (int bitPosition = 0; bitPosition < numBitsPerNumber; bitPosition++)
            {
                co2ScrubberRating[bitPosition] = arrayOfBinaryNums[rowIndicesList[0], bitPosition];
            }

            return co2ScrubberRating;
        }
    }
}
