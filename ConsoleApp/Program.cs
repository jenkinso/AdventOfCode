using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine($"Solution to Day 1 Part 1 is {Day1SonarSweepPart1(@"./data/day01_input.txt")}");
            //Console.WriteLine($"Solution to Day 1 Part 2 is {Day1SonarSweepPart2(@"./data/day01_input.txt")}");

            //Console.WriteLine($"Solution to Day 2 Part 1 is {Day2DivePart1(@"./data/day02_input.txt")}");
            //Console.WriteLine($"Solution to Day 2 Part 2 is {Day2DivePart2(@"./data/day02_input.txt")}");

            //Console.WriteLine($"Solution to Day 3 Part 1 is {Day3BinaryDiagnosticPart1(@"./data/day03_input.txt")}");
            Console.WriteLine($"Solution to Day 3 Part 2 is {Day3BinaryDiagnosticPart2(@"./data/day03_input.txt")}");

            Console.ReadLine();
        }

        private static int Day1SonarSweepPart1(string filepath)
        {
            const int minNumberMeasurements = 2;

            int[] sonarMeasurements = HelperMethods.GetArrayOfNumbersFromFile(filepath);

            if (sonarMeasurements.Length < minNumberMeasurements)
            {
                return 0;
            }

            int increaseCount = 0;
            for (int i = 1; i < sonarMeasurements.Length; i++)
            {
                if (sonarMeasurements[i] > sonarMeasurements[i-1])
                {
                    increaseCount++;
                }
            }
            
            return increaseCount;
        }

        private static int Day1SonarSweepPart2(string filepath, int slidingWindowLength = 3)
        {
            int minNumberMeasurements = slidingWindowLength + 1;

            int[] sonarMeasurements = HelperMethods.GetArrayOfNumbersFromFile(filepath);

            if (sonarMeasurements.Length < minNumberMeasurements)
            {
                return 0;
            }

            int increaseCount = 0;
            int window1Sum = 0; // window 1 = upper window
            int window2Sum = 0; // window 2 = lower window
            int startIndex = minNumberMeasurements - 1;

            // find sums in first window positions
            for (int i = 0; i < slidingWindowLength; i++)
            {
                window1Sum += sonarMeasurements[i + 1];
                window2Sum += sonarMeasurements[i];
            }

            for (int i = startIndex; i < sonarMeasurements.Length; i++)
            {
                int window1UpperIndex = i;
                int window2UpperIndex = i - 1;

                if (i > startIndex)
                {
                    // calculate the new sum. add the new value at the top of the window; remove the old value at the bottom.
                    window1Sum = window1Sum - sonarMeasurements[window1UpperIndex - slidingWindowLength] + sonarMeasurements[window1UpperIndex];
                    window2Sum = window2Sum - sonarMeasurements[window2UpperIndex - slidingWindowLength] + sonarMeasurements[window2UpperIndex];
                }

                if (window1Sum > window2Sum)
                {
                    increaseCount++;
                }
            }

            return increaseCount;
        }

        private static int Day2DivePart1(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            int horizontal = 0;
            int depth = 0;

            // assuming no errors/inconsistencies in the input data!
            foreach (string line in lines)
            {
                string[] split = line.Split(' ');

                string commandString = split[0];
                int commandMagnitude = Convert.ToInt32(split[1]);

                if (commandString == "forward")
                {
                    horizontal += commandMagnitude;
                }
                else if (commandString == "down")
                {
                    depth += commandMagnitude;
                }
                else if (commandString == "up")
                {
                    depth -= commandMagnitude;
                }
            }

            return horizontal * depth;
        }

        private static int Day2DivePart2(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            // assuming no errors/inconsistencies in the input data!
            foreach (string line in lines)
            {
                string[] split = line.Split(' ');

                string commandString = split[0];
                int commandMagnitude = Convert.ToInt32(split[1]);

                if (commandString == "forward")
                {
                    horizontal += commandMagnitude;
                    depth += (aim * commandMagnitude);
                }
                else if (commandString == "down")
                {
                    aim += commandMagnitude;
                }
                else if (commandString == "up")
                {
                    aim -= commandMagnitude;
                }
            }

            return horizontal * depth;
        }

        private static double Day3BinaryDiagnosticPart1(string filepath)
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

            int[] gammaRateBits = new int[maxNumBits];
            int[] epsilonRateBits = new int[maxNumBits];
            double gammaDecimalNum = 0;
            double epsilonDecimalNum = 0;

            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    gammaRateBits[i] += Convert.ToInt32(line[i].ToString());
                }
            }

            for (int i = 0; i < maxNumBits; i++)
            {
                gammaRateBits[i] = (gammaRateBits[i] > lines.Length - gammaRateBits[i]) ? 1 : 0;
                epsilonRateBits[i] = (gammaRateBits[i] == 1) ? 0 : 1;

                int exponent = maxNumBits - i - 1;
                gammaDecimalNum += gammaRateBits[i] * Math.Pow(2, exponent);
                epsilonDecimalNum += epsilonRateBits[i] * Math.Pow(2, exponent);
            }

            return gammaDecimalNum * epsilonDecimalNum;
        }

        private static double Day3BinaryDiagnosticPart2(string filepath)
        {
            int[,] arrayOfBinaryNums = HelperMethods.GetArrayOfBitsFromFile(filepath);

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
