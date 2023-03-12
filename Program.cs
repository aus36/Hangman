//Object Oriented Programming (CSCI 428) - Assignment II
//Hangman Game
//Austin Hale

//LogBook located in Hangman/bin/Debug/net7.0/

namespace Hangman;
public class Program
{
    public static void Main()
    {
        //Create new instance of puzzle class for managing puzzle list
        Puzzle puzzle = new Puzzle("puzzles.txt");

        //Start new games infinitely until user quits
        while (1==1)
        {
            //Pick a puzzle phrase and ask user for name
            string phrase = puzzle.GetRandPuzzle();

            Console.WriteLine("Enter your name: ");
            Player player = new Player(Console.ReadLine()!);
            Console.WriteLine();

            //Game begins with new puzzle phrase and player
            Game game = new Game(phrase, player);

            //Play game until win or loss
            while (!game.isOver)
            {
                game.Display();
                string userGuess = Game.GetUserInput();
                game.MakeGuess(userGuess);
            }

            //After a game is over, ask if player would like to play again
            Console.WriteLine("\nEnter q to quit, or any other key to start a new game.");
            if(Console.ReadLine() == "q")
            {
                Console.WriteLine("Thanks for playing!");
                break;
            }
        }
    }
}