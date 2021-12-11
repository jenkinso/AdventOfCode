using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day08SevenSegmentSearch
    {
        public static string Parts1And2(string filepath)
        {
            //string[] inputData = getTestData();
            string[] inputData = getFileData(filepath);            

            List<NotesEntry> entries = getListOfNoteEntries(inputData);

            int uniqueDigitscounter = 0;
            int sumOfOutputDigits = 0;

            foreach (var entry in entries)
            {
                uniqueDigitscounter += entry.NumUniqueOutputDigits;
                sumOfOutputDigits += entry.OutputDigits;
            }

            return $"\nOutput values contained {uniqueDigitscounter} digits using a unique number of segments." + 
                   $"\nSum of all output values was {sumOfOutputDigits}.";
        }

        private static string[] getFileData(string filepath)
        {
            return File.ReadAllLines(filepath);
        }

        private static string[] getTestData()
        {
            return new string[] { "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf" };
        }
        
        private static List<NotesEntry> getListOfNoteEntries(string[] inputData)
        {
            List<NotesEntry> entries = new List<NotesEntry>();

            foreach (string entry in inputData)
            {
                entries.Add(new NotesEntry(entry));
            }

            return entries;
        }
    }
}
