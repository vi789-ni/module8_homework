using System;
using System.Collections.Generic;
using System.Linq;

namespace DecoratorPattern
{
    public interface IBeverage
    {
        string BaseName { get; }     
        List<string> Additives { get; } 
        string GetDescription();
        double Cost();
    }

    public abstract class BeverageBase : IBeverage
    {
        public abstract string BaseName { get; }
        public List<string> Additives { get; protected set; } = new List<string>();
        public abstract double Cost();

        public virtual string GetDescription()
        {
            if (Additives.Count == 0)
                return BaseName;

            if (Additives.Count == 1)
                return $"{BaseName} с {Additives[0]}";

            string last = Additives.Last();
            string others = string.Join(", ", Additives.Take(Additives.Count - 1));
            return $"{BaseName} с {others} и {last}";
        }
    }

    public class Espresso : BeverageBase
    {
        public override string BaseName => "кофе эспрессо";
        public override double Cost() => 300;
    }

    public class Tea : BeverageBase
    {
        public override string BaseName => "чай";
        public override double Cost() => 200;
    }

    public class Latte : BeverageBase
    {
        public override string BaseName => "латте";
        public override double Cost() => 350;
    }

    public class Mocha : BeverageBase
    {
        public override string BaseName => "мокко";
        public override double Cost() => 400;
    }

    public abstract class BeverageDecorator : IBeverage
    {
        protected IBeverage beverage;
        public abstract string AdditiveName { get; }

        public string BaseName => beverage.BaseName;
        public List<string> Additives => beverage.Additives;

        public BeverageDecorator(IBeverage beverage)
        {
            this.beverage = beverage;
            this.beverage.Additives.Add(AdditiveName);
        }

        public virtual string GetDescription() => beverage.GetDescription();
        public abstract double Cost();
    }

    public class Milk : BeverageDecorator
    {
        public override string AdditiveName => "молоком";
        public Milk(IBeverage beverage) : base(beverage) { }
        public override double Cost() => beverage.Cost() + 50;
    }

    public class Sugar : BeverageDecorator
    {
        public override string AdditiveName => "сахаром";
        public Sugar(IBeverage beverage) : base(beverage) { }
        public override double Cost() => beverage.Cost() + 20;
    }

    public class WhippedCream : BeverageDecorator
    {
        public override string AdditiveName => "взбитыми сливками";
        public WhippedCream(IBeverage beverage) : base(beverage) { }
        public override double Cost() => beverage.Cost() + 70;
    }

    public class Caramel : BeverageDecorator
    {
        public override string AdditiveName => "карамелью";
        public Caramel(IBeverage beverage) : base(beverage) { }
        public override double Cost() => beverage.Cost() + 60;
    }

    public class Cinnamon : BeverageDecorator
    {
        public override string AdditiveName => "корицей";
        public Cinnamon(IBeverage beverage) : base(beverage) { }
        public override double Cost() => beverage.Cost() + 40;
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Выберите напиток:");
            Console.WriteLine("1 - Эспрессо");
            Console.WriteLine("2 - Чай");
            Console.WriteLine("3 - Латте");
            Console.WriteLine("4 - Мокко");
            Console.Write("Ваш выбор: ");
            int drinkChoice = int.Parse(Console.ReadLine() ?? "1");

            IBeverage beverage = drinkChoice switch
            {
                1 => new Espresso(),
                2 => new Tea(),
                3 => new Latte(),
                4 => new Mocha(),
                _ => new Espresso()
            };

            bool adding = true;
            while (adding)
            {
                Console.WriteLine("\nДобавить что-то еще?");
                Console.WriteLine("1 - Молоко");
                Console.WriteLine("2 - Сахар");
                Console.WriteLine("3 - Взбитые сливки");
                Console.WriteLine("4 - Карамель");
                Console.WriteLine("5 - Корица");
                Console.WriteLine("0 - Готово");
                Console.Write("Ваш выбор: ");
                int addChoice = int.Parse(Console.ReadLine() ?? "0");

                switch (addChoice)
                {
                    case 1: beverage = new Milk(beverage); break;
                    case 2: beverage = new Sugar(beverage); break;
                    case 3: beverage = new WhippedCream(beverage); break;
                    case 4: beverage = new Caramel(beverage); break;
                    case 5: beverage = new Cinnamon(beverage); break;
                    case 0: adding = false; break;
                    default: Console.WriteLine("Неверный выбор."); break;
                }
            }

            Console.WriteLine($"\nВаш заказ: {beverage.GetDescription()}");
            Console.WriteLine($"Итоговая стоимость: {beverage.Cost()} тг");
        }
    }
}
