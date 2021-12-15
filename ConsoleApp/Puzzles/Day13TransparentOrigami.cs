using ConsoleApp.Puzzles.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day13TransparentOrigami
    {
        public static string Parts1And2(string filepath)
        {
            //filepath = "./data/day13_testdata.txt";
            string[] inputData = getFileData(filepath);

            OrigamiPaper origami = new OrigamiPaper(inputData);

            origami.PrintGridToConsole();

            return $"\nNumber of dots is {origami.NumberOfDots}.";
        }

        private static string[] getFileData(string filepath)
        {
            return File.ReadAllLines(filepath);
        }
    }
}
