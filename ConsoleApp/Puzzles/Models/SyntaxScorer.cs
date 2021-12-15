using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Puzzles.Models
{
    public class SyntaxScorer
    {
        public List<string> Code { get; private set; }
        public int CorruptedLineCount { get; private set; }
        public int SyntaxErrorScore { get; private set; }
        public List<long> LineCompletionScores { get; private set; } = new List<long>();
        private Dictionary<char, char> tagPairs = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        private Dictionary<char, int> illegalCharacterScores = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        private Dictionary<char, int> completetionCharacterScores = new Dictionary<char, int>() { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };

        public SyntaxScorer(string[] code)
        {
            Code = new List<string>(code);

            parseLines();
        }

        public long getWinningCompletionScore()
        {
            LineCompletionScores.Sort();

            return LineCompletionScores[LineCompletionScores.Count / 2];
        }

        private void parseLines()
        {
            for (int lineIndex = 0; lineIndex < Code.Count; lineIndex++)
            {
                Stack<char> openedTags = new Stack<char>();

                bool isLineCorrupted = checkCorrupted(Code[lineIndex], openedTags);

                if (isLineCorrupted == false)
                {
                    string completedLine = completeTheLine(Code[lineIndex], openedTags);

                    Code[lineIndex] = completedLine;
                }
            }
        }

        private bool checkCorrupted(string line, Stack<char> openedTags)
        {
            bool isLineCorrupted = false;

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
                        isLineCorrupted = true;

                        break;
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Invalid character found. '{character}' is neither a valid opening or closing tag.");
                }
            }

            return isLineCorrupted;
        }

        private string completeTheLine(string line, Stack<char> openedTags)
        {
            int numOpenedTags = openedTags.Count;
            long completionScore = 0;

            for (int i = 0; i < numOpenedTags; i++)
            {
                char lastOpenedTag = openedTags.Pop();

                char closingTag = tagPairs[lastOpenedTag];

                line += closingTag;

                completionScore = completionScore * 5 + completetionCharacterScores[closingTag];
            }

            LineCompletionScores.Add(completionScore);

            return line;
        }
    }
}
