using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _lastElementIsDigit;
        private bool _lastOperationIsDecimalPoint;

        private readonly List<char> _listOfOperations;

        private const int MaxStringLengthIfZeroOrMinusZero = 3;

        public MainWindow()
        {
            _listOfOperations = new List<char>() { '+', '-', '*', '÷' };

            InitializeComponent();
        }

        private void ButtonZero_Click(object sender, RoutedEventArgs e)
        {
            var stringToCalculate = textDisplay.Content.ToString();

            if (stringToCalculate.Length < MaxStringLengthIfZeroOrMinusZero)
            {
                if (!(StringToCalculateIsZero(stringToCalculate) || StringToCalculateIsMinusZero(stringToCalculate)))
                {
                    textDisplay.Content += "0";

                    _lastElementIsDigit = true;
                }
            }
            else
            {
                if (!IsZeroAfterOperation(stringToCalculate))
                {
                    textDisplay.Content += "0";

                    _lastElementIsDigit = true;
                }
            }
        }

        private void ButtonOne_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("1");
        }

        private void ButtonTwo_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("2");
        }

        private void ButtonThree_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("3");
        }

        private void ButtonFour_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("4");
        }

        private void ButtonFive_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("5");
        }

        private void ButtonSix_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("6");
        }

        private void ButtonSeven_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("7");
        }

        private void ButtonEight_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("8");
        }

        private void ButtonNine_Click(object sender, RoutedEventArgs e)
        {
            InputDigit("9");
        }

        private void ButtonDecimalPoint_Click(object sender, RoutedEventArgs e)
        {
            if (_lastElementIsDigit && !_lastOperationIsDecimalPoint)
            {
                textDisplay.Content += ".";

                _lastElementIsDigit = false;
                _lastOperationIsDecimalPoint = true;
            }
        }

        private void ButtonDivide_Click(object sender, RoutedEventArgs e)
        {
            InputOperation("÷");
        }

        private void ButtonMultiply_Click(object sender, RoutedEventArgs e)
        {
            InputOperation("*");
        }

        private void ButtonSubtract_Click(object sender, RoutedEventArgs e)
        {
            if (textDisplay.Content.ToString() == string.Empty)
            {
                textDisplay.Content = "-";

                _lastElementIsDigit = false;
                _lastOperationIsDecimalPoint = false;
            }
            else
            {
                InputOperation("-");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            InputOperation("+");
        }

        private void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            textDisplay.Content = string.Empty;

            _lastElementIsDigit = false;
            _lastOperationIsDecimalPoint = false;
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            var stringToCalculate = textDisplay.Content.ToString();

            if (IsCorrectStringToCalculate(stringToCalculate))
            {
                stringToCalculate = stringToCalculate.Replace('÷', '/');

                var result = new DataTable().Compute(stringToCalculate, null);

                var resultToString = Math.Round(Convert.ToDouble(result), 4).ToString().Replace(',', '.');

                if (resultToString.Contains("."))
                {
                    _lastOperationIsDecimalPoint = true;
                }

                textDisplay.Content = resultToString;
            }
        }

        private void InputDigit(string digit)
        {
            var stringToCalculate = textDisplay.Content.ToString();

            if (stringToCalculate.Length < MaxStringLengthIfZeroOrMinusZero)
            {
                if (StringToCalculateIsZero(stringToCalculate))
                {
                    textDisplay.Content = digit;
                }
                else if (StringToCalculateIsMinusZero(stringToCalculate))
                {
                    textDisplay.Content = "-" + digit;
                }
                else
                {
                    textDisplay.Content += digit;
                }
            }
            else if (IsZeroAfterOperation(stringToCalculate))
            {
                ReplaceLastZero(stringToCalculate, digit);
            }
            else
            {
                textDisplay.Content += digit;
            }

            _lastElementIsDigit = true;
        }

        private void InputOperation(string operation)
        {
            if (_lastElementIsDigit)
            {
                textDisplay.Content += operation;

                _lastElementIsDigit = false;
                _lastOperationIsDecimalPoint = false;
            }
            else if (!string.IsNullOrEmpty(textDisplay.Content.ToString()))
            {
                var stringToCalculate = textDisplay.Content.ToString();

                var lastSymbol = stringToCalculate[stringToCalculate.Length - 1];

                if (stringToCalculate.Length > 1 && _listOfOperations.Contains(lastSymbol))
                {
                    ReplaceLastOperation(stringToCalculate, operation);
                }
            }
        }

        public bool StringToCalculateIsZero(string stringToCalculate)
        {
            if (stringToCalculate is "0")
            {
                return true;
            }

            return false;
        }

        public bool StringToCalculateIsMinusZero(string stringToCalculate)
        {
            if (stringToCalculate is "-0")
            {
                return true;
            }

            return false;
        }

        public bool IsZeroAfterOperation(string stringToCalculate)
        {
            if (stringToCalculate.Length > 2)
            {
                var lastDigit = stringToCalculate[stringToCalculate.Length - 1];
                var preLastDigit = stringToCalculate[stringToCalculate.Length - 2];

                if (_listOfOperations.Contains(preLastDigit) && lastDigit is '0')
                {
                    return true;
                }
            }

            return false;
        }

        private void ReplaceLastZero(string stringToCalculate, string digit)
        {
            var stringToCalculateLength = stringToCalculate.Length;

            textDisplay.Content = stringToCalculate.Remove(stringToCalculateLength - 1).
                Insert(stringToCalculateLength - 1, digit);
        }

        private void ReplaceLastOperation(string stringToCalculate, string operation)
        {
            var stringToCalculateLength = stringToCalculate.Length;

            textDisplay.Content = stringToCalculate.Remove(stringToCalculateLength - 1).
                Insert(stringToCalculateLength - 1, operation);
        }

        public bool IsCorrectStringToCalculate(string stringToCalculate)
        {
            if (string.IsNullOrEmpty(stringToCalculate))
            {
                MessageBox.Show("String to calculate is empty!");

                return false;
            }
            else if (!char.IsDigit(stringToCalculate[stringToCalculate.Length - 1]))
            {
                MessageBox.Show("The calculation string was entered incorrectly (perhaps the last character is not a number)!");

                return false;
            }

            if (stringToCalculate[0] is '-')
            {
                stringToCalculate = stringToCalculate.Remove(0, 1);
            }

            bool correctString = false;

            foreach (var item in _listOfOperations)
            {
                if (stringToCalculate.Contains(item))
                {
                    correctString = true;
                    break;
                }
            }

            if (!correctString)
            {
                MessageBox.Show("Please do some operatons!");

                return false;
            }

            return true;
        }
    }
}