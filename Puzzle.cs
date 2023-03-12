using System;
using System.IO;
namespace Hangman
{
	public class Puzzle
	{
        //Properties
        public string path { get; set; }
        public string[] puzzles { get; set; }

        //Constructor
        public Puzzle(string p)
        {
            this.path = p;
            this.puzzles = File.ReadAllLines(this.path);
        }

        //Methods
        public string GetRandPuzzle() //pulls a random puzzle from the input file
        {
            Random random = new Random();
            int seed = random.Next(this.puzzles.Length);
            return this.puzzles[seed];
        }

        public static string ConvertPhrase(string phrase) //method for converting strings to underlines
        {
            string output = "";
            foreach(char c in phrase)
            {
                if (c != ' ')
                {
                    output = output + "_";
                }
                else
                {
                    output = output + " ";
                }
            }
            return output;
        }

        public static bool CheckForUnderlines(string input) //method used in checking to see if all letters have been guessed
        {
            foreach (char c in input)
            {
                if(c == '_')
                {
                    return true;
                }
            }
            return false;
        }
    }
}

