using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day05HydrothermalVenture
    {
        public static int Part1(string filepath)
        {
            var pairsList = getCoordinatePairs(filepath);

            var horizontalAndVerticalVents = filterForHorizontalAndVerticalVents(pairsList);

            // get a 2d array of points where each array element contains the count of vents present at that point.
            int[,] pointsArray = mapHorizontalAndVerticalVents(horizontalAndVerticalVents);

            int numDangerousPoints = countDangerousPoints(pointsArray);

            return numDangerousPoints;
        }

        public static int Part2(string filepath)
        {
            var pairsList = getCoordinatePairs(filepath);

            int[,] pointsArray = mapAllVents(pairsList);

            int numDangerousPoints = countDangerousPoints(pointsArray);

            return numDangerousPoints;
        }

        private static List<(int x1, int y1, int x2, int y2)> getCoordinatePairs(string filepath)
        {
            List<(int x1, int y1, int x2, int y2)> coordinatePairs = new List<(int x1, int y1, int x2, int y2)>();

            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                string[] pairs = line.Trim().Split(" -> ");

                string[] pair1 = pairs[0].Split(',');
                string[] pair2 = pairs[1].Split(',');

                coordinatePairs.Add((
                    Convert.ToInt32(pair1[0]), 
                    Convert.ToInt32(pair1[1]), 
                    Convert.ToInt32(pair2[0]), 
                    Convert.ToInt32(pair2[1])
                    ));
            }

            return coordinatePairs;
        }

        private static List<(int x1, int y1, int x2, int y2)> filterForHorizontalAndVerticalVents(List<(int x1, int y1, int x2, int y2)> coordPairs)
        {
            List<(int x1, int y1, int x2, int y2)> filteredPairs = new List<(int x1, int y1, int x2, int y2)>();

            foreach (var pairs in coordPairs)
            {
                if (pairs.x1 == pairs.x2 || pairs.y1 == pairs.y2)
                {
                    filteredPairs.Add(pairs);
                }
            }

            return filteredPairs;
        }

        private static int[,] mapHorizontalAndVerticalVents(List<(int x1, int y1, int x2, int y2)> coordPairs, int gridLength = 1000)
        {
            int[,] pointsArray = new int[gridLength, gridLength];

            foreach (var pairs in coordPairs)
            {
                if (pairs.x1 == pairs.x2)
                {
                    // keep x constant and loop over the values of y that this vent covers. increment our vent counter at each (x,y) position.
                    for (int y = Math.Min(pairs.y1, pairs.y2); y <= Math.Max(pairs.y1, pairs.y2); y++)
                    {
                        pointsArray[pairs.x1, y] += 1;
                    }
                }
                else
                {
                    // keep y constant and loop over the values of x that this vent covers. increment our vent counter at each (x,y) position.
                    for (int x = Math.Min(pairs.x1, pairs.x2); x <= Math.Max(pairs.x1, pairs.x2); x++)
                    {
                        pointsArray[x, pairs.y1] += 1;
                    }
                }
            }

            return pointsArray;
        }

        private static int countDangerousPoints(int[,] pointsArray, int dangerThreshold = 2)
        {
            int dangerousPointCounter = 0;

            for (int row = 0; row < pointsArray.GetLength(0); row++)
            {
                for (int col = 0; col < pointsArray.GetLength(1); col++)
                {
                    if (pointsArray[row, col] >= dangerThreshold)
                    {
                        dangerousPointCounter++;
                    }
                }
            }

            return dangerousPointCounter;
        }

        private static int[,] mapAllVents(List<(int x1, int y1, int x2, int y2)> coordPairs, int gridLength = 1000)
        {
            int[,] pointsArray = new int[gridLength, gridLength];

            foreach (var pairs in coordPairs)
            {
                int numPointsOnLine = Math.Max(Math.Abs(pairs.x2 - pairs.x1), Math.Abs(pairs.y2 - pairs.y1)) + 1;

                int deltaX = getDeltaPerPoint(pairs.x2 - pairs.x1);
                int deltaY = getDeltaPerPoint(pairs.y2 - pairs.y1);

                int x = pairs.x1;
                int y = pairs.y1;

                for (int i = 0; i < numPointsOnLine; i++)
                {
                    pointsArray[x, y]++;

                    x += deltaX;
                    y += deltaY;
                }
            }

            return pointsArray;
        }

        private static int getDeltaPerPoint(int delta)
        {
            if (delta > 0)
            {
                return 1;
            }
            else if (delta == 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

    }

    
}
