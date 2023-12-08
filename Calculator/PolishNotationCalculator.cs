using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class PolishNotationCalculator
    {
        /// <summary>
        /// Get a regular expression without ""
        /// </summary>
        private static string[] RegaxClearStings(string expression, string pattern)
        {
            Regex regex = new(pattern);
            string[] elements = regex.Split(expression);
            List<string> cleanStrings = new();

            for (int i = 0; i < elements.Length; i++)
            {
                if (!elements[i].Equals(""))
                    cleanStrings.Add(elements[i]);
            }

            return cleanStrings.ToArray();
        }

        /// <summary>
        /// Checks for available characters
        /// </summary>
        private static bool IsAnInvalidCharacter(string line)
        {
            var pattern = @"[^0-9-+*\/(),.\^]";
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

        /// <summary>
        /// Needed to calculate where the unary minus is
        /// </summary>
        private static bool IsСlosedBracketOrDefault(char c)
        {
            if (c == '(' || c == default)
                return true;
            return false;
        }

        /// <summary>
        /// Check whether it is an operator or a parenthesis
        /// </summary>
        private static bool IsOperatorOrDefault(string c)
        {
            if (c == "^"
                || c == "~"
                || c == "*"
                || c == "/"
                || c == "+"
                || c == "-"
                || c == "("
                || c == ")"
                || c == default)
                return true;
            return false;
        }

        /// <summary>
        /// Get the priority of the operator, if it is not the operator, then the priority is minimal
        /// </summary>
        private static int GetOperatorPriority(string chr)
        {
            switch (chr)
            {
                case "^":
                    return 3;
                case "~":
                    return 2;
                case "*":
                case "/":
                    return 1;
                case "+":
                case "-":
                    return 0;
                default:
                    return int.MinValue;
            }
        }

        /// <summary>
        /// Returns an expression with unary minuses
        /// </summary>
        private static string GetExpressionWithUnaryMinuses(string input)
        {
            StringBuilder output = new();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '-' && IsСlosedBracketOrDefault(input.ElementAtOrDefault(i - 1)))
                    output.Append('~');
                else
                    output.Append(input[i]);
            }

            return output.ToString();
        }


        /// <summary>
        /// Convert expression to Polish notation
        /// </summary>
        private static Stack<string> MakePolishNotation(string[] expression)
        {
            Stack<string> notatiom = new();
            Stack<string> operators = new();

            for (int i = 0; i < expression.Length; i++)
            {
                // if it is not the operator (numbers)
                if (!IsOperatorOrDefault(expression[i]))
                    notatiom.Push(expression[i]);
                // in the else operators come in
                else
                {
                    // if it is an open bracket, then push it to the operators
                    if (expression[i] == "(")
                        operators.Push(expression[i]);
                    // if it is a closed bracket, then we push operators into the notation until we encounter an opening bracketесли дужка обратная выкудувать до дужки обычной 
                    else if (expression[i] == ")")
                    {
                        while (operators.Count > 0)
                        {
                            if (operators.TryPeek(out string res) && res == "(")
                            {
                                operators.Pop();
                                break;
                            }
                            notatiom.Push(operators.Pop());
                        }
                    }
                    // if these are operators, not brackets
                    else if (IsOperatorOrDefault(expression[i]))
                    {
                        bool isPeek = operators.TryPeek(out string op);
                        // if the operator stack is empty or the last operator has a lower priority
                        if (isPeek == false | GetOperatorPriority(op) < GetOperatorPriority(expression[i]))
                        {
                            operators.Push(expression[i]);
                        }
                        else
                        {
                            // drop into the notation operators that have a priority >= our current operator
                            do
                            {
                                notatiom.Push(operators.Pop());
                                operators.TryPeek(out op);
                            }
                            while (GetOperatorPriority(op) >= GetOperatorPriority(expression[i]) && operators.Count > 0);
                            operators.Push(expression[i]);
                        }
                    }
                }
            }
            // drop the remaining operators into the note
            int countOperators = operators.Count;
            for (int i = 0; i < countOperators; i++)
            {
                notatiom.Push(operators.Pop());
            }

            // flip the stack
            int countNotation = notatiom.Count;
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < countNotation; i++)
            {
                if (notatiom.Peek() != "(")
                    stack.Push(notatiom.Pop());
                else
                    notatiom.Pop();
            }

            return stack;
        }

        /// <summary>
        /// Calculate Polish notation from the stack
        /// </summary>
        private static double CalculatePolishNotation(Stack<string> notation)
        {
            int lenNotation = notation.Count;
            Stack<double> nums = new();

            for (int i = 0; i < lenNotation; i++)
            {
                if (!IsOperatorOrDefault(notation.Peek()))
                {
                    string tryParse = notation.Pop();
                    if (double.TryParse(tryParse, out double result) ||
                        double.TryParse(tryParse, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                        nums.Push(result);
                    else
                        throw new FormatException("Invalid number format");
                }
                else
                {
                    string oprt = notation.Pop();
                    double firstNum;
                    double secondNum;
                    switch (oprt)
                    {
                        case "^":
                            firstNum = nums.Pop();
                            secondNum = nums.Pop();
                            nums.Push(Math.Pow(secondNum, firstNum));
                            break;
                        case "~":
                            firstNum = nums.Pop();
                            nums.Push(-firstNum);
                            break;
                        case "*":
                            firstNum = nums.Pop();
                            secondNum = nums.Pop();
                            nums.Push(secondNum * firstNum);
                            break;
                        case "/":
                            firstNum = nums.Pop();
                            secondNum = nums.Pop();
                            if (firstNum == 0 || secondNum == 0)
                                throw new DivideByZeroException();
                            nums.Push(secondNum / firstNum);
                            break;
                        case "+":
                            firstNum = nums.Pop();
                            secondNum = nums.Pop();
                            nums.Push(secondNum + firstNum);
                            break;
                        case "-":
                            firstNum = nums.Pop();
                            secondNum = nums.Pop();
                            nums.Push(secondNum - firstNum);
                            break;
                        default:
                            throw new ArgumentException("Unknown operation");

                    }
                }
            }

            return nums.Pop();
        }

        /// <summary>
        /// Calculate the expression
        /// </summary>
        public static double Calculate(string expression)
        {
            // Remove spaces
            expression = String.Join("", expression.Split(' '));

            if (IsAnInvalidCharacter(expression))
                throw new ArgumentException("Can contain only \"0-9, -, +, *, /, (, ),'.',','\"");
            if (!IsAllBracketsClosedOutCountBrackets(expression, out int numberBrackets))
                throw new ArithmeticException("Not all brackets are closed");

            // Unary minuses are converted to ~
            expression = GetExpressionWithUnaryMinuses(expression);
            string[] elements = RegaxClearStings(expression, @"([- + \/ * \^ \( \ \~)])|([^- + \/ * \^ \( \) \~]*)");

            Stack<string> staclPol = MakePolishNotation(elements);

            return CalculatePolishNotation(staclPol);
        }
    }
}
