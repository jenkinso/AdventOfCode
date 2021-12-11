using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public class NotesEntry
    {
        public int OutputDigits { get; private set; }
        public int NumUniqueOutputDigits
        {
            get
            {
                int counter = 0;

                foreach (string item in outputValues)
                {
                    if (item.Length == 2 || item.Length == 3 || item.Length == 4 || item.Length == 7) counter++;
                }

                return counter;
            }
        }
        private string[] signalPatterns;
        private string[] outputValues;        
        private Dictionary<char, char> segmentToWireMappings = new Dictionary<char, char>();
        private string[] outputValuesDecoded;
        public static Dictionary<string, int> standardSegmentsDigitMapping = new Dictionary<string, int>()
        {
            { "abcefg", 0},
            { "cf", 1 },
            { "acdeg", 2 },
            { "acdfg", 3 },
            { "bcdf", 4 },
            { "abdfg", 5 },
            { "abdefg", 6 },
            { "acf", 7 },
            { "abcdefg", 8 },
            { "abcdfg", 9 }
        };
        
        public NotesEntry(string entryString)
        {
            processEntryString(entryString);

            findInitialMappings();

            findRemainingMappings();

            decodeOutputValues();

            generateFinalOutputDigits();
        }

        private void processEntryString(string entryString)
        {
            string[] entries = entryString.Split(" | ");

            signalPatterns = entries[0].Split(' ');
            outputValues = entries[1].Split(' ');

            outputValuesDecoded = new string[outputValues.Length];
        }

        private void findInitialMappings()
        {
            char[] segments = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };

            const int bFrequency = 6;
            const int eFrequency = 4;
            const int fFrequency = 9;

            foreach (char segment in segments)
            {
                int frequencyCount = findSegmentFrequencyInSignalPatterns(segment);

                switch (frequencyCount)
                {
                    case bFrequency:
                        segmentToWireMappings.Add(segment, 'b');
                        break;
                    case eFrequency:
                        segmentToWireMappings.Add(segment, 'e');
                        break;
                    case fFrequency:
                        segmentToWireMappings.Add(segment, 'f');
                        break;
                }
            }
        }

        private void findRemainingMappings()
        {
            // must be called in this order!
            findMissingMappedCharacter(2, 'c');
            findMissingMappedCharacter(3, 'a');
            findMissingMappedCharacter(4, 'd');
            findMissingMappedCharacter(7, 'g');
        }

        private void decodeOutputValues()
        {
            for (int i = 0; i < outputValues.Length; i++)
            {
                string decodedString = "";

                foreach (char character in outputValues[i])
                {
                    decodedString += segmentToWireMappings[character];
                }

                outputValuesDecoded[i] = sortStringAlphabetically(decodedString);
            }
        }

        private void generateFinalOutputDigits()
        {
            string digits = "";

            foreach (string segments in outputValuesDecoded)
            {
                digits += standardSegmentsDigitMapping[segments];
            }

            OutputDigits = Convert.ToInt32(digits);
        }

        private string sortStringAlphabetically(string input)
        {
            char[] a = input.ToCharArray();
            int stringLength = a.Length;

            for (int i = 0; i < stringLength - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < stringLength; j++)
                {
                    if (a[j] < a[minIndex])
                    {
                        minIndex = j;
                    }
                }

                char temp = a[minIndex];
                a[minIndex] = a[i];
                a[i] = temp;
            }

            string sortedString = "";
            foreach (char character in a)
            {
                sortedString += character;
            }

            return sortedString;
        }

        private void findMissingMappedCharacter(int patternLength, char mappedSignalCharacter)
        {
            string signalPattern = findSignalPatternWithGivenLength(patternLength);

            foreach (char c in signalPattern)
            {
                if (segmentToWireMappings.ContainsKey(c) == false)
                {
                    segmentToWireMappings.Add(c, mappedSignalCharacter);
                }
            }
        }

        private string findSignalPatternWithGivenLength(int uniqueLength)
        {
            string patternFound = "";

            foreach (string pattern in signalPatterns)
            {
                if (pattern.Length == uniqueLength)
                {
                    patternFound = pattern;
                }
            }

            return patternFound;
        }

        private int findSegmentFrequencyInSignalPatterns(char segment)
        {
            int frequencyCount = 0;

            foreach (string pattern in signalPatterns)
            {
                if (pattern.Contains(segment))
                {
                    frequencyCount++;
                }
            }

            return frequencyCount;
        }
    }

}
