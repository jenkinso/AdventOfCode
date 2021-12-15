using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.Puzzles.Models
{
    public class LavaTubeSimple
    {
        public int LowPointCount { get { return LowPoints.Count; } }
        public int RiskLevelSum 
        {   get
            {
                int sum = 0;

                foreach (LowPoint point in LowPoints)
                {
                    sum += point.RiskLevel;
                }

                return sum;
            }
        }
        public List<LowPoint> LowPoints { get; private set; } = new List<LowPoint>();
        private int[,] heightMap;
        private int numRows;
        private int numCols;

        public LavaTubeSimple(string[] inputData)
        {
            generateHeightMapArray(inputData);

            findLowPoints();
        }

        private void generateHeightMapArray(string[] inputData)
        {
            numRows = inputData.GetLength(0);
            numCols = inputData[0].Length;

            int[,] heightMap = new int[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    heightMap[row, col] = Convert.ToInt32(inputData[row][col].ToString());
                }
            }

            this.heightMap = heightMap;
        }

        private void findLowPoints()
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (isLowPoint(row, col))
                    {
                        LowPoints.Add(new LowPoint(row, col, heightMap[row, col], heightMap));
                    }

                }
            }
        }

        private bool isLowPoint(int row, int col)
        {
            List<int> adjacentPoints = new List<int>();

            if (row - 1 >= 0)
            {
                adjacentPoints.Add(heightMap[row - 1, col]);
            }

            if (row + 1 <= numRows - 1)
            {
                adjacentPoints.Add(heightMap[row + 1, col]);
            }

            if (col - 1 >= 0)
            {
                adjacentPoints.Add(heightMap[row, col - 1]);
            }

            if (col + 1 <= numCols - 1)
            {
                adjacentPoints.Add(heightMap[row, col + 1]);
            }

            bool isLowPoint = true;

            foreach (int point in adjacentPoints)
            {
                if (heightMap[row, col] >= point)
                {
                    isLowPoint = false;
                    break;
                }
            }

            return isLowPoint;
        }
    }
}