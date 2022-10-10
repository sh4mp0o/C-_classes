using System;
using System.IO;

namespace lab2_1
{
    abstract class Ship
    {
        private string _condition;
        private string _size;
        private int _length;
        private bool _isPassanger;
        private bool _Fuel;
        public Ship(string condition, string size, int length)
        {
            this.Size = size;
            this.Condition = condition;
            this.Length = length;
        }
        public int Length
        {
            get { return _length; }
            set
            {
                if (value < 20 || value > 400)
                    _length = 20;
                else
                    _length = value;
            }
        }
        public string Condition
        {
            get { return _condition; }
            set
            {
                if (value != "sunk" && value != "repaired" && value != "broken")
                    _condition = "sunk";
                else
                    _condition = value;
            }
        }
        public bool IsPassanger
        {
            get { return _isPassanger; }
            set
            {
                if (this.GetType() == typeof(Liner) || this.GetType() == typeof(Corvette)) _isPassanger = true;
                else _isPassanger = false;
            }
        }
        public bool Fuel
        {
            get { return _Fuel; }
            set
            {
                if(this.GetType() == typeof(Corvette)) _Fuel = false;
                else _Fuel = true;
            }
        }
        public string Size
        {
            get { return _size; }
            set
            {
                if (value != "small" && value != "medium" && value != "big")
                    _size = "medium";
                else
                    _size = value;
            }
        }
        abstract public void Move();
        virtual public void Repair()
        {
            if (this.Condition == "broken")
            {
                this.Condition = "repaired";
                Console.WriteLine("Судно успешно отремонтировано.\n" +
                    "------------------------------------------------------------------------------");
            }
            else if (this.Condition == "sunk")
            {
                Console.WriteLine("Затонувшее судно нельзя отремонтировать.\n" +
                    "------------------------------------------------------------------------------");
            }
            else Console.WriteLine("Судно не нуждается в ремонте.\n" +
                "------------------------------------------------------------------------------");
        }
        public static bool operator !=(Ship n1, Ship n2)
        {
            if (n1.Size != n2.Size)
            {
                if (n1.Length != n2.Length)
                {
                }
            }
            return false;
        }
        public static bool operator ==(Ship n1, Ship n2)
        {
            if (n1.Size == n2.Size)
            {
                if (n1.Length == n2.Length)
                {
                }
            }
            return false;
        }
    }
    class Liner : Ship
    {
        private int _tourists_count;

