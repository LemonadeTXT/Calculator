using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _lastElementIsDigit; //private явно укажи
        bool _lastOperationIsDecimalPoint; //private
        readonly List<char> _listOfOperations; //private

        public MainWindow()
        {
            _listOfOperations = new List<char>() { '+', '-', '*', '÷' };

            InitializeComponent();
        }

        private void ButtonZero_Click(object sender, RoutedEventArgs e)
        {
            var stringToCalculate = textDisplay.Content.ToString();

            if (textDisplay.Content.ToString().Length < 3) //магическое число(magic number) почему именно меньше 3(можно вынести константой в отдельную перменную) 
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
                textDisplay.Content += ",";

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
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            var stringToCalculate = textDisplay.Content.ToString();

            if (IsCorrectStringToCalculate(stringToCalculate))
            {
                stringToCalculate = stringToCalculate.Replace('÷', '/').Replace(',', '.');

                object result = new DataTable().Compute(stringToCalculate, null); //var

                if (result.ToString().Contains("."))
                {
                    _lastOperationIsDecimalPoint = true;
                }

                textDisplay.Content = result.ToString().Replace('.', ',');
            }
        }

        private void InputDigit(string digit)
        {
            var stringToCalculate = textDisplay.Content.ToString();

            if (stringToCalculate.Length < 3)//магическое число(magic number) почему именно меньше 3(можно вынести константой в отдельную перменную)
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
            if (stringToCalculate.Length > 2)//тоже магия, программист не должен думать, а почему тут именно 2, а дожен сразу понимать(например из названия константы)
            {
                var lastDigit = stringToCalculate[stringToCalculate.Length - 1]; //[^1]
                var preLastDigit = stringToCalculate[stringToCalculate.Length - 2]; //[^2]

                if (_listOfOperations.Contains(preLastDigit) && lastDigit is '0')
                {
                    return true;
                }
            }

            return false;
        }

        private void ReplaceLastZero(string stringToCalculate, string digit)
        {
            stringToCalculate = stringToCalculate.Remove(stringToCalculate.Length - 1); //можно сделать одной строкой remove + insert, а длину строки в отдельную переменную вынеси

            textDisplay.Content = stringToCalculate.Insert(stringToCalculate.Length, digit);
        }

        public bool IsCorrectStringToCalculate(string stringToCalculate)
        {
            if (string.IsNullOrEmpty(stringToCalculate))
            {
                MessageBox.Show("String to calculate is empty!");

                return false;
            }
            else if (!char.IsDigit(stringToCalculate[stringToCalculate.Length - 1])) //[^1]
            {
                MessageBox.Show("The calculation string was entered incorrectly (perhaps the last character is not a number)!");

                return false;
            }

            bool correctString = false; //перемести где используешь, стоит перед ифом хотя его скипаешь

            if (stringToCalculate[0] is '-')
            {
                stringToCalculate = stringToCalculate.Remove(0, 1);
            }

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