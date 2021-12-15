using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles.Models
{
    public class BingoBoard
    {
        private int[,] board;
        private int[,] markedBoard;
        private int gridSize;
        private int lastCalledNumber = 0;
        private bool _bingo = false;
        public bool Bingo
        {
            get
            {
                return _bingo;
            }
            set
            {
                _bingo = value;

                if (value == true)
                {
                    calculateScore();
                    StillPlaying = false;
                }
            }
        }
        public int Score { get; private set; }
        public bool StillPlaying { get; private set; } = true;
        public int BoardNumber { get; private set; }

        public BingoBoard(int[,] board, int boardNumber)
        {
            this.board = board;

            gridSize = board.GetLength(0);  
            
            markedBoard = new int[gridSize, gridSize];

            this.BoardNumber = boardNumber;
        }

        public void CallNumber(int number)
        {
            if (StillPlaying == false)
            {
                // don't mark any new numbers or alter any state
                return;
            }

            lastCalledNumber = number;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (board[row, col] == number)
                    {
                        markedBoard[row, col] = 1;
                    }
                }
            }

            checkBingoStatus();
        }

        private void checkBingoStatus()
        {
            int markedSum = 0;

            // check each row for Bingo
            for (int row = 0; row < gridSize; row++)
            {
                markedSum = 0;

                for (int col = 0; col < gridSize; col++)
                {
                    markedSum += markedBoard[row, col];
                }

                if (markedSum == gridSize)
                {
                    Bingo = true;
                    return;
                }
            }

            // check each column for Bingo
            for (int col = 0; col < gridSize; col++)
            {
                markedSum = 0;

                for (int row = 0; row < gridSize; row++)
                {
                    markedSum += markedBoard[row, col];
                }

                if (markedSum == gridSize)
                {
                    Bingo = true;
                    return;
                }
            }
        }

        private void calculateScore()
        {
            int sumOfUnmarkedNumbers = 0;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (markedBoard[row, col] == 0)
                    {
                        sumOfUnmarkedNumbers += board[row, col];
                    }
                }
            }

            Score = sumOfUnmarkedNumbers * lastCalledNumber;
        }
    }
}
