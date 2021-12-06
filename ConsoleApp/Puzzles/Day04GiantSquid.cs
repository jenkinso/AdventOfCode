using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day04GiantSquid
    {
        public static string Parts1And2(string filepath)
        {
            // Get and process the input data
            int[] drawnNumbers = getDrawnNumbersFromFile(filepath);
            List<int[,]> bingoBoardData = getBingoBoardData(filepath);

            // Create our Bingo Boards using the input data
            List<BingoBoard> bingoBoards = new List<BingoBoard>();
            int boardCounter = 0;
            foreach (var board in bingoBoardData)
            {
                boardCounter++;
                bingoBoards.Add(new BingoBoard(board, boardCounter));
            }

            // Play Bingo!
            List<int> listOfFinishers = new List<int>();

            foreach (int number in drawnNumbers)
            {
                foreach (BingoBoard board in bingoBoards)
                {
                    bool wasStillPlaying = board.StillPlaying;

                    board.CallNumber(number);

                    if (board.Bingo && wasStillPlaying)
                    {
                        listOfFinishers.Add(bingoBoards.IndexOf(board));
                    }
                }
            }

            BingoBoard firstPlace = bingoBoards[listOfFinishers[0]];
            BingoBoard lastPlace = bingoBoards[listOfFinishers[listOfFinishers.Count - 1]];

            string puzzleAnswer = $"\nBoard {firstPlace.BoardNumber} won with a final score of {firstPlace.Score}." +
                                  $"\nBoard {lastPlace.BoardNumber} finished last with a final score of {lastPlace.Score}."; 

            return puzzleAnswer;
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
