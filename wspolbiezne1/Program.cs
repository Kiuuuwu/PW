using System;

namespace CalculatorNS
{
    public class Calculator
    {

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
            Console.WriteLine("Please specify two numbers you would like to add, subtract, multiply or divide:" +
                "\nPlease type in the first number");

            double number = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please type in the second number");
            double number1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Please specify operation:\n[1] add\n[2] subtract\n[3] multiply\n[4] divide:");
            string? operationNumber = Console.ReadLine();
            Calculator calculator = new Calculator();

            switch (operationNumber)
            {
                case "1":
                    Console.WriteLine(number + " + " + number1 + " = " + calculator.Add(number, number1));
                    break;
                case "2":
                    Console.WriteLine(number + " - " + number1 + " = " + calculator.Subtract(number, number1));
                    break;
                case "3":
                    Console.WriteLine(number + " * " + number1 + " = " + calculator.Multiply(number, number1));
                    break;
                case "4":
                    Console.WriteLine(number + " / " + number1 + " = " + calculator.Divide(number, number1));
                    break;
            }  
        }
    }
}