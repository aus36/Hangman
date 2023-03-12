using System;
using System.Text;
using System.Xml.Linq;
namespace Hangman
{
	public class Game
	{
        //Properties
        public bool isOver { get; set; }
		public int guessesLeft { get; set; }
		public string incorrectGuesses { get; set; }
		public int successfulGuesses { get; set; }
		public bool winLoss { get; set; }
        public string guessPhrase { get; set; }
		public Player player { get; set; }
        public string phrase { get; set; }
        public DateTime dateTime { get; set; }

		//Constructor
        public Game(string phrase, Player P)
		{
			this.isOver = false;
			this.winLoss = false;
			this.guessesLeft = 6;
			this.incorrectGuesses = "";
			this.successfulGuesses = 0;
			this.player = P;
            this.guessPhrase = Puzzle.ConvertPhrase(phrase);
            this.phrase = phrase;
            this.dateTime = DateTime.Now;
		}
		
		//Methods
		public void Display() //method for showing the current game screen every turn
		{
			if(this.guessesLeft == 1)
			{
                Console.WriteLine("DANGER: Only 1 Guess Remaining!\n");
			}
			Console.WriteLine("             Welcome " + this.player.name);
			Console.WriteLine("-----------------------------------------");
			Game.DisplayStickMan(this.guessesLeft);
            Console.WriteLine("\nYour Puzzle: "+this.guessPhrase);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Guesses Remaining: " + this.guessesLeft);
			Console.WriteLine("Incorrect guesses: " + this.incorrectGuesses);
            Console.WriteLine("-----------------------------------------");

        }

        public static void DisplayStickMan(int lives) //method called by display to print the stick figure based on lives left
		{
			switch (lives)
			{
                case (6):
                    Console.WriteLine("  -------");
                    Console.WriteLine("  |/    |");
                    Console.WriteLine("  |     |");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("-----");
                    break;
                case (5):
					Console.WriteLine("  -------");
                    Console.WriteLine("  |/    |");
                    Console.WriteLine("  |     |");
                    Console.WriteLine("  |    (_)");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
					Console.WriteLine("-----");
                    break;
				case (4):
                    Console.WriteLine("  -------");
                    Console.WriteLine("  |/    |");
                    Console.WriteLine("  |     |");
                    Console.WriteLine("  |    (_)");
                    Console.WriteLine("  |     |");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("-----");
                    break;
                case (3):
                    Console.WriteLine("  -------");
                    Console.WriteLine("  |/    |");
                    Console.WriteLine("  |     |");
                    Console.WriteLine("  |    (_)");
                    Console.WriteLine("  |    -|");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("-----");
                    break;
                case (2):
                    Console.WriteLine("  -------");
                    Console.WriteLine("  |/    |");
                    Console.WriteLine("  |     |");
                    Console.WriteLine("  |    (_)");
                    Console.WriteLine("  |    -|-");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("-----");
                    break;
                case (1):
                    Console.WriteLine("  -------");
                    Console.WriteLine("  |/    |");
                    Console.WriteLine("  |     |");
                    Console.WriteLine("  |    (_)");
                    Console.WriteLine("  |    -|-");
                    Console.WriteLine("  |    /");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("-----");
                    break;
                case (0):
                    Console.WriteLine("  -------");
                    Console.WriteLine("  |/    |");
                    Console.WriteLine("  |     |");
                    Console.WriteLine("  |    (_)");
                    Console.WriteLine("  |    -|-");
                    Console.WriteLine("  |    / \\");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("-----");
                    break;
            }
				
		}

		public void MakeGuess(string guess) //method for player to make a letter guess with logic for effects of said guess
		{
            //Loop with error handling to ensure proper char input from user
            bool validInput = false;
            char guessLetter = ' ';
            while (!validInput)
            {
                try
                {
                    char.Parse(guess);
                }

                catch(System.FormatException ex)
                {
                    Console.WriteLine("Improper input: "+ex.Message+"\n");
                    continue;
                }

                guessLetter = char.Parse(guess);

                if (Char.IsLetter(guessLetter))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("That character is not a letter. Please enter another: \n");
                }
            }
            guessLetter = Char.ToLower(guessLetter);

            //Check user input char with the phrase to see if it was a correct guess
            bool success = false;
            foreach (char c in this.phrase)
            {
                if(guessLetter == c)
                {
                    success = true;
                }
            }

            //Reveal any correctly guessed characters
            if (success == true)
            {
                StringBuilder str = new StringBuilder(this.guessPhrase);
                for(int i=0; i<this.phrase.Length; i++)
                {
                    if (this.phrase[i] == guessLetter)
                    {
                        str[i] = guessLetter;
                    }
                }
                this.guessPhrase = str.ToString();
            }

            //Remove one life if wrong and add incorrect letter to display
            else
            {
                this.incorrectGuesses = this.incorrectGuesses + guessLetter + " ";
                this.guessesLeft--;
            }

            //Check to see if game is won or lost
            if (!Puzzle.CheckForUnderlines(this.guessPhrase))
            {
                this.player.wins++;
                this.winLoss = true;
                this.PrintGameEndScreen();
                this.LogGame();
                this.isOver = true;
            }
            else if(this.guessesLeft == 0)
            {
                this.player.losses++;
                this.winLoss = false;
                this.PrintGameEndScreen();
                this.LogGame();
                this.isOver = true;
            }
		}

        public void PrintGameEndScreen() //method for showing endgame screen based on win/loss
        {
            if (this.winLoss)
            {
                Console.WriteLine("\n-----------------------------------------");
                Console.WriteLine("You Won :) Congratulations " + this.player.name);
                Console.WriteLine("-----------------------------------------\n");
            }
            else
            {
                this.Display();
                Console.WriteLine("\n----------------------------------------------");
                Console.WriteLine("You Lost :( Better luck next time " + this.player.name);
                Console.WriteLine("----------------------------------------------\n");
            }
        }

        public void LogGame() //method to write to logbook after finishing each game
        {
            if (!File.Exists("LogBook.xml")) //if XML log doesn't exist, create XML log and create root and children of XML tree
            {
                File.CreateText("LogBook.xml");
                XElement x = new XElement("LogBook",
                    new XElement("Games"),
                    new XElement("Stats")
                    );
                x.Save("LogBook.xml");
            }

            var newGame = new XElement("Game", //create an XML element of the finished game
                new XElement("PlayerName", this.player.name),
                new XElement("DateTimePlayed", dateTime),
                new XElement("Phrase", this.phrase),
                new XElement("WonFlag", this.winLoss)
                );

            XDocument Log = XDocument.Load("LogBook.xml");
            Log.Root!.Element("Games")!.Add(newGame);
            Log.Save("LogBook.xml");

            this.player.LogPlayer(); //add this game's player to the XML log stats portion
        }

        public static string GetUserInput()
        {
            Console.WriteLine("Guess a letter: ");
            return Console.ReadLine()!;
        }
    }
}