namespace Calculator.Tests
{
    [TestClass]
    public class PolishNotationCalculatorTests
    {
        [TestMethod]
        public void Calculate_WithValidInput4_ShouldReturnExpectedValue()
        {
            var input = "-(25-1*(120*2-26*7*12/(14+24-845-445/45)*5/(4-9)+2)-457)+2/2-(3^2)^2";
            var expected = "591,3264417845485";

            var actual = PolishNotationCalculatorTools.CalculateString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithValidInput_ShouldReturnExpectedValue()
        {
            var input = "( -3) * ( 200*1.5 + 400-(50+230) - (30) * (445/50*30))";
            var expected = "22770";

            var actual = PolishNotationCalculatorTools.CalculateString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithValidInput2_ShouldReturnExpectedValue()
        {
            var input = "1+2*(3+2)";
            var expected = "11";

            var actual = PolishNotationCalculatorTools.CalculateString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithValidInput3_ShouldReturnExpectedValue()
        {
            var input = "2+15/3+4*2";
            var expected = "15";

            var actual = PolishNotationCalculatorTools.CalculateString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithIncorrectInput_ShouldReturnStringArgumentException()
        {
            var input = "1+x+4";
            var expected = "Exception. Incorrect input format.";

            var actual = PolishNotationCalculatorTools.CalculateString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithCorrectInputButDivisionByZero_ShouldReturnStringDivideByZeroException()
        {
            var input = "4/0";
            var expected = "Exception. Not divisible by 0.";

            var actual = PolishNotationCalculatorTools.CalculateString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_InputWithNonRepeatingBrackets_ShouldReturnStringArithmeticException()
        {
            var input = "10+(3-3))";
            var expected = "Exception. Not all brackets are closed.";

            var actual = PolishNotationCalculatorTools.CalculateString(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
