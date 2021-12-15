using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles.Models
{
    public delegate bool IsLowPointHandler(int row, int col);

    public class LavaTube
    {
        public int LowPointCount { get; private set; }
        public int RiskLevelSum { get; private set; }
        private int[,] heightMap;
        private int numRows;
        private int numCols;
        

        public LavaTube(string[] inputData)
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
                    IsLowPointHandler handler = getIsLowPointHandler(row, col);
                    bool isLowPoint = handler(row, col);

                    if (isLowPoint)
                    {
                        LowPointCount++;
                        RiskLevelSum += heightMap[row, col] + 1;
                    }
                }
            }
        }

        private IsLowPointHandler getIsLowPointHandler(int row, int col)
        {
            if (row == 0 && col == 0)
            {
                return isLowPoint_TopLeft;
            }
            else if (row == 0 && col == numCols - 1)
            {
                return isLowPoint_TopRight;
            }
            else if (row == numRows - 1 && col == 0)
            {
                return isLowPoint_BottomLeft;
            }
            else if (row == numRows - 1 && col == numCols - 1)
            {
                return isLowPoint_BottomRight;
            }
            else if (row == 0)
            {
                return isLowPoint_TopRow;
            }
            else if (col == 0)
            {
                return isLowPoint_FirstColumn;
            }
            else if (col == numCols - 1)
            {
                return isLowPoint_LastColumn;
            }
            else if (row == numRows - 1)
            {
                return isLowPoint_BottomRow;
            }
            else
            {
                return isLowPoint_MiddleLocation;
            }
        }

        private bool isLowPoint_TopLeft(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row, col + 1] && height < heightMap[row + 1, col];

            return isLowPoint;
        }

        private bool isLowPoint_TopRight(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row, col - 1] && height < heightMap[row + 1, col];

            return isLowPoint;
        }

        private bool isLowPoint_BottomLeft(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row - 1, col] && height < heightMap[row, col + 1];

            return isLowPoint;
        }

        private bool isLowPoint_BottomRight(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row, col - 1] && height < heightMap[row - 1, col];

            return isLowPoint;
        }

        private bool isLowPoint_TopRow(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row, col - 1] && height < heightMap[row, col + 1] && height < heightMap[row + 1, col];

            return isLowPoint;
        }

        private bool isLowPoint_FirstColumn(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row - 1, col] && height < heightMap[row, col + 1] && height < heightMap[row + 1, col];

            return isLowPoint;
        }

        private bool isLowPoint_LastColumn(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row - 1, col] && height < heightMap[row, col - 1] && height < heightMap[row + 1, col];

            return isLowPoint;
        }

        private bool isLowPoint_BottomRow(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row - 1, col] && height < heightMap[row, col - 1] && height < heightMap[row, col + 1];

            return isLowPoint;
        }

        private bool isLowPoint_MiddleLocation(int row, int col)
        {
            int height = heightMap[row, col];

            bool isLowPoint = height < heightMap[row, col - 1] && height < heightMap[row - 1, col] && height < heightMap[row, col + 1] && height < heightMap[row + 1, col];

            return isLowPoint;
        }        
    }
}
