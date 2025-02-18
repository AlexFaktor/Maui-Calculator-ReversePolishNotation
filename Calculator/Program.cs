namespace Calculator
{
    public class Program
    {
        public static string CalculateString(string expression)
        {
            try
            {
                return PolishNotationCalculator.Calculate(expression).ToString();
            }
            catch (ArgumentException)
            {
                return "ArgumentException. Incorrect input format.";
            }
            catch (DivideByZeroException)
            {
                return "DivideByZeroException. Not divisible by 0.";
            }
            catch (ArithmeticException)
            {
                return "ArithmeticException. Not all brackets are closed.";
            }
            catch (InvalidOperationException)
            {
                return "InvalidOperationException. The operation in the expression is invalid";
            }
            catch (Exception)
            {
                return "Exception. Unexpected exception.";
            }

        }

        static int Main(string[] args)
        {
            if (args.Length == 2)
            {
                using StreamReader reader = File.OpenText(args[0]);
                using StreamWriter writer = File.CreateText(args[1]);

                string line;
                while ((line = reader.ReadLine()!) != null)
                {
                    var result = $"{line} -> {CalculateString(line)}";

                    writer.WriteLine(result);
                }
                return 0;
            }

            Console.Write("Enter the expression -> ");
            var input = Console.ReadLine();
            Console.WriteLine($"{input} -> {CalculateString(input!)}");

            return 0;
        }
    }
}
