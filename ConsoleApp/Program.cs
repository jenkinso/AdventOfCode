using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApp.Puzzles;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine($"Solution to Day 1 Part 1 is {Day01SonarSweep.Part1(@"./data/day01_input.txt")}");
            //Console.WriteLine($"Solution to Day 1 Part 2 is {Day01SonarSweep.Part2(@"./data/day01_input.txt")}");

            //Console.WriteLine($"Solution to Day 2 Part 1 is {Day02Dive.Part1(@"./data/day02_input.txt")}");
            //Console.WriteLine($"Solution to Day 2 Part 2 is {Day02Dive.Part2(@"./data/day02_input.txt")}");

            //Console.WriteLine($"Solution to Day 3 Part 1 is {Day03BinaryDiagnostic.Part1(@"./data/day03_input.txt")}");
            //Console.WriteLine($"Solution to Day 3 Part 2 is {Day03BinaryDiagnostic.Part2(@"./data/day03_input.txt")}");

            Console.WriteLine($"Solution to Day 4 is: {Day04GiantSquid.Parts1And2(@"./data/day04_input.txt")}");

            Console.ReadLine();
        }        
    }
}
