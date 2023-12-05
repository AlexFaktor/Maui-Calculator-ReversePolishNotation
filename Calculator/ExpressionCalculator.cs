using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class ExpressionCalculator
    {
        /// <summary>
        /// removes empty lines after Regax
        /// </summary>
        private static List<string> RemoveEmptyStrings(string[] strings)
        {
            List<string> cleanStrings = new();
            for (int i = 0; i < strings.Length; i++)
            {
                if (!strings[i].Equals(""))
                    cleanStrings.Add(strings[i]);
            }

            return cleanStrings;
        }

        /// <summary>
        /// Checks for available characters
        /// </summary>

        private static bool IsAnInvalidCharacter(string line)
        {
            var pattern = @"[^0-9-+*\/(),.]";
            Regex regex = new(pattern);
            return regex.Matches(line).Any();
        }

        /// <summary>
        /// Returns a bool value, depending on whether the brackets are in the correct order, and also returns their number in out
        /// </summary>
        private static bool IsAllBracketsClosedOutCountBrackets(string line, out int countBrackets)
        {
            var unclosedBrackets = 0;
            int count = 0;

            foreach (char c in line)
            {
                if (c == '(')
                {
                    unclosedBrackets++;
                    count++;
                }

                if (c == ')')
                    unclosedBrackets--;
                if (unclosedBrackets < 0)
                    throw new ArithmeticException();
            }
            countBrackets = count;
            return unclosedBrackets == 0;
        }

        private static string ExpressionArithmeticClear(string input)
        {
            input = input.Replace("*+", "*");
            input = input.Replace("/+", "/");
            input = input.Replace("--", "+");
            input = input.Replace("++", "+");
            input = input.Replace("-+", "-");

            return input;
        }

        /// <summary>
        /// Finds and returns the most appropriate operation in an expression, without brackets
        /// </summary>
        private static string FindOperation(string input)
        {
            var pattern = @"([-+\/*])|([^-+\/*]*)";
            Regex regex = new(pattern);
            List<string> elements = RemoveEmptyStrings(regex.Split(input));

            StringBuilder needToChange = new();

            if (elements.Contains("*") || elements.Contains("/"))
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    if (elements[i] == "*" || elements[i] == "/")
                    {
                        if (elements.ElementAtOrDefault(i - 2) != "-" && elements.ElementAtOrDefault(i + 1) != "-")
                        {
                            needToChange.Append(elements[i - 1]);
                            needToChange.Append(elements[i]);
                            needToChange.Append(elements[i + 1]);
                            return needToChange.ToString();
                        }
                        if (elements.ElementAtOrDefault(i - 2) == "-" && elements.ElementAtOrDefault(i + 1) != "-")
                        {
                            needToChange.Append(elements[i - 2]);
                            needToChange.Append(elements[i - 1]);
                            needToChange.Append(elements[i]);
                            needToChange.Append(elements[i + 1]);
                            return needToChange.ToString();
                        }
                        if (elements.ElementAtOrDefault(i - 2) != "-" && elements.ElementAtOrDefault(i + 1) == "-")
                        {
                            needToChange.Append(elements[i - 1]);
                            needToChange.Append(elements[i]);
                            needToChange.Append(elements[i + 1]);
                            needToChange.Append(elements[i + 2]);
                            return needToChange.ToString();
                        }

                        if (elements.ElementAtOrDefault(i - 2) == "-" && elements.ElementAtOrDefault(i + 1) == "-")
                        {
                            needToChange.Append(elements[i - 2]);
                            needToChange.Append(elements[i - 1]);
                            needToChange.Append(elements[i]);
                            needToChange.Append(elements[i + 1]);
                            needToChange.Append(elements[i + 2]);
                            return needToChange.ToString();
                        }
                    }
                }
            }
            else if (elements.Contains("-") || elements.Contains("+"))
            {
                for (int i = 1; i < elements.Count; i++)
                {
                    if (elements[i] == "-" || elements[i] == "+")
                    {
                        if (elements.ElementAtOrDefault(i - 2) != "-" && elements.ElementAtOrDefault(i + 1) != "-")
                        {
                            needToChange.Append(elements[i - 1]);
                            needToChange.Append(elements[i]);
                            needToChange.Append(elements[i + 1]);
                            return needToChange.ToString();
                        }
                        if (elements.ElementAtOrDefault(i - 2) == "-" && elements.ElementAtOrDefault(i + 1) != "-")
                        {
                            needToChange.Append(elements[i - 2]);
                            needToChange.Append(elements[i - 1]);
                            needToChange.Append(elements[i]);
                            needToChange.Append(elements[i + 1]);
                            return needToChange.ToString();
                        }
                        if (elements.ElementAtOrDefault(i - 2) != "-" && elements.ElementAtOrDefault(i + 1) == "-")
                        {
                            needToChange.Append(elements[i - 1]);
                            needToChange.Append(elements[i]);
                            needToChange.Append(elements[i + 1]);
                            needToChange.Append(elements[i + 2]);
                            return needToChange.ToString();
                        }

                        if (elements.ElementAtOrDefault(i - 2) == "-" && elements.ElementAtOrDefault(i + 1) == "-")
                        {
                            needToChange.Append(elements[i - 2]);
                            needToChange.Append(elements[i - 1]);
                            needToChange.Append(elements[i]);
                            needToChange.Append(elements[i + 1]);
                            needToChange.Append(elements[i + 2]);
                            return needToChange.ToString();
                        }
                    }
                }
            }
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Calculates 2 numbers and returns the result
        /// </summary>
        private static string CalculateTwoNumbers(string input)
        {
            var pattern = @"([-+\/*])|([^-+\/*]*)";
            Regex regex = new(pattern);

            List<string> elements = RemoveEmptyStrings(regex.Split(input));

            // need to simplify the code
            if (elements.Count == 4 && (elements[0] == "-" || elements[0] == "+"))
            {
                elements[0] += elements[1];
                elements[1] = elements[2];
                elements[2] = elements[3];
            }

            if (elements.Count == 4 && (elements[2] == "-" || elements[2] == "+"))
            {
                elements[2] += elements[3];
            }

            if (elements.Count == 5)
            {
                elements[0] += elements[1];
                elements[1] = elements[2];
                elements[2] = elements[3] + elements[4];
            }

            if (!double.TryParse(elements[0], out double elmFirst) &&
                !double.TryParse(elements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out elmFirst))
                throw new ArgumentException("Firsr number cant parse");

            if (!double.TryParse(elements[2], out double elmSecond) &&
                !double.TryParse(elements[2], NumberStyles.Any, CultureInfo.InvariantCulture, out elmSecond))
                throw new ArgumentException("Second number cant parse");

            StringBuilder result = new();

            switch (elements[1])
            {
                case "-":
                    result.Append((elmFirst - elmSecond).ToString());
                    return result.ToString();
                case "+":
                    result.Append((elmFirst + elmSecond).ToString());
                    return result.ToString();
                case "/":
                    if (elmFirst == 0 || elmSecond == 0)
                        throw new DivideByZeroException();
                    result.Append((elmFirst / elmSecond).ToString());
                    if (double.Parse(result.ToString()) > 0)
                        return result.Insert(0, "+").ToString();
                    return result.ToString();
                case "*":
                    result.Append((elmFirst * elmSecond).ToString());
                    if (double.Parse(result.ToString()) > 0)
                        return result.Insert(0, "+").ToString();
                    return result.ToString();
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Evaluates an expression without brackets and returns the result
        /// </summary>
        private static string CalculateExpression(string input)
        {
            string pattern = @"([-+\/*])|([^-+\/*]*)";
            Regex regex = new(pattern);
            List<string> elements = RemoveEmptyStrings(regex.Split(input));

            if (elements.Count <= 2)
                return input;

            input = ExpressionArithmeticClear(input);

            string needToChange = FindOperation(input);
            string res = CalculateTwoNumbers(needToChange);
            input = input.Replace(needToChange, res);

            return CalculateExpression(input);
        }

        /// <summary>
        /// Evaluates an expression with brackets and returns result 
        /// </summary>
        public static string Calculate(string input)
        {
            try
            {
                input = String.Join("", input.Split(' '));
                if (IsAnInvalidCharacter(input))
                    throw new ArgumentException("Can contain only \"0-9, -, +, *, /, (, ),'.',','\"");
                if (!IsAllBracketsClosedOutCountBrackets(input, out int numberBrackets))
                    throw new ArithmeticException("Not all brackets are closed");

                while (true)
                {
                    var countBrackets = 0;

                    if (numberBrackets == 0)
                        return CalculateExpression(input);

                    for (int i = 0; i < input.Length; i++)
                    {
                        if (input[i] == '(')
                            countBrackets++;

                        if (input[i] == '(' && countBrackets == numberBrackets)
                        {
                            StringBuilder expression = new();
                            for (int j = i + 1; input[j] != ')'; j++)
                            {
                                expression.Append(input[j]);
                            }
                            string expressionResult = CalculateExpression(expression.ToString());
                            expression.Insert(0, '(');
                            expression.Append(')');

                            input = input.Replace(expression.ToString(), expressionResult);
                            return Calculate(input);
                        }
                    }
                }
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
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
