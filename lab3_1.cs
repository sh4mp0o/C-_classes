using System;
using System.IO;

namespace lab2_1
{
    [Serializable]
    public class RepairException : Exception
    {
        public RepairException() { }
        public RepairException(string message) : base(message) { }
        public RepairException(string message, Exception inner) : base(message, inner) { }
    }
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
                try
                {
                    if (value < 20 || value > 400)
                        throw new ArgumentException("Недопустимое значение длины судна.");
                    else
                        _length = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(System.DateTime.Now.ToString());
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                catch(Exception ex)
                {
                    Log(ex);
                }
            }
        }
        public string Condition
        {
            get { return _condition; }
            set
            {
                try
                {
                    if (value != "sunk" && value != "repaired" && value != "broken")
                        throw new ArgumentException("Недопустимое значение состояния судна.");
                    else
                        _condition = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(System.DateTime.Now.ToString());
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
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
                if (this.GetType() == typeof(Corvette)) _Fuel = false;
                else _Fuel = true;
            }
        }
        public string Size
        {
            get { return _size; }
            set
            {
                try
                {
                    if (value != "small" && value != "medium" && value != "big")
                        throw new ArgumentException("Недопустимое значение размера судна.");
                    else
                        _size = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(System.DateTime.Now.ToString());
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            }
        }
        abstract public void Move();
        virtual public void Log(Exception ex)
        {
            Console.WriteLine("Что-то пошло не так...");
            Console.WriteLine(System.DateTime.Now.ToString());
            Console.WriteLine(ex.Message);
            using (StreamWriter sw = new StreamWriter("logs.txt", true))
            {
                sw.WriteLine(System.DateTime.Now.ToString());
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine(ex.InnerException);
                sw.WriteLine(ex.TargetSite);
                sw.WriteLine(ex.Data);
                sw.WriteLine(ex.Source);
            }
        }
        virtual public void Repair()
        {
            try
            {
                if (this.Condition == "broken")
                {
                    this.Condition = "repaired";
                    Console.WriteLine("Судно успешно отремонтировано.\n" +
                        "------------------------------------------------------------------------------");
                }
                else if (this.Condition == "sunk")
                {
                    throw new RepairException("Потонувшие суда нельзя починить!");
                }
                else Console.WriteLine("Судно не нуждается в ремонте.\n" +
                    "------------------------------------------------------------------------------");
            }
            catch (RepairException ex)
            {
                Console.WriteLine(System.DateTime.Now.ToString());
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
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
            try
            {
                if (this.Condition == "repaired")
                    Console.WriteLine($"Лайнер загрузил {this.Tourists} пассажиров и движется к берегам Карибского бассейна.\n" +
                        $"------------------------------------------------------------------------------");
                else throw new RepairException("Сначала отремонтируйте лайнер!");
            }
            catch (RepairException ex)
            {
                Console.WriteLine(System.DateTime.Now.ToString());
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
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
                try
                {
                    if (value > 0 && value < 5000)
                    {
                        _tourists_count = value;
                    }
                    else throw new ArgumentException("Количество туристов не может быть отрицательным и превышать 5000!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(System.DateTime.Now.ToString());
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
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
            try
            {
                if (this.Condition == "repaired")
                    Console.WriteLine($"Грузовой корабль перевозит {this.Containers} контейнеров.\n" +
                        $"------------------------------------------------------------------------------");
                else throw new RepairException("Сначала отремонтируйте грузовое судно!");
            }
            catch (RepairException ex)
            {
                Console.WriteLine(System.DateTime.Now.ToString());
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
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
            get { return _containerscnt; }
            set
            {
                try
                {
                    if (value > 0 && value < 18000) { _containerscnt = value; }
                    else throw new ArgumentException("Количество контейнеров не может быть отрицательным и превышать 18000!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(System.DateTime.Now.ToString());
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
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
            get { return _sailcnt; }
            set
            {
                try
                {
                    if (value > 0 && value < 11) _sailcnt = value;
                    else throw new ArgumentException("Количество палуб не может быть отрицательным и превышать 11!");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(System.DateTime.Now.ToString());
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            }
        }
        virtual public void Wind_Adjustment()
        {
            try
            {
                if (this.Condition == "repaired")
                {
                    Random rnd = new Random();
                    if (rnd.Next(1, 3) == 1) Console.WriteLine("Парусник успешно настроился по ветру.\n" +
                        "------------------------------------------------------------------------------");
                    else Console.WriteLine("Штиль! Никакого ветра нет.\n" +
                        "------------------------------------------------------------------------------");
                }
                else throw new RepairException("Сначала отремонтируйте судно!");
            }
            catch (RepairException ex)
            {
                Console.WriteLine(System.DateTime.Now.ToString());
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
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
            try
            {
                if (this.Condition == "repaired")
                    Console.WriteLine($"{this.Sailboatcnt}-палубный корвет бороздит просторы морей.\n" +
                        $"------------------------------------------------------------------------------");
                else throw new RepairException("Сначала отремонтируйте корвет!");
            }
            catch (RepairException ex)
            {
                Console.WriteLine(System.DateTime.Now.ToString());
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
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
            string fileName;
            while (true)
            {
                try
                {
                    Console.Write("Введите имя файла: ");
                    fileName = Console.ReadLine();
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        line = sr.ReadLine().Split();
                        ships[0] = new Liner(line[0], line[1], int.Parse(line[2]), int.Parse(line[3]));
                        line = sr.ReadLine().Split();
                        ships[1] = new Cargo_Ship(line[0], line[1], int.Parse(line[2]), int.Parse(line[3]));
                        line = sr.ReadLine().Split();
                        ships[2] = new Corvette(line[0], line[1], int.Parse(line[2]), int.Parse(line[3]));
                    }
                    break;
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Файл, имя которого вы ввели, не существует.\n" +
                        "Введите имя существующего файла.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Что-то пошло не так.");
                    Console.WriteLine(System.DateTime.Now.ToString());
                    Console.WriteLine(ex.Message);
                    using (StreamWriter sw = new StreamWriter("logs.txt", true))
                    {
                        sw.WriteLine(System.DateTime.Now.ToString());
                        sw.WriteLine(ex.Message);
                        sw.WriteLine(ex.StackTrace);
                        sw.WriteLine(ex.InnerException);
                        sw.WriteLine(ex.TargetSite);
                        sw.WriteLine(ex.Data);
                    }
                }
            }
            foreach (var ship in ships)
            {
                ship.Repair();
                ship.Move();
                Console.WriteLine(ship.ToString());
            }
            Console.ReadKey();
        }
    }
}
