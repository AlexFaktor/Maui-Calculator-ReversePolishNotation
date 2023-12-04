namespace Calculator
{
    public class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 2)
            {
                var text = ExpressionCalculatorTools.CalculateList(File.ReadAllLines(args[0]).ToArray());
                StreamWriter writer = File.CreateText(args[1]);
                writer.WriteLine(text);
                writer.Close();

                return 0;
            }

            Console.Write("Enter the expression -> ");
            var input = Console.ReadLine();
            Console.WriteLine($"{input} -> {ExpressionCalculator.Calculate(input!)}");

            return 0;
        }
    }
}
