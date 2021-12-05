using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public static class Day02Dive
    {
        public static int Part1(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            int horizontal = 0;
            int depth = 0;

            // assuming no errors/inconsistencies in the input data!
            foreach (string line in lines)
            {
                string[] split = line.Split(' ');

                string commandString = split[0];
                int commandMagnitude = Convert.ToInt32(split[1]);

                if (commandString == "forward")
                {
                    horizontal += commandMagnitude;
                }
                else if (commandString == "down")
                {
                    depth += commandMagnitude;
                }
                else if (commandString == "up")
                {
                    depth -= commandMagnitude;
                }
            }

            return horizontal * depth;
        }

        public static int Part2(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            // assuming no errors/inconsistencies in the input data!
            foreach (string line in lines)
            {
                string[] split = line.Split(' ');

                string commandString = split[0];
                int commandMagnitude = Convert.ToInt32(split[1]);

                if (commandString == "forward")
                {
                    horizontal += commandMagnitude;
                    depth += (aim * commandMagnitude);
                }
                else if (commandString == "down")
                {
                    aim += commandMagnitude;
                }
                else if (commandString == "up")
                {
                    aim -= commandMagnitude;
                }
            }

            return horizontal * depth;
        }
    }
}
