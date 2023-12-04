namespace Calculator.Tests
{
    [TestClass]
    public class ExpressionCalculatorTests
    {
        [TestMethod]
        public void Calculate_WithValidInput_ShouldReturnExpectedValue()
        {
            var input = "( -3) * ( 200*1.5 + 400-(50+230) - (30) * (445/50*30))";
            var expected = "22770";

            var actual = ExpressionCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithValidInput2_ShouldReturnExpectedValue()
        {
            var input = "1+2*(3+2)";
            var expected = "11";

            var actual = ExpressionCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithValidInput3_ShouldReturnExpectedValue()
        {
            var input = "2+15/3+4*2";
            var expected = "15";

            var actual = ExpressionCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithIncorrectInput_ShouldReturnStringArgumentException()
        {
            var input = "1+x+4";
            var expected = "Exception. Incorrect input format.";

            var actual = ExpressionCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithCorrectInputButDivisionByZero_ShouldReturnStringDivideByZeroException()
        {
            var input = "4/0";
            var expected = "Exception. Not divisible by 0.";

            var actual = ExpressionCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_InputWithNonRepeatingBrackets_ShouldReturnStringArithmeticException()
        {
            var input = "10+(3-3))";
            var expected = "Exception. Not all brackets are closed.";

            var actual = ExpressionCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