        public override void Move()
        {
            if (this.Condition == "repaired")
                Console.WriteLine($"Лайнер загрузил {this.Tourists} пассажиров и движется к берегам Карибского бассейна.\n" +
                    $"------------------------------------------------------------------------------");
            else Console.WriteLine("Сначала отремонтируйте судно!");
        }
        public Liner() : this("repaired", "medium", 100, 1500) { }
        public Liner(string condition, string size, int length, int touristcnt) : base(condition, size, length)
        {
            this.Tourists = touristcnt;
            this.IsPassanger = true;
            this.Fuel = true;
        }
        public int Tourists
        {
            get { return _tourists_count; }
            set
            {
                if (value > 0 && value < 5000)
                {
                    _tourists_count = value;
                }
                else _tourists_count = 1000;
            }
        }
        public override string ToString()
        {
            return String.Format(
                $"Длина лайнера: {this.Length},\nРазмер лайнера: {this.Size}," +
                $"\nКоличество пассажиров: {this.Tourists},\nСостояние лайнера: {this.Condition}," +
                $"\nПассажирский: {this.IsPassanger},\nНужно топливо: {this.Fuel}\n" +
                $"------------------------------------------------------------------------------");
        }
    }
    class Cargo_Ship : Ship
    {
        private int _containerscnt;
        public override void Move()
        {
            if (this.Condition == "repaired")
                Console.WriteLine($"Грузовой корабль перевозит {this.Containers} контейнеров.\n" +
                $"------------------------------------------------------------------------------");
            else Console.WriteLine("Сначала отремонтируйте судно!");
        }
        public Cargo_Ship() : this("repaired", "big", 200, 15000) { }
        public Cargo_Ship(string condition, string size, int length, int contscnt) : base(condition, size, length)
        {
            this.Containers = contscnt;
            this.Fuel = true;
            this.IsPassanger = false;
        }
        public int Containers
        {
            get { return _containerscnt;}
            set
            {
                if(value > 0 && value < 18000) { _containerscnt = value; }
                else _containerscnt = 1000;
            }
        }
        public override string ToString()
        {
            return String.Format(
                $"Длина грузового корабля: {this.Length},\nРазмер грузового корабля: {this.Size}," +
                $"\nКоличество контейнеров: {this.Containers},\nСостояние грузового корабля: {this.Condition}," +
                $"\nПассажирский: {this.IsPassanger},\nНужно топливо: {this.Fuel}\n" +
                $"------------------------------------------------------------------------------");
        }
    }
    abstract class Sailboat : Ship
    {
        private int _sailcnt;
        public Sailboat(string condition, string size, int length, int sails) : base(condition, size, length) { }
        public int Sailboatcnt
        {
            get { return _sailcnt;}
            set
            {
                if (value > 0 && value < 11) _sailcnt = value;
                else _sailcnt = 3;
            }
        }
        virtual public void Wind_Adjustment()
        {
            if (this.Condition == "repaired")
            {
                Random rnd = new Random();
                if (rnd.Next(1, 3) == 1) Console.WriteLine("Парусник успешно настроился по ветру.\n" +
                    "------------------------------------------------------------------------------");
                else Console.WriteLine("Штиль! Никакого ветра нет.\n" +
                    "------------------------------------------------------------------------------");
            }
            else Console.WriteLine("Сначала отремонтируйте судно!");
        }
    }
    class Corvette : Sailboat
    {
        public Corvette() : this("repaired", "medium", 70, 3) { }
        public Corvette(string condition, string size, int length, int sails) : base(condition, size, length, sails)
        {
            this.Sailboatcnt = sails;
            this.Fuel = false;
            this.IsPassanger = true;
        }
        public override void Move()
        {
            if (this.Condition == "repaired")
                Console.WriteLine($"{this.Sailboatcnt}-палубный корвет бороздит просторы морей.\n" +
                $"------------------------------------------------------------------------------");
            else Console.WriteLine("Сначала отремонтируйте судно!");
        }
        public override string ToString()
        {
            return String.Format(
                $"Длина парусного судна: {this.Length},\nРазмер парусного судна: {this.Size}," +
                $"\nКоличество палуб: {this.Sailboatcnt},\nСостояние парусного судна: {this.Condition}," +
                $"\nПассажирский: {this.IsPassanger},\nНужно топливо: {this.Fuel}\n" +
                $"------------------------------------------------------------------------------");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Ship[] ships = new Ship[3];
            string[] line = new string[4];
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                line = sr.ReadLine().Split();
                ships[0] = new Liner(line[0],line[1],int.Parse(line[2]), int.Parse(line[3]));
                line = sr.ReadLine().Split();
                ships[1] = new Cargo_Ship(line[0], line[1], int.Parse(line[2]), int.Parse(line[3]));
                line = sr.ReadLine().Split();
                ships[2] = new Corvette(line[0], line[1], int.Parse(line[2]), int.Parse(line[3]));
            }
            foreach (var ship in ships)
            {
                ship.Repair();
                ship.Move();
                Console.WriteLine(ship.ToString());
            }
            //Liner liner = new Liner();
            //Cargo_Ship cargo = new Cargo_Ship();
            //Corvette corvette = new Corvette();
            //Console.WriteLine(corvette.ToString());
            //corvette.Move();
            //Console.WriteLine(liner.ToString());
            //liner.Move();
            //Console.WriteLine(cargo.ToString());
            //cargo.Move();
            //corvette.Wind_Adjustment();
            //cargo.Condition = "broken";
            //liner.Condition = "sunk";
            //Console.WriteLine(corvette.ToString());
            //Console.WriteLine(liner.ToString());
            //Console.WriteLine(cargo.ToString());
            //cargo.Repair();
            //liner.Repair();
            //corvette.Repair();
            //Console.WriteLine(corvette.ToString());
            //Console.WriteLine(liner.ToString());
            //Console.WriteLine(cargo.ToString());
            Console.ReadKey();
        }
    }
}
