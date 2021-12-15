using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConsoleApp.Puzzles.Models;

namespace ConsoleApp.Puzzles
{
    public static class Day11DumboOctopus
    {
        public static string Parts1And2(string filepath)
        {
            //filepath = "./data/day11_testdata.txt";
            //filepath = "./data/day11_testdatalarger.txt";
            string[] inputData = getFileData(filepath);

            OctopusCavern octopusCavern = new OctopusCavern(inputData, 1000);

            int firstStepBigFlash = 0;

            if (octopusCavern.BigFlashSteps.Count > 0)
            {
                firstStepBigFlash = octopusCavern.BigFlashSteps[0];
            }

            return $"\nThere were {octopusCavern.FlashCount} flashes in total." + 
                   $"\nThe first step with a Big Flash was {firstStepBigFlash}.";
        }

        private static string[] getFileData(string filepath)
        {
            return File.ReadAllLines(filepath);
        }
    }
}
