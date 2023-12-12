using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class PolishNotationCalculator
    {
        /// <summary>
        /// Checks for available characters
        /// </summary>
        private static bool IsAnInvalidCharacter(string line)
        {
            var regex = new Regex(@"[^0-9-+*\/(),.\^]");
            return regex.IsMatch(line);
        }

        /// <summary>
        /// Returns a bool value, depending on whether the brackets are in the correct order, and also returns their number in out
        /// </summary>
        private static bool IsAllBracketsClosedOutCountBrackets(string line)
        {
            var unclosedBrackets = 0;

            foreach (char c in line)
            {
                if (c == '(')
                    unclosedBrackets++;
                if (c == ')')
                    unclosedBrackets--;
                if (unclosedBrackets < 0)
                    throw new ArithmeticException();
            }
            return unclosedBrackets == 0;
        }

        /// <summary>
        /// Check whether it is an operator or a parenthesis
        /// </summary>
        private static bool IsOperator(string c)
        {
            if (c == "^"
                || c == "~"
                || c == "*"
                || c == "/"
                || c == "+"
                || c == "-"
                || c == "("
                || c == ")")
                return true;
            return false;
        }

        /// <summary>
        /// Get the priority of the operator, if it is not the operator, then the priority is minimal
        /// </summary>
        private static int GetOperatorPriority(string chr)
        {
            return chr switch
            {
                "^" => 3,
                "~" => 2,
                "*" => 1,
                "/" => 1,
                "+" => 0,
                "-" => 0,
                _ => int.MinValue,
            };
        }

        /// <summary>
        /// Returns an expression with unary minuses
        /// </summary>
        private static string GetExpressionWithUnaryMinuses(string input)
        {
            StringBuilder output = new();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '-' && (input.ElementAtOrDefault(i - 1) == '(' || input.ElementAtOrDefault(i - 1) == default) )
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
                if (!IsOperator(expression[i]))
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
                            if (operators.TryPeek(out string? res) && res == "(")
                            {
                                operators.Pop();
                                break;
                            }
                            notatiom.Push(operators.Pop());
                        }
                    }
                    // if these are operators, not brackets
                    else if (IsOperator(expression[i]))
                    {
                        bool isPeek = operators.TryPeek(out string? op);

                        // if the operator stack is empty or the last operator has a lower priority
                        if (isPeek == false | GetOperatorPriority(op!) < GetOperatorPriority(expression[i]))
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
                            while (GetOperatorPriority(op!) >= GetOperatorPriority(expression[i]) && operators.Count > 0);
                            operators.Push(expression[i]);
                        }
                    }
                }
            }
            // drop the remaining operators into the note
            while(operators.Count != 0)
            {
                notatiom.Push(operators.Pop());
            }

            // flip the stack
            Stack<string> stack = new();
            while (notatiom.Count != 0)
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
                if (!IsOperator(notation.Peek()))
                {
                    string tryParse = notation.Pop();
                    if (double.TryParse(tryParse, NumberStyles.Any, CultureInfo.CurrentCulture, out double result) ||
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
            expression = expression.Replace(" ", "");

            if (IsAnInvalidCharacter(expression))
                throw new ArgumentException("Can contain only \"0-9, -, +, *, /, (, ),'.',','\"");
            if (!IsAllBracketsClosedOutCountBrackets(expression))
                throw new ArithmeticException("Not all brackets are closed");         

            // Unary minuses are converted to ~
            expression = GetExpressionWithUnaryMinuses(expression);

            var regex = new Regex(@"([- + \/ * \^ \( \ \~)])|([^- + \/ * \^ \( \) \~]*)");

            string[] elements = regex.Split(expression).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Stack<string> staclPol = MakePolishNotation(elements);

            return CalculatePolishNotation(staclPol);
        }
    }
}
