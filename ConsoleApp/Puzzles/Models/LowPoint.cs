using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles.Models
{
    public class LowPoint
    {
        public (int row, int col) Coords { get; private set; }
        public int Height { get; private set; }
        public int RiskLevel { get; private set; }        
        public List<(int row, int col)> BasinLocations { get; private set; } = new List<(int row, int col)>();
        public int BasinSize 
        {
            get { return BasinLocations.Count; }
        }

        public LowPoint(int row, int col, int height, int[,] heightMap)
        {
            Coords = (row, col);
            Height = height;
            RiskLevel = height + 1;            

            findBasinLocations(Coords, -1, heightMap);
        }

        private void findBasinLocations((int row, int col) coord, int heightAdjacentPoint, int[,] heightMap)
        {
            if (coord.row < 0 || coord.row > heightMap.GetLength(0) - 1 || coord.col < 0 || coord.col > heightMap.GetLength(1) - 1) return;

            if (heightMap[coord.row, coord.col] == 9) return;

            if (BasinLocations.Contains(coord)) return;

            if (heightAdjacentPoint < heightMap[coord.row, coord.col])
            {
                BasinLocations.Add(coord);
                findBasinLocations((coord.row - 1, coord.col), heightMap[coord.row, coord.col], heightMap);
                findBasinLocations((coord.row + 1, coord.col), heightMap[coord.row, coord.col], heightMap);
                findBasinLocations((coord.row, coord.col - 1), heightMap[coord.row, coord.col], heightMap);
                findBasinLocations((coord.row, coord.col + 1), heightMap[coord.row, coord.col], heightMap);

                return;
            }

            return;
        }
    }
}
