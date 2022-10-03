using System;

namespace lab1_1
{
    public class Ship
    {
        private string _type;
        private string _condition;
        private int _number_of_decks;

        public Ship() : this("boat", "combat-ready", 1)
        {
        }
        public Ship(string type, string condition, int number_of_decks)
        {
            this.Type = type;
            this.Condition = condition;
            this.Number_Of_decks = number_of_decks;
        }
        public string Condition
        {
            get { return _condition; }
            set
            {
                if (value != "sunk" && value != "combat-ready" && value != "injured")
                    _condition = "sunk";
                else
                    _condition = value;
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                if (value != "battleship" && value != "cruiser" && value != "destroyer" && value != "boat")
                    _type = "boat";
                else
                    _type = value;
            }
        }
        public int Number_Of_decks
        {
            get { return _number_of_decks; }
            set
            {
                if (0 < value && value < 5) _number_of_decks = value;
                else _number_of_decks = 1;
            }
        }
        public void Repair()
        {
            if (this.Condition == "sunk")
                Console.WriteLine($"We can't repair a sunken ship.\nR.I.P {this.Type}...");
            else if (this.Condition == "injured")
            {
                this.Condition = "combat-ready";
                Console.WriteLine($"{this.Type} has been repaired.");
            }
            else if (this.Condition == "combat-ready")
                Console.WriteLine($"{this.Type} has already been repaired.");
        }
        public void Battle(Ship n1)
        {
            if (n1.Condition == "sunk" && this.Condition == "sunk")
                Console.WriteLine("draw");
            else if (n1.Condition != "sunk" && this.Condition == "sunk")
                Console.WriteLine($"{n1.Type} wins!");
            else if (this.Condition == "sunk" && n1.Condition != "sunk")
                Console.WriteLine($"{this.Type} wins!");
            else if (n1.Condition != "sunk" && this.Condition != "sunk" &&
                n1.Number_Of_decks > this.Number_Of_decks)
                Console.WriteLine($"{n1.Type} wins!");
            else if (n1.Condition != "sunk" && this.Condition != "sunk" &&
                n1.Number_Of_decks < this.Number_Of_decks)
                Console.WriteLine($"{this.Type} wins!");
            else
                Console.WriteLine("draw");
        }
        public static bool operator !=(Ship n1, Ship n2)
        {
            if (n1.Condition != n2.Condition)
            {
                if (n1.Type != n2.Type)
                {
                    if (n1.Number_Of_decks != n2.Number_Of_decks)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool operator ==(Ship n1, Ship n2)
        {
            if (n1.Condition == n2.Condition)
            {
                if (n1.Type == n2.Type)
                {
                    if (n1.Number_Of_decks == n2.Number_Of_decks)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override string ToString()
        {
            return String.Format("{0} is {1} and has {2} decks", _type, _condition, _number_of_decks);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Ship battleship = new Ship("battleship", "combat-ready", 20);
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
            battleship.Repair();
            boat.Repair();
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            battleship.Condition = "sunk";
            battleship.Repair();
            battleship.Condition = "combat-ready";
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            battleship.Number_Of_decks = 4;
            battleship.Battle(boat);
            battleship.Condition = "combat-ready";
            battleship.Type = "battleship";
            battleship.Number_Of_decks = 4;
            boat.Condition = "combat-ready";
            boat.Type = "battleship";
            boat.Number_Of_decks = 4;
            Console.WriteLine(battleship.ToString());
            Console.WriteLine(boat.ToString());
            Console.WriteLine(battleship == boat);
            Console.ReadKey();
        }
    }
}
