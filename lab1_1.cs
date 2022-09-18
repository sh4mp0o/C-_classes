using System;

namespace lab1_1
{
    public class Ship
    {
        private string type;
        private string condition;
        private int number_of_decks;

        public Ship()
        {
            type = "boat";
            condition = "combat-ready";
            number_of_decks = 1;
        }
        public Ship(string type, string condition, int number_of_decks)
        {
            this.type = type;
            this.condition = condition;
            this.number_of_decks = number_of_decks;
        }
        public string Condition
        {
            get { return condition; }
            set
            {
                if (value != "sunk" && value != "combat-ready" && value != "injured")
                    condition = "sunk";
                else
                    condition = value;
            }
        }
        public string Type
        {
            get { return type; }
            set
            {
                if (value != "battleship" && value != "cruiser" && value != "destroyer" && value != "boat")
                    type = "boat";
                else
                    type = value;
            }
        }
        public int Number_Of_decks
        {
            get { return number_of_decks; }
            set
            {
                if (0 < value && value < 5) number_of_decks = value;
                else number_of_decks = 1;
            }
        }
        public void Repair(Ship ship)
        {
            if (ship.condition == "sunk")
                Console.WriteLine($"We can't repair a sunken ship.\nR.I.P {ship.Type}...");
            else if (ship.condition == "injured")
            {
                ship.condition = "combat-ready";
                Console.WriteLine($"{ship.Type} has been repaired.");
            }
            else if (ship.condition == "combat-ready")
                Console.WriteLine($"{ship.Type} has already been repaired.");
        }
        public void Battle(Ship n1, Ship n2)
        {
            if (n1.condition == "sunk" && n2.condition == "sunk")
                Console.WriteLine("draw");
            else if (n1.condition != "sunk" && n2.condition == "sunk")
                Console.WriteLine($"{n1.Type} wins!");
            else if (n2.condition == "sunk" && n1.condition != "sunk")
                Console.WriteLine($"{n2.Type} wins!");
            else if (n1.condition != "sunk" && n2.condition != "sunk" &&
                n1.number_of_decks > n2.number_of_decks)
                Console.WriteLine($"{n1.Type} wins!");
            else if (n1.condition != "sunk" && n2.condition != "sunk" &&
                n1.number_of_decks < n2.number_of_decks)
                Console.WriteLine($"{n2.Type} wins!");
            else
                Console.WriteLine("draw");
        }
        public override string ToString()
        {
            return String.Format("{0} is {1} and has {2} decks", type, condition, number_of_decks);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Ship battleship = new Ship("battleship", "combat-ready", 4);
            Ship boat = new Ship();
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            battleship.Condition = "injured";
            boat.Condition = "injured";
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            battleship.Type = "aboba";
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            battleship.Type = "battleship";
            battleship.Number_Of_decks = 0;
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            Console.WriteLine(battleship.Number_Of_decks);
            battleship.Repair(battleship);
            boat.Repair(boat);
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            battleship.Condition = "sunk";
            battleship.Repair(battleship);
            battleship.Condition = "combat-ready";
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            battleship.Number_Of_decks = 4;
            battleship.Battle(boat, battleship);
            Console.ReadKey();
        }
    }
}
