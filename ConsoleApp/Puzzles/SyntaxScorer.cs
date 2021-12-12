using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles
{
    public class SyntaxScorer
    {
        public string[] Code { get; private set; }
        public int CorruptedLineCount { get; private set; }
        public int SyntaxErrorScore { get; private set; }
        private Dictionary<char, char> tagPairs = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        private Dictionary<char, int> illegalCharacterScores = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } }; 

        public SyntaxScorer(string[] code)
        {
            Code = code;

            findCorruptedLines();
        }

        private void findCorruptedLines()
        {
            Stack <char> openedTags = new Stack<char>();

            foreach (string line in Code)
            {
                foreach (char character in line)
                {
                    bool isOpeningTag = tagPairs.ContainsKey(character);
                    bool isClosingTag = tagPairs.ContainsValue(character);

                    if (isOpeningTag)
                    {
                        openedTags.Push(character);
                    }
                    else if (isClosingTag)
                    {
                        if (openedTags.Count == 0)
                        {
                            throw new InvalidOperationException("Closing tag found when there are no opening tags on the stack.");
                        }

                        char lastOpenedTag = openedTags.Pop();

                        if (tagPairs[lastOpenedTag] != character)
                        {
                            CorruptedLineCount++;
                            SyntaxErrorScore += illegalCharacterScores[character];
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"Invalid character found. '{character}' is neither a valid opening or closing tag.");
                    }
                }
            }
        }
    }
}
