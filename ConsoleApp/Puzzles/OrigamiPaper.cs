using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public class OrigamiPaper
    {
        public char[,] Grid { get; private set; }
        public int NumberOfDots { 
            get
            {
                int counter = 0;

                foreach (char item in Grid)
                {
                    if (item == dotCharacter) counter++;
                }

                return counter;
            } 
        }
        private List<(int x, int y)> dots = new List<(int x, int y)>();
        private List<(char axis, int lineIndex)> foldInstructions = new List<(char axis, int lineIndex)>();
        private const char dotCharacter = '#';

        public OrigamiPaper(string[] inputData, int numFoldInstructions = -1)
        {
            parseInputData(inputData);

            applyFolds(numFoldInstructions);

            setupGrid();

            populateGrid();
        }

        public void PrintGridToConsole()
        {
            Console.WriteLine();

            for (int row = 0; row < Grid.GetLength(0); row++)
            {
                for (int col = 0; col < Grid.GetLength(1); col++)
                {
                    Console.Write(Grid[row, col]);
                }

                Console.Write("\n");
            }

            Console.WriteLine();
        }

        private void parseInputData(string[] inputData)
        {
            int lineIndex = 0;

            for (int i = 0; i < inputData.Length; i++)
            {
                lineIndex = i;

                if (inputData[i] == "") break;                

                string[] coords = inputData[i].Split(',');

                this.dots.Add((Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1])));
            }

            for (int i = lineIndex + 1; i < inputData.Length; i++)
            {
                if (inputData[i] == "") break;

                string line = inputData[i].Remove(0, 11);

                string[] instructions = line.Split('=');

                this.foldInstructions.Add((Convert.ToChar(instructions[0]), Convert.ToInt32(instructions[1])));   
            }
        }

        private void applyFolds(int numFoldInstructions)
        {
            if (numFoldInstructions < 0)
            {
                numFoldInstructions = foldInstructions.Count;
            }

            for (int i = 0; i < numFoldInstructions; i++)
            {
                if (foldInstructions[i].axis == 'y')
                {
                    horizontalFold(foldInstructions[i].lineIndex);
                }
                else
                {
                    verticalFold(foldInstructions[i].lineIndex);
                }
            }
        }

        private void horizontalFold(int y)
        {
            for (int i = 0; i < dots.Count; i++)
            {
                if (dots[i].y > y)
                {
                    int distanceBelowFold = dots[i].y - y;
                    int newYCoord = dots[i].y - 2 * distanceBelowFold;

                    dots[i] = (dots[i].x, newYCoord);
                }
            }
        }

        private void verticalFold(int x)
        {
            for (int i = 0; i < dots.Count; i++)
            {
                if (dots[i].x > x)
                {
                    int distanceBeyondFold = dots[i].x - x;
                    int newXCoord = dots[i].x - 2 * distanceBeyondFold;

                    dots[i] = (newXCoord, dots[i].y);
                }
            }
        }

        private void setupGrid()
        {
            int xMax = 0;
            int yMax = 0;

            foreach (var dot in dots)
            {
                if (dot.x > xMax) xMax = dot.x;
                if (dot.y > yMax) yMax = dot.y;
            }

            this.Grid = new char[yMax + 1, xMax + 1];
        }

        private void populateGrid()
        {
            for (int y = 0; y < Grid.GetLength(0); y++)
            {
                for (int x = 0; x < Grid.GetLength(1); x++)
                {
                    this.Grid[y, x] = ' ';
                }
            }

            foreach (var dot in dots)
            {
                this.Grid[dot.y, dot.x] = dotCharacter;
            }
        }

    }
}
