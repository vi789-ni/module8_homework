using System;

namespace PaymentAdapterPattern
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($" Оплата {amount} тг через PayPal успешно выполнена.");
            Console.ResetColor();
        }
    }

    public class StripePaymentService
    {
        public void MakeTransaction(double totalAmount)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($" Stripe: Транзакция на сумму {totalAmount} тг выполнена успешно.");
            Console.ResetColor();
        }
    }

    public class StripePaymentAdapter : IPaymentProcessor
    {
        private readonly StripePaymentService _stripe;

        public StripePaymentAdapter(StripePaymentService stripe)
        {
            _stripe = stripe;
        }

        public void ProcessPayment(double amount)
        {
            _stripe.MakeTransaction(amount);
        }
    }

    public class KaspiPaymentService
    {
        public void SendMoney(double amount)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" Kaspi: Оплата {amount} тг успешно проведена через Kaspi.kz.");
            Console.ResetColor();
        }
    }

    public class KaspiPaymentAdapter : IPaymentProcessor
    {
        private readonly KaspiPaymentService _kaspi;
        public KaspiPaymentAdapter(KaspiPaymentService kaspi)
        {
            _kaspi = kaspi;
        }

        public void ProcessPayment(double amount)
        {
            _kaspi.SendMoney(amount);
        }
    }

    public class HalykPaymentService
    {
        public void PayWithCard(double total)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($" Halyk Bank: Платеж на сумму {total} тг прошел успешно.");
            Console.ResetColor();
        }
    }

    public class HalykPaymentAdapter : IPaymentProcessor
    {
        private readonly HalykPaymentService _halyk;
        public HalykPaymentAdapter(HalykPaymentService halyk)
        {
            _halyk = halyk;
        }

        public void ProcessPayment(double amount)
        {
            _halyk.PayWithCard(amount);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("Введите сумму оплаты: ");
            double amount = double.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("\nВыберите способ оплаты:");
            Console.WriteLine("1 - PayPal");
            Console.WriteLine("2 - Stripe");
            Console.WriteLine("3 - Kaspi");
            Console.WriteLine("4 - Halyk");
            Console.Write("Ваш выбор: ");
            int choice = int.Parse(Console.ReadLine() ?? "1");

            IPaymentProcessor processor = choice switch
            {
                1 => new PayPalPaymentProcessor(),
                2 => new StripePaymentAdapter(new StripePaymentService()),
                3 => new KaspiPaymentAdapter(new KaspiPaymentService()),
                4 => new HalykPaymentAdapter(new HalykPaymentService()),
                _ => new PayPalPaymentProcessor()
            };

            Console.WriteLine("\n");
            processor.ProcessPayment(amount);
            Console.WriteLine("");
        }
    }
}
