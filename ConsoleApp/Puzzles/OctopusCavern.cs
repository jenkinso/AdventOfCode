using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public class OctopusCavern
    {
        public long FlashCount { get; private set; }
        public List<int> BigFlashSteps { get; private set; } = new List<int>();
        private int[,] energies;
        private bool[,] flashed;
        private int numRows;
        private int numCols;
        private const int flashEnergy = 10;
        private const int energyChange = 1;


        public OctopusCavern(string[] inputData, int numSteps = 100)
        {
            generateEnergyArray(inputData);

            modelTheOctopuses(numSteps);
        }

        private void generateEnergyArray(string[] inputData)
        {
            numRows = inputData.GetLength(0);
            numCols = inputData[0].Length;

            flashed = new bool[numRows, numCols];

            int[,] energyArray = new int[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    energyArray[row, col] = Convert.ToInt32(inputData[row][col].ToString());
                }
            }

            this.energies = energyArray;
        }

        private void modelTheOctopuses(int numSteps)
        {
            for (int step = 1; step <= numSteps; step++)
            {
                incrementTheEnergies();

                checkForFlashes();

                resetForNextStep(step);
            }
        }

        private void incrementTheEnergies()
        {
            for (int row = 0; row < numCols; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    energies[row, col] += energyChange;
                }
            }
        }

        private void resetForNextStep(int stepNum)
        {
            int energiesSum = 0;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    flashed[row, col] = false;

                    if (energies[row, col] >= flashEnergy)
                    {
                        energies[row, col] = 0;
                    }

                    energiesSum += energies[row, col];
                }
            }

            if (energiesSum == 0)
            {
                BigFlashSteps.Add(stepNum);
            }
        }

        private void checkForFlashes()
        {
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    flash(row, col, 0);
                }
            }
        }

        private void flash(int row, int col, int energyIncrement)
        {
            if (row < 0 || row > numRows - 1 || col < 0 || col > numCols - 1) return;

            energies[row, col] += energyIncrement;

            if (flashed[row, col]) return;

            if (energies[row, col] < flashEnergy) return;

            FlashCount++;
            flashed[row, col] = true;

            flash(row, col - 1, energyChange);
            flash(row - 1, col - 1, energyChange);
            flash(row - 1, col, energyChange);
            flash(row - 1, col + 1, energyChange);
            flash(row, col + 1, energyChange);
            flash(row + 1, col + 1, energyChange);
            flash(row + 1, col, energyChange);
            flash(row + 1, col - 1, energyChange);

            return;
        }
    }
}
