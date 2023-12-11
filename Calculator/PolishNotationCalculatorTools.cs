using System.Text;

namespace Calculator
{
    public class PolishNotationCalculatorTools
    {
        /// <summary>
        /// Gets a list of expressions, returns a list of solved expressions
        /// </summary>
        public static string CalculateList(string[] expressions)
        {
            StringBuilder output = new();

            for (int i = 0; i < expressions.Length; i++)
            {
                output.Append($"{expressions[i]} -> {PolishNotationCalculatorTools.CalculateString(expressions[i])}\n");
            }

            return output.ToString();
        }

        /// <summary>
        /// Convert Polish notation to string format and return errors
        /// </summary>
        public static string CalculateString(string expression)
        {
            try
            {
                return PolishNotationCalculator.Calculate(expression).ToString();
            }
            catch (ArgumentException)
            {
                return "Exception. Incorrect input format.";
            }
            catch (DivideByZeroException)
            {
                return "Exception. Not divisible by 0.";
            }
            catch (ArithmeticException)
            {
                return "Exception. Not all brackets are closed.";
            }
            catch (Exception)
            {
                return "Exception. Unexpected exception.";
            }

        }
    }
}
