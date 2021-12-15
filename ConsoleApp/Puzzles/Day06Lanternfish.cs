using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using ConsoleApp.Puzzles.Models;

namespace ConsoleApp.Puzzles
{
    public static class Day06Lanternfish
    {
        public static int Part1(string filepath)
        {
            const int numDays = 80;

            //sbyte[] inputData = getTestData();
            sbyte[] inputData = getFileData(filepath);
            
            List<Lanternfish> fishes = createInitialLanternFishPopulation(inputData);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int day = 0; day < numDays; day++)
            {
                int spawnCounter = 0;

                foreach (Lanternfish fish in fishes)
                {
                    bool spawn = fish.IncrementDay();
                    if (spawn) spawnCounter++;
                }

                for (int i = 0; i < spawnCounter; i++)
                {
                    fishes.Add(new Lanternfish());
                }

                Console.WriteLine($"After {day + 1} days. {fishes.Count} fish. Spawn count of {spawnCounter}.");
            }

            stopWatch.Stop();
            Console.WriteLine($"Elapsed time is {stopWatch.Elapsed.TotalSeconds} seconds.");

            return fishes.Count;
        }

        public static int Part2_Attempt1(string filepath)
        {
            const int numDays = 80;
            const sbyte newFishTimerStart = 8;
            const sbyte oldFishTimerStart = 6;
            
            //sbyte[] inputData = getTestData();
            sbyte[] inputData = getFileData(filepath);

            List<sbyte> fishes = createInitialLanternFishTimerList(inputData);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int day = 0; day < numDays; day++)
            {
                int spawnCounter = 0;

                for (int fish = 0; fish < fishes.Count; fish++)
                {
                    fishes[fish]--;

                    if (fishes[fish] < 0)
                    {
                        fishes[fish] = oldFishTimerStart;
                        spawnCounter++;
                    }
                }

                for (int spawn = 0; spawn < spawnCounter; spawn++)
                {
                    fishes.Add(newFishTimerStart);
                }

                Console.WriteLine($"After {day + 1} days. {fishes.Count} fish. Spawn count of {spawnCounter}.");
            }

            stopWatch.Stop();
            Console.WriteLine($"Elapsed time is {stopWatch.Elapsed.TotalSeconds} seconds.");

            return fishes.Count;
        }

        public static long Part2_Attempt2(string filepath)
        {
            const int numDays = 256;
            const sbyte newFishTimerStart = 8;
            const sbyte oldFishTimerStart = 6;

            //sbyte[] inputData = getTestData();
            sbyte[] inputData = getFileData(filepath);

            // Array to hold the count of fish with each timer value (where the array index is equal to the timer value)
            long[] fishCounts = new long[newFishTimerStart + 1];

            // Process the input data
            for (int f = 0; f < inputData.Length; f++)
            {
                fishCounts[inputData[f]]++;
            }

            for (int day = 0; day < numDays; day++)
            {
                // store how many fish need their timers reset
                long fishResetCount = fishCounts[0];
                fishCounts[0] = 0;

                // move all the other fish down the fishCounts array (effectively decrementing their timer value)
                for (int timerValue = 1; timerValue <= newFishTimerStart; timerValue++)
                {
                    fishCounts[timerValue - 1] += fishCounts[timerValue];
                    fishCounts[timerValue] = 0;
                }

                // add all the newly spawned fish and the old "reset" fish
                fishCounts[newFishTimerStart] += fishResetCount;
                fishCounts[oldFishTimerStart] += fishResetCount;
            }

            long fishTotal = 0;
            for (int timerValue = 0; timerValue <= newFishTimerStart; timerValue++)
            {
                fishTotal += fishCounts[timerValue];
            }

            return fishTotal;
        }

        private static sbyte[] getTestData()
        {
            return new sbyte[] { 3, 4, 3, 1, 2 };
        }

        private static List<Lanternfish> createInitialLanternFishPopulation(sbyte[] inputData)
        {
            List<Lanternfish> fish = new List<Lanternfish>();

            foreach (sbyte timerValue in inputData)
            {
                fish.Add(new Lanternfish(timerValue));
            }

            return fish;
        }

        private static sbyte[] getFileData(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            
            lines = lines[0].Split(',');

            sbyte[] inputData = new sbyte[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                inputData[i] = Convert.ToSByte(lines[i]);
            }

            return inputData;
        }

        private static List<sbyte> createInitialLanternFishTimerList(sbyte[] inputData)
        {
            List<sbyte> timerList = new List<sbyte>();

            foreach (sbyte timerValue in inputData)
            {
                timerList.Add(timerValue);
            }

            return timerList;
        }
    }
}
