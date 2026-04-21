using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace firstApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void Solve_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            double a = double.Parse(CoeffA.Text ?? "0");
            double b = double.Parse(CoeffB.Text ?? "0");
            double c = double.Parse(CoeffC.Text ?? "0");

            if (a == 0)
            {
                if (b == 0)
                {
                    if (c == 0)
                        ResultBox.Text = "Уравнение имеет бесконечно много решений (0 = 0)";
                    else
                        ResultBox.Text = "Уравнение не имеет решений (противоречие)";
                }
                else
                {
                    double x = -c / b;
                    ResultBox.Text = $"Это линейное уравнение (a = 0)\n\nКорень: x = {FormatNumber(x)}";
                }
                return;
            }

            double discriminant = b * b - 4 * a * c;

            string equation = $"{FormatNumber(a)}x²";
            if (b >= 0) equation += $" + {FormatNumber(b)}x";
            else equation += $" - {FormatNumber(Math.Abs(b))}x";
            
            if (c >= 0) equation += $" + {FormatNumber(c)}";
            else equation += $" - {FormatNumber(Math.Abs(c))}";
            
            equation += " = 0";

            if (discriminant > 0)
            {
                double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                
                ResultBox.Text = $"Уравнение: {equation}\n\n" +
                                 $"Дискриминант: D = {FormatNumber(discriminant)} > 0\n\n" +
                                 $"Два действительных корня:\n" +
                                 $"   x₁ = {FormatNumber(x1)}\n" +
                                 $"   x₂ = {FormatNumber(x2)}\n\n" +
                                 $"x₁ + x₂ = {FormatNumber(x1 + x2)} (сумма = {FormatNumber(-b / a)})\n" +
                                 $"x₁ · x₂ = {FormatNumber(x1 * x2)} (произведение = {FormatNumber(c / a)})";
            }
            else if (discriminant == 0)
            {
                double x = -b / (2 * a);
                
                ResultBox.Text = $"Уравнение: {equation}\n\n" +
                                 $"Дискриминант: D = 0\n\n" +
                                 $"Один корень (два совпадающих):\n" +
                                 $"   x = {FormatNumber(x)}\n\n" +
                                 $"Проверка: {a}·({FormatNumber(x)})² + {b}·({FormatNumber(x)}) + {c} = 0";
            }
            else
            {
                double realPart = -b / (2 * a);
                double imaginaryPart = Math.Sqrt(-discriminant) / (2 * a);
                
                ResultBox.Text = $"Уравнение: {equation}\n\n" +
                                 $"Дискриминант: D = {FormatNumber(discriminant)} < 0\n\n" +
                                 $"Комплексные корни:\n" +
                                 $"   x₁ = {FormatNumber(realPart)} + {FormatNumber(imaginaryPart)}i\n" +
                                 $"   x₂ = {FormatNumber(realPart)} - {FormatNumber(imaginaryPart)}i\n\n" +
                                 $"Дискриминант отрицательный — корни комплексные.";
            }
        }
        catch (FormatException)
        {
            ResultBox.Text = "Ошибка: Введите корректные числа!\n\n" +
                             "Используйте цифры и точку (например: 2.5, -3, 0.75)";
        }
        catch (Exception ex)
        {
            ResultBox.Text = $"Непредвиденная ошибка:\n{ex.Message}";
        }
    }

    private string FormatNumber(double number)
    {
        if (double.IsNaN(number) || double.IsInfinity(number))
            return "не определено";
        
        string formatted = Math.Round(number, 4).ToString();
        
        if (formatted.EndsWith(".0"))
            formatted = formatted.Substring(0, formatted.Length - 2);
        
        if (Math.Abs(number) < 0.0001 && number != 0)
            return number.ToString("E3");
        
        return formatted;
    }
}