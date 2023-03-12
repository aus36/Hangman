using System;
using System.Xml.Linq;
namespace Hangman
{
	public class Player
	{
        //Properties
        public string? name { get; set; }
		public int wins { get; set; }
		public int losses { get; set; }
		public decimal winPercentage { get; set; }

        //Constructor
        public Player(string n)
		{
			this.name = n;
			this.wins = 0;
			this.losses = 0;
			this.winPercentage = 0;
		}

		//Methods
		public void UpdateWinPercentage() //method for updating winningpercentage for use in XML log
		{
			this.winPercentage = Decimal.Multiply(Decimal.Divide(Convert.ToDecimal(this.wins), (Decimal.Add(Convert.ToDecimal(this.wins), Convert.ToDecimal(this.losses)))), 100);
		}

		public void LogPlayer() //XML logging method for stats portion
		{
			this.UpdateWinPercentage(); //make sure winpercent is up to date for logging

            var newPlayer = new XElement("Player", //create an XML element of the finished game
                new XElement("Name", this.name),
                new XElement("GamesWon", this.wins),
                new XElement("GamesLost", this.losses),
                new XElement("WinningPercentage", this.winPercentage)
                );

            XDocument Log = XDocument.Load("LogBook.xml");
            Log.Root!.Element("Stats")!.Add(newPlayer);
            Log.Save("LogBook.xml");
        }
	}
}

