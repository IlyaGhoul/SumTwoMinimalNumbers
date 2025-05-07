using System;

namespace SumTwoMinimalNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var testRunnerCheck = new TestRunnerCheck();

            testRunnerCheck.RunTestCheck(new TestNormalCase());

            testRunnerCheck.RunTestCheck(new TestOverflowCase());

            testRunnerCheck.RunTestCheck(new TestEmptyArrayCase());

            testRunnerCheck.RunTestCheck(new TestSingleElementArrayCase());

            testRunnerCheck.RunTestCheck(new TestNullArrayCase());
        }
    }

    public interface ITestCase
    {
        public string NameShowInfoTestCase { get; }
        public int[] InputArray { get; }
        public int ExpectedResult { get; }
        public Type ExpectedException { get; }
    }

    public class TestNormalCase : ITestCase
    {
        public string NameShowInfoTestCase => "Normal array case";

        public int[] InputArray => [-10, 1, 0, -5, 2, 523];

        public int ExpectedResult => -15; // -10 + (-5)

        public Type ExpectedException => null;
    }

    public class TestOverflowCase : ITestCase
    {
        public string NameShowInfoTestCase => "Overflow array case";

        public int[] InputArray => [int.MinValue, int.MinValue, 3, 19, 492, 1];

        public int ExpectedResult => 0;

        public Type ExpectedException => typeof(OverflowException);
    }

    public class TestEmptyArrayCase : ITestCase
    {
        public string NameShowInfoTestCase => "Empty array case";

        public int[] InputArray => [];

        public int ExpectedResult => 0;
                                                                                                                                                        
        public Type ExpectedException => typeof(ArgumentException);
    }

    public class TestSingleElementArrayCase : ITestCase
    {
        public string NameShowInfoTestCase => "Single element array case";

        public int[] InputArray => [8];

        public int ExpectedResult => 0;

        public Type ExpectedException => typeof(ArgumentException);
    }

    public class TestNullArrayCase : ITestCase
    {
        public string NameShowInfoTestCase => "Null array case";
        public int[] InputArray => null;
        public int ExpectedResult => 0;
        public Type ExpectedException => typeof(ArgumentNullException);
    }


    public class TestRunnerCheck
    {
        private CalculatingNumbers _calculatingNumbers = new CalculatingNumbers();

        public void RunTestCheck(ITestCase testCase)
        {
            Console.WriteLine($"\nTest: {testCase.NameShowInfoTestCase}");

            try
            {
                var result = _calculatingNumbers.SumOfTwoMinimalNumbers(testCase.InputArray);

                if (testCase.ExpectedException != null)
                {
                    Console.WriteLine($"Error: Expected exception {testCase.ExpectedException.Name} but no exception was thrown");
                    return;
                }

                if (result == testCase.ExpectedResult)
                {
                    Console.WriteLine("Success: " + result);
                }
                else
                {
                    Console.WriteLine($"Error: {testCase.ExpectedResult}, it worked out: {result}");
                }

            }
            catch (Exception ex) 
            {
                if (ex.GetType() == testCase.ExpectedException)
                {
                    Console.WriteLine($"Success! Expected exception: {ex.GetType().Name}");
                }
                else
                {
                    Console.WriteLine($"Error: Unexpected exception {ex.GetType().Name} (expected {testCase.ExpectedException.Name})");
                }

            }
           
        } 
    }

    public class CalculatingNumbers
    {
        public int SumOfTwoMinimalNumbers(int[] inputInts)
        {
            if (inputInts == null)
                throw new ArgumentNullException("The array of numbers is empty");
            else if (inputInts.Length < 2)
                throw new ArgumentException("The array of numbers must be greater than 1");

            int minOneNumber = int.MaxValue;
            int minTwoNumber = int.MaxValue;

            foreach (var number in inputInts)
            {
                if (number < minOneNumber)
                {
                    minTwoNumber = minOneNumber;
                    minOneNumber = number;
                }
                else if (number < minTwoNumber)
                {
                    minTwoNumber = number;
                }
            }

            checked
            {
                return minOneNumber + minTwoNumber;
            }

        }
    }
}
