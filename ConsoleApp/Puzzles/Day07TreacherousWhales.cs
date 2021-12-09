using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day07TreacherousWhales
    {
        public static int Part1(string filepath)
        {
            //int[] crabPositions = getTestData();
            int[] crabPositions = getFileData(filepath);

            int maxPosition = getMaxValue(crabPositions);

            int[] totalFuels = new int[maxPosition + 1];

            for (int crab = 0; crab < crabPositions.Length; crab++)
            {
                for (int position = 0; position < totalFuels.Length; position++)
                {
                    totalFuels[position] += Math.Abs(crabPositions[crab] - position);
                }
            }

            int optimumPosition = getIndexWithMinValue(totalFuels);
            int optimumTotalFuel = totalFuels[optimumPosition];

            return optimumTotalFuel;
        }

        public static int Part2(string filepath)
        {
            //int[] crabPositions = getTestData();
            int[] crabPositions = getFileData(filepath);

            int maxPosition = getMaxValue(crabPositions);

            int[] totalFuels = new int[maxPosition + 1];
            
            int loopCounter = 0;
            
            for (int crab = 0; crab < crabPositions.Length; crab++)
            {
                for (int position = 0; position < totalFuels.Length; position++)
                {
                    int absDistance = Math.Abs(crabPositions[crab] - position);
                    totalFuels[position] += calcFuelCost_NonRecursive(absDistance);
                    loopCounter++;
                }
            }

            int optimumPosition = getIndexWithMinValue(totalFuels);
            int optimumTotalFuel = totalFuels[optimumPosition];

            return optimumTotalFuel;
        }

        private static int calcFuelCost_Recursive(int distance)
        {
            if (distance <= 1) return distance;

            return calcFuelCost_Recursive(distance - 1) + distance;
        }

        private static int calcFuelCost_NonRecursive(int distance)
        {
            int deltaCost = 0;
            int cost = 0;

            for (int i = 1; i <= distance; i++)
            {
                deltaCost += 1;
                cost += deltaCost;                
            }

            return cost;
        }

        private static int[] getTestData()
        {
            return new int[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
        }

        private static int[] getFileData(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            lines = lines[0].Split(',');

            int[] inputData = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                inputData[i] = Convert.ToInt32(lines[i]);
            }

            return inputData;
        }

        private static int getMaxValue(int[] intArray)
        {
            int maxValue = Int32.MinValue;

            for (int i = 0; i < intArray.Length; i++)
            {
                if (intArray[i] >= maxValue)
                {
                    maxValue = intArray[i];
                }
            }

            return maxValue;
        }

        private static int getIndexWithMinValue(int[] intArray)
        {
            int minValue = Int32.MaxValue;
            int index = -1;

            for (int i = 0; i < intArray.Length; i++)
            {
                if (intArray[i] <= minValue)
                {
                    minValue = intArray[i];
                    index = i;
                }
            }

            return index;
        }
    }
}
