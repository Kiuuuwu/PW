using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorNS;

namespace TestProject_PW
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Add_WithValidInput_ReturnsSum()
        {
            double number = 1.5;
            double number1 = 2;
            double expected = 3.5;
            Calculator calculator = new Calculator();

            double actual = calculator.Add(number, number1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Subtract_WithValidInput_ReturnsSubtraction()
        {
            double number = 1.5;
            double number1 = 2;
            double expected = -0.5;
            Calculator calculator = new Calculator();

            double actual = calculator.Subtract(number, number1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Multiply_WithValidInput_ReturnsMultiplication()
        {
            double number = 1.5;
            double number1 = 2;
            double expected = 3;
            Calculator calculator = new Calculator();

            double actual = calculator.Multiply(number, number1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Divide_WithValidInput_ReturnsDivision()
        {
            double number = 1.5;
            double number1 = 2;
            double expected = 0.75;
            Calculator calculator = new Calculator();

            double actual = calculator.Divide(number, number1);

            Assert.AreEqual(expected, actual);
        }
    }
}