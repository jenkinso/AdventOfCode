using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    public static class HelperMethods
    {
        public static int[] GetArrayOfNumbersFromFile(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            List<int> numbersList = new List<int>();

            int number;
            for (int i = 0; i < lines.Length; i++)
            {
                bool succeeded = int.TryParse(lines[i], out number);

                if (succeeded)
                {
                    numbersList.Add(number);
                }
            }

            return numbersList.ToArray();
        }
    }
}
