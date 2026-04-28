using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Calculator;

public partial class MainWindow : Window
{
    private string currentInput = "";
    private double firstNumber = 0;
    private string currentOperator = "";
    private bool isNewInput = true;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Number_Click(object? sender, RoutedEventArgs e)
    {
        Button? button = sender as Button;
        string number = button?.Content?.ToString() ?? "";

        if (isNewInput)
        {
            currentInput = number;
            isNewInput = false;
        }
        else
        {
            currentInput += number;
        }

        DisplayBox.Text = currentInput;
    }

    private void Decimal_Click(object? sender, RoutedEventArgs e)
    {
        if (!currentInput.Contains(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator))
        {
            if (string.IsNullOrEmpty(currentInput) || isNewInput)
            {
                currentInput = "0" + CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
                isNewInput = false;
            }
            else
            {
                currentInput += CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator;
            }
            DisplayBox.Text = currentInput;
        }
    }

    private void Operator_Click(object? sender, RoutedEventArgs e)
    {
        Button? button = sender as Button;
        
        if (!string.IsNullOrEmpty(currentInput))
        {
            try
            {
                firstNumber = ParseNumber(currentInput);
            }
            catch
            {
                DisplayBox.Text = "Ошибка";
                ClearCalculator();
                return;
            }
        }
        
        currentOperator = button?.Content?.ToString() ?? "";
        isNewInput = true;
    }

    private void Equals_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(currentOperator) || string.IsNullOrEmpty(currentInput))
            return;

        try
        {
            double secondNumber = ParseNumber(currentInput);
            double result = 0;

            switch (currentOperator)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    if (secondNumber != 0)
                        result = firstNumber / secondNumber;
                    else
                    {
                        DisplayBox.Text = "Ошибка: деление на 0";
                        ClearCalculator();
                        return;
                    }
                    break;
                default:
                    DisplayBox.Text = "Ошибка";
                    ClearCalculator();
                    return;
            }

            string resultStr = FormatResult(result);
            DisplayBox.Text = resultStr;
            firstNumber = result;
            currentInput = resultStr;
            currentOperator = "";
            isNewInput = true;
        }
        catch (Exception ex)
        {
            DisplayBox.Text = "Ошибка";
            ClearCalculator();
        }
    }

    private void Clear_Click(object? sender, RoutedEventArgs e)
    {
        ClearCalculator();
        DisplayBox.Text = "0";
    }

    private void ClearCalculator()
    {
        currentInput = "";
        firstNumber = 0;
        currentOperator = "";
        isNewInput = true;
    }

    private double ParseNumber(string input)
    {
        if (string.IsNullOrEmpty(input))
            return 0;
        
        input = input.Replace(",", ".");
        
        return double.Parse(input, CultureInfo.InvariantCulture);
    }

    private string FormatResult(double number)
    {
        if (double.IsNaN(number) || double.IsInfinity(number))
            return "Ошибка";
        
        string result = number.ToString(CultureInfo.InvariantCulture);
        
        if (result.EndsWith(".0"))
            result = result.Substring(0, result.Length - 2);
        
        return result;
    }
}