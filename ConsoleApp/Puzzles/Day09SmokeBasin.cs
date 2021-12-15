using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConsoleApp.Puzzles.Models;

namespace ConsoleApp.Puzzles
{
    public static class Day09SmokeBasin
    {
        public static string Parts1And2(string filepath)
        {
            //string[] inputData = getFileData("./data/day09_testdata.txt");
            string[] inputData = getFileData(filepath);

            LavaTubeSimple lavaTube = new LavaTubeSimple(inputData);

            List<LowPoint> lowPointsOrdered = lavaTube.LowPoints.OrderByDescending(lp => lp.BasinSize).ToList();

            int basinSizeProduct = 0;

            if (lowPointsOrdered.Count > 2)
            {
                basinSizeProduct = lowPointsOrdered[0].BasinSize * lowPointsOrdered[1].BasinSize * lowPointsOrdered[2].BasinSize;
            }

            return $"\nSum of risk levels for all low points = {lavaTube.RiskLevelSum}." +
                   $"\nProduct of the sizes of the three largest basins = {basinSizeProduct}.";
        }

        private static string[] getFileData(string filepath)
        {
            return File.ReadAllLines(filepath);
        }
    }
}
