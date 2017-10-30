using System;
using NUnit.Framework;
using StringCalculator;

namespace StringCalculatorTest
{
    [TestFixture]
    public class CalculatorTest
    {
        private Calculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new Calculator();
        }
       
        [TestCase("", "")]
        [TestCase("1", "1")]
        [TestCase("2", "2")]
        [TestCase("3", "3")]
        [TestCase("1,1", "2")]
        [TestCase("2,1", "3")]
        [TestCase("0,1", "1")]
        [TestCase("0,0", "0")]
        [TestCase("1,1", "2")]
        [TestCase("1,1,1", "3")]
        [TestCase("1,1,1,1", "4")]
        public void Sould_sum_numbers(string numbers, string output)
        {
            Assert.AreEqual(output, _calculator.Add(numbers));
        }

        [TestCase(";\n1;2", "3")]
        [TestCase("1,2\n3", "6")]
        [TestCase("[***]\n1***2***3", "6")]
        [TestCase("[kk]\n1kk2kk3", "6")]
        public void Sould_add_new_delimiter(string numbers, string output)
        {
            Assert.AreEqual(output, _calculator.Add(numbers));
        }
        
        [TestCase("[*][%]\n1*2%3", "6")]
        public void Sould_allow_multiple_delimiters(string numbers, string output)
        {
            Assert.AreEqual(output, _calculator.Add(numbers));
        }

        [TestCase("1001,2", "2")]
        [TestCase("1000,2", "1002")]
        public void Sould_not_calculete_numbers_greater_than_1000 (string numbers, string output)
        {
            Assert.AreEqual(output, _calculator.Add(numbers));
        }

        [TestCase("1,-1,1", "negatives not allowed: -1")]
        [TestCase("1,-1,-2", "negatives not allowed: -1, -2")]
        public void Should_return_exception_if_numbers_are_negative(string numbers, string output)
        {
            var exception = Assert.Throws<FormatException>(() => _calculator.Add(numbers));
            Assert.AreEqual(output, exception.Message);
        }

        [TestCase("0,a", "0")]
        [TestCase("b,a", "0")]
        [TestCase(",a", "0")]
        [TestCase("1,\n", "1")]
        [TestCase("1\n2,3", "6")]
        public void Should_return_exception_if_chars_in_array_are_not_numbers(string numbers, string output)
        {
            Assert.Throws<FormatException>(() => _calculator.Add(numbers));
        }
    }
}
