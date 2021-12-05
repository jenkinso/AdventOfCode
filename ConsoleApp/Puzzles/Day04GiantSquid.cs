using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day04GiantSquid
    {
        public static int Part1(string filepath)
        {
            int[] drawnNumbers = getDrawnNumbersFromFile(filepath);

            List<int[,]> bingoBoardData = getBingoBoardData(filepath);

            List<BingoBoard> bingoBoards = new List<BingoBoard>();

            foreach (var board in bingoBoardData)
            {
                bingoBoards.Add(new BingoBoard(board));
            }

            List<int> winningBoardIndices = new List<int>();

            foreach (int number in drawnNumbers)
            {
                for (int boardIndex = 0; boardIndex < bingoBoards.Count; boardIndex++)
                {
                    bool wasStillPlaying = bingoBoards[boardIndex].StillPlaying;
                    
                    bingoBoards[boardIndex].CallNumber(number);
                    if (bingoBoards[boardIndex].BingoStatus && wasStillPlaying)
                    {
                        winningBoardIndices.Add(boardIndex);
                    }
                }

                if (winningBoardIndices.Count > 0)
                {
                    //break;
                }
            }

            int finalPuzzleAnswer = 0;

            if (winningBoardIndices.Count == 1)
            {
                finalPuzzleAnswer = bingoBoards[winningBoardIndices[0]].Score;
            }

            return finalPuzzleAnswer;
        }

        private static int[] getDrawnNumbersFromFile(string filepath, int lineIndexWithDrawnNumbers = 0)
        {
            string[] lines = File.ReadAllLines(filepath);

            string[] numbersAsStrings = lines[lineIndexWithDrawnNumbers].Split(',');

            int numbersArrayLength = numbersAsStrings.Length;

            int[] drawnNumbers = new int[numbersArrayLength];

            for (int i = 0; i < numbersArrayLength; i++)
            {
                drawnNumbers[i] = Convert.ToInt32(numbersAsStrings[i]);
            }

            return drawnNumbers;
        }

        private static List<int[,]> getBingoBoardData(string filepath)
        {
            const int gridSize = 5;
            const int startLineIndex = 1;

            string[] lines = File.ReadAllLines(filepath);
            List<int[,]> bingoBoardsList = new List<int[,]>();

            int[,] bingoBoard = new int[gridSize, gridSize];
            int boardLineIndex = 0;

            for (int i = startLineIndex; i < lines.Length; i++)
            {
                if (lines[i] == "")
                {
                    // Each new Bingo Board in the input data is preceded by a blank line
                    bingoBoard = new int[gridSize, gridSize];
                    boardLineIndex = 0;
                    bingoBoardsList.Add(bingoBoard);

                    continue;
                }

                string[] bingoLine = lines[i].Trim().Replace("  ", " ").Split(' ');

                for (int j = 0; j < bingoLine.Length; j++)
                {
                    bingoBoard[boardLineIndex, j] = Convert.ToInt32(bingoLine[j]);
                }

                boardLineIndex++;
            }

            return bingoBoardsList;
        }
    }

    
}
