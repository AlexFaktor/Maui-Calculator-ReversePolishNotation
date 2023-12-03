namespace Calculator
{
    public class Program
    {
        private static string ShowCalculateResult(string input)
        {
            try
            {
                return ExpressionCalculator.Calculate(input).ToString();
            }
            catch (ArgumentException ex)
            {
                return "Incorrect input format";
            }
            catch (DivideByZeroException ex)
            {
                return "Not divisible by 0";
            }
            catch (ArithmeticException ex)
            {
                return "Not all brackets are closed";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {

            }

            Console.Write("Enter the expression -> ");
            var input = Console.ReadLine();
            Console.WriteLine($"{input} -> {ShowCalculateResult(input)}");
        }
    }
}
