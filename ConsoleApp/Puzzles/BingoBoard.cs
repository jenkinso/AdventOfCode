using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public class BingoBoard
    {
        private int[,] board;
        private int[,] markedBoard;
        private int gridSize;
        private int lastCalledNumber = 0;
        private bool _bingoStatus = false;
        public bool BingoStatus
        {
            get
            {
                return _bingoStatus;
            }
            set
            {
                _bingoStatus = value;

                if (value == true)
                {
                    calculateScore();
                    StillPlaying = false;
                }
            }
        }
        public int Score { get; private set; }
        public bool StillPlaying { get; private set; } = true;

        public BingoBoard(int[,] board)
        {
            this.board = board;

            gridSize = board.GetLength(0);
            
            markedBoard = new int[gridSize, gridSize];

            BingoStatus = false;
        }

        public void CallNumber(int number)
        {
            if (StillPlaying == false)
            {
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
                    BingoStatus = true;
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
                    BingoStatus = true;
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
