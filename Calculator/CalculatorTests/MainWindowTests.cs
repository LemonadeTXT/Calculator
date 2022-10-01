using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        readonly MainWindow calculator = new MainWindow();

        [TestMethod()]
        public void IsCorrectStringToCalculate_EmptyString_False()
        {
            Assert.AreEqual(false, calculator.IsCorrectStringToCalculate(string.Empty));
        }

        [TestMethod()]
        public void IsCorrectStringToCalculate_OneDecimalPonit_False()
        {
            Assert.AreEqual(false, calculator.IsCorrectStringToCalculate("1."));
        }

        [TestMethod()]
        public void IsCorrectStringToCalculate_FivePlusSevenMinus_False()
        {
            Assert.AreEqual(false, calculator.IsCorrectStringToCalculate("5+7-"));
        }

        [TestMethod()]
        public void IsCorrectStringToCalculate_LongStringToCalculate_False()
        {
            Assert.AreEqual(false, calculator.IsCorrectStringToCalculate("96+2.39-78875/"));
        }

        [TestMethod()]
        public void IsCorrectStringToCalculate_LongStringToCalculate_True()
        {
            Assert.AreEqual(true, calculator.IsCorrectStringToCalculate("96+2.39-78875/2"));
        }

        [TestMethod()]
        public void IsZeroAfterOperation_StringIsOne_False()
        {
            Assert.AreEqual(false, calculator.IsZeroAfterOperation("1"));
        }

        [TestMethod()]
        public void IsZeroAfterOperation_SumOfNumbers_False()
        {
            Assert.AreEqual(false, calculator.IsZeroAfterOperation("45+99"));
        }

        [TestMethod()]
        public void IsZeroAfterOperation_LongStringToCalculate_True()
        {
            Assert.AreEqual(true, calculator.IsZeroAfterOperation("21/7+0"));
        }

        [TestMethod()]
        public void StringToCalculateIsMinusZero_CommonString_False()
        {
            Assert.AreEqual(false, calculator.StringToCalculateIsMinusZero("21/7+0"));
        }

        [TestMethod()]
        public void StringToCalculateIsMinusZero_MinusZero_True()
        {
            Assert.AreEqual(true, calculator.StringToCalculateIsMinusZero("-0"));
        }

        [TestMethod()]
        public void StringToCalculateIsZero_CommonString_False()
        {
            Assert.AreEqual(false, calculator.StringToCalculateIsZero("21/7+0"));
        }

        [TestMethod()]
        public void StringToCalculateIsZero_Zero_True()
        {
            Assert.AreEqual(true, calculator.StringToCalculateIsZero("0"));
        }
    }
}