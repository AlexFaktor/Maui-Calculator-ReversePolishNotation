namespace Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            { 

            }

            Console.Write("Enter the expression -> ");
            var input = Console.ReadLine();
            Console.WriteLine($"{input} -> {ExpressionCalculator.Calculate(input)}");
        }
    }
}
