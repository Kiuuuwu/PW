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

        public double Subtract(double number, double number1)
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
            Console.WriteLine("Please specify two numbers you would like to:\nadd, subtract, multiply or divide:" +
                "\nPlease type in the first number");
            double number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please type in the second number");
            double number1 = Convert.ToInt32(Console.ReadLine());
            Calculator calculator = new Calculator();

            Console.WriteLine("add(" + number + ", " + number1 + "): " + calculator.Add(number, number1));
            Console.WriteLine("subtract(" + number + ", " + number1 + "): " + calculator.Subtract(number, number1));
            Console.WriteLine("multiply(" + number + ", " + number1 + "): " + calculator.Multiply(number, number1));
            Console.WriteLine("divide(" + number + ", " + number1 + "): " + calculator.Divide(number, number1));
        }
    }
}