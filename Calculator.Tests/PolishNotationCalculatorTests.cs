namespace Calculator.Tests
{
    [TestClass]
    public class PolishNotationCalculatorTests
    {
        [TestMethod]
        public void Calculate_WithValidInput1_ShouldReturnExpectedValue()
        {
            var input = "-(25-1*(120*2-26*7*12/(14+24-845-445/45)*5/(4-9)+2)-457)+2/2-(3^2)^2";
            var expected = 591.3264417845485;

            var actual = PolishNotationCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithValidInput2_ShouldReturnExpectedValue()
        {
            var input = "( -3) * ( 200*1.5 + 400-(50+230) - (30) * (445/50*30))";
            var expected = 22770;

            var actual = PolishNotationCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithValidInput3_ShouldReturnExpectedValue()
        {
            var input = "1+2*(3+2)";
            var expected = 11;

            var actual = PolishNotationCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithValidInput4_ShouldReturnExpectedValue()
        {
            var input = "2+15/3+4*2";
            var expected = 15;

            var actual = PolishNotationCalculator.Calculate(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_WithIncorrectInput_ShouldReturnStringArgumentException()
        {
            var input = "1+x+4";

            Assert.ThrowsException<ArgumentException>(() => PolishNotationCalculator.Calculate(input));
        }

        [TestMethod]
        public void Calculate_WithCorrectInputButDivisionByZero_ShouldReturnStringDivideByZeroException()
        {
            var input = "4/0";

            Assert.ThrowsException<DivideByZeroException>(() => PolishNotationCalculator.Calculate(input));
        }

        [TestMethod]
        public void Calculate_InputWithNonRepeatingBrackets_ShouldReturnStringArithmeticException()
        {
            var input = "10+(3-3))";
            
            Assert.ThrowsException<ArithmeticException>(() => PolishNotationCalculator.Calculate(input));
        }

        [TestMethod]
        public void Calculate_InputWithWrongSequenceOfOperators_ShouldReturnStringUnexpectedException()
        {
            var input0 = "3++3";
            var input1 = "3//3";
            var input2 = "3**3";
            var input3 = "3+/3";
            var input4 = "3/*3";

            Assert.ThrowsException<InvalidOperationException>(() => PolishNotationCalculator.Calculate(input0));
            Assert.ThrowsException<InvalidOperationException>(() => PolishNotationCalculator.Calculate(input1));
            Assert.ThrowsException<InvalidOperationException>(() => PolishNotationCalculator.Calculate(input2));
            Assert.ThrowsException<InvalidOperationException>(() => PolishNotationCalculator.Calculate(input3));
            Assert.ThrowsException<InvalidOperationException>(() => PolishNotationCalculator.Calculate(input4));
        }
    }
}
