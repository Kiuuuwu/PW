using System;

namespace CalculatorNS
{
    public class Calculator
    {
        private double c_number;
        private double c_number1;

        public Calculator() {}

        public double Add(double number, double number1)
        {
            return number + number1;
        }

        public double Substract(double number, double number1)
        {
            return number - number1;
        }

        public double Multiply(double number, double number1)
        {
            return number * number1;
        }

        public double Divide(double number, double number1)
        {
            return number / number1;
        }

        public static void Main()
        {
            double number = 2;
            double number1 = 7;
            Calculator calculator = new Calculator();

            Console.WriteLine("add(" + number + ", " + number1 + "): " + calculator.Add(number, number1));
            Console.WriteLine("substract(" + number + ", " + number1 + "): " + calculator.Substract(number, number1));
            Console.WriteLine("multiply(" + number + ", " + number1 + "): " + calculator.Multiply(number, number1));
            Console.WriteLine("divide(" + number + ", " + number1 + "): " + calculator.Divide(number, number1));
        }
    }
}