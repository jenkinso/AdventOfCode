using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day01SonarSweep
    {
        public static int Part1(string filepath)
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
                if (sonarMeasurements[i] > sonarMeasurements[i - 1])
                {
                    increaseCount++;
                }
            }

            return increaseCount;
        }

        public static int Part2(string filepath, int slidingWindowLength = 3)
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

    }

    
}
