using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class LineCalculator
    {
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

        private static bool IsAnInvalidCharacter(string line)
        {
            string pattern = @"[^0-9-+*\/()\n\t\r]";
            Regex regex = new(pattern);
            return regex.Matches(line).Any();
        }

        private static bool IsAllBracketsClosedIfTrueReturnCountBrackets(string line, out int countBrackets)
        {
            int unclosedBrackets = 0;
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
            }
            countBrackets = count;
            return unclosedBrackets == 0;
        }

        private static string FindCalculateOperation(string input)
        {
            string pattern = @"([-+\/*])|([^-+\/*]*)";
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

        public static double CalculateTwoNumbers(string input)
        {
            string pattern = @"([-+\/*])|([^-+\/*]*)";
            Regex regex = new(pattern);

            List<string> elements = RemoveEmptyStrings(regex.Split(input));

            // need to simplify the code
            if (elements.Count == 4 && elements[0] == "-")
            {
                elements[0] = elements[0] + elements[1];
                elements[1] = elements[2];
                elements[2] = elements[3];
            }

            if (elements.Count == 4 && elements[2] == "-")
            {
                elements[2] = elements[2] + elements[3];
            }

            if (elements.Count == 5)
            {
                elements[0] = elements[0] + elements[1];
                elements[1] = elements[2];
                elements[2] = elements[3] + elements[4];
            }

            if (!double.TryParse(elements[0], out double elmFirst) &&
                !double.TryParse(elements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out elmFirst))
                throw new ArgumentException("Firsr number cant parse");

            if (!double.TryParse(elements[2], out double elmSecond) &&
                !double.TryParse(elements[2], NumberStyles.Any, CultureInfo.InvariantCulture, out elmSecond))
                throw new ArgumentException("Second number cant parse");

            switch (elements[1])
            {
                case "-":
                    return elmFirst - elmSecond;
                case "+":
                    return elmFirst + elmSecond;
                case "/":
                    if (elmFirst == 0 || elmSecond == 0)
                        throw new DivideByZeroException();
                    return elmFirst / elmSecond;
                case "*":
                    return elmFirst * elmSecond;
                default:
                    throw new ArgumentException();
            }
        }

        public static string CalculateExpression(string input)
        {
            string pattern = @"([-+\/*])|([^-+\/*]*)";
            Regex regex = new(pattern);
            List<string> elements = RemoveEmptyStrings(regex.Split(input));

            if (elements.Count < 3)
                return input;

            string needToChange = FindCalculateOperation(input);
            double res = CalculateTwoNumbers(needToChange);
            input = input.Replace(needToChange, res.ToString());

            return CalculateExpression(input);
        }

        public static double Calculate(string input)
        {
            input = String.Join("", input.Split(' ')); // Remove spaces

            if (IsAnInvalidCharacter(input))
                throw new ArgumentException("Can contain only \"0-9, -, +, *, /, (, ), \\n, \\t, \\r \"");
            if (!IsAllBracketsClosedIfTrueReturnCountBrackets(input, out int numberBrackets))
                throw new ArithmeticException("Not all brackets are closed");

            while (true)
            {
                var countBrackets = 0;
                if (numberBrackets == 0)
                    return double.Parse(CalculateExpression(input));
                for (int i = 0; i < input.Length; i++) 
                {
                    if (input[i] == '(')
                        countBrackets++;

                    if (input[i] == '(' && countBrackets == numberBrackets)
                    {
                        StringBuilder expression = new();
                        for (int j = i+1; input[j] != ')'; j++)
                        {
                            expression.Append(input[j]);
                        }
                        string expressionResult = CalculateExpression(expression.ToString());
                        expression.Insert(0, "(");
                        expression.Append(")");

                        input = input.Replace(expression.ToString(), expressionResult);
                        return Calculate(input);
                    }
                }
            }
        }
    }
}
