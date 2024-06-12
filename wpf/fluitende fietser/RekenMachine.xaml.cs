using System;
using System.Windows;
using System.Windows.Controls;

namespace fluitende_fietser
{
    /// <summary>
    /// Interaction logic for RekenMachine.xaml
    /// </summary>
    public partial class RekenMachine : Window
    {
        private string currentInput = "";
        private double currentValue = 0;
        private int currentOperator = ' ';

        public RekenMachine()
        {
            InitializeComponent();
        }

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                currentInput += button.Content.ToString();
                UpdateDisplay();
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (currentInput != "")
                {
                    currentValue = double.Parse(currentInput);
                    currentOperator = button.Content.ToString()[0];
                    currentInput = "";
                }
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput != "")
            {
                double secondValue = double.Parse(currentInput);
                switch (currentOperator)
                {
                    case '+':
                        currentValue += secondValue;
                        break;
                    case '-':
                        currentValue -= secondValue;
                        break;
                    case '*':
                        currentValue *= secondValue;
                        break;
                    case '/':
                        if (secondValue != 0)
                            currentValue /= secondValue;
                        else
                            MessageBox.Show("kan niet delen met 0");
                        break;
                }
                currentInput = currentValue.ToString();
                UpdateDisplay();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            currentValue = 0;
            currentOperator = ' ';
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            DisplayTextBox.Text = currentInput;
        }
    }
}
