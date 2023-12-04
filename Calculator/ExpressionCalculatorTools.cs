using System.Text;

namespace Calculator
{
    public class ExpressionCalculatorTools
    {
        /// <summary>
        /// Gets a list of expressions, returns a list of solved expressions
        /// </summary>
        public static string CalculateList(string[] expressions)
        {
            StringBuilder output = new();

            for (int i = 0; i < expressions.Length; i++)
            {
                output.Append($"{expressions[i]} -> {ExpressionCalculator.Calculate(expressions[i])}\n");
            }

            return output.ToString();
        }
    }
}
