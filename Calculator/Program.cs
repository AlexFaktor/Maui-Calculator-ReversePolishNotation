namespace Calculator
{
    public class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 2)
            {
                using StreamReader reader = File.OpenText(args[0]);
                using StreamWriter writer = File.CreateText(args[1]);

                string line;
                while ((line = reader.ReadLine()!) != null)
                {
                    var result = $"{line} -> {PolishNotationCalculatorTools.CalculateString(line)}";

                    writer.WriteLine(result);
                }
                return 0;
            }

            Console.Write("Enter the expression -> ");
            var input = Console.ReadLine();
            Console.WriteLine($"{input} -> {PolishNotationCalculator.Calculate(input!)}");

            return 0;
        }
    }
}
