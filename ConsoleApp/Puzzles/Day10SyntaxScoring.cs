using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ConsoleApp.Puzzles.Models;

namespace ConsoleApp.Puzzles
{
    public static class Day10SyntaxScoring
    {
        public static string Parts1And2(string filepath)
        {
            //filepath = "./data/day10_testdata.txt";
            string[] inputData = getFileData(filepath);

            SyntaxScorer scorer = new SyntaxScorer(inputData);

            long winningCompletionScore = scorer.getWinningCompletionScore();

            return $"\nThe total syntax error score was {scorer.SyntaxErrorScore}." + 
                   $"\nThe middle line completion score was {scorer.getWinningCompletionScore()}.";
        }

        private static string[] getFileData(string filepath)
        {
            return File.ReadAllLines(filepath);
        }
    }
}
