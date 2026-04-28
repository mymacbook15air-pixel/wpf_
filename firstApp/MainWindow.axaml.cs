using System;
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
                    ResultBox.Text = c == 0 
                        ? "Уравнение имеет бесконечно много решений (0 = 0)"
                        : "Уравнение не имеет решений (противоречие)";
                }
                else
                {
                    double x = -c / b;
                    ResultBox.Text = $"Это линейное уравнение (a = 0)\n\nКорень: x = {FormatNumber(x)}";
                }
                return;
            }

            double d = b * b - 4 * a * c;
            string equation = $"{FormatNumber(a)}x²";
            if (b >= 0) equation += $" + {FormatNumber(b)}x";
            else equation += $" - {FormatNumber(Math.Abs(b))}x";
            if (c >= 0) equation += $" + {FormatNumber(c)}";
            else equation += $" - {FormatNumber(Math.Abs(c))}";
            equation += " = 0";

            if (d > 0)
            {
                double x1 = (-b + Math.Sqrt(d)) / (2 * a);
                double x2 = (-b - Math.Sqrt(d)) / (2 * a);
                ResultBox.Text = $"Уравнение: {equation}\n\nДискриминант: D = {FormatNumber(d)} > 0\n\n" +
                                 $"Два корня:\n   x₁ = {FormatNumber(x1)}\n   x₂ = {FormatNumber(x2)}";
            }
            else if (d == 0)
            {
                double x = -b / (2 * a);
                ResultBox.Text = $"Уравнение: {equation}\n\nДискриминант: D = 0\n\n" +
                                 $"Один корень: x = {FormatNumber(x)}";
            }
            else
            {
                double real = -b / (2 * a);
                double imag = Math.Sqrt(-d) / (2 * a);
                ResultBox.Text = $"Уравнение: {equation}\n\nДискриминант: D = {FormatNumber(d)} < 0\n\n" +
                                 $"Комплексные корни:\n   x₁ = {FormatNumber(real)} + {FormatNumber(imag)}i\n" +
                                 $"   x₂ = {FormatNumber(real)} - {FormatNumber(imag)}i";
            }
        }
        catch
        {
            ResultBox.Text = "Ошибка: введите корректные числа!";
        }
    }

    private string FormatNumber(double n)
    {
        if (double.IsNaN(n) || double.IsInfinity(n)) return "не определено";
        string s = Math.Round(n, 4).ToString();
        if (s.EndsWith(".0")) s = s.Substring(0, s.Length - 2);
        return s;
    }
}