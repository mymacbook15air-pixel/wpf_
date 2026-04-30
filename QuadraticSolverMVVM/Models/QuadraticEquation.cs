using System;

namespace QuadraticSolverMVVM.Models
{
    public class QuadraticEquation
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public QuadraticEquation(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public (string result, string details) Solve()
        {
            if (A == 0)
            {
                return SolveLinear();
            }

            double discriminant = B * B - 4 * A * C;

            if (discriminant > 0)
            {
                double x1 = (-B + Math.Sqrt(discriminant)) / (2 * A);
                double x2 = (-B - Math.Sqrt(discriminant)) / (2 * A);
                return ($"x1 = {FormatNumber(x1)}, x2 = {FormatNumber(x2)}", 
                        $"D = {FormatNumber(discriminant)} > 0");
            }
            else if (discriminant == 0)
            {
                double x = -B / (2 * A);
                return ($"x = {FormatNumber(x)}", 
                        $"D = 0");
            }
            else
            {
                double realPart = -B / (2 * A);
                double imaginaryPart = Math.Sqrt(-discriminant) / (2 * A);
                return ($"x1 = {FormatNumber(realPart)} + {FormatNumber(imaginaryPart)}i, x2 = {FormatNumber(realPart)} - {FormatNumber(imaginaryPart)}i",
                        $"D = {FormatNumber(discriminant)} < 0");
            }
        }

        private (string result, string details) SolveLinear()
        {
            if (B == 0)
            {
                if (C == 0)
                    return ("Бесконечно много решений", "0 = 0");
                else
                    return ("Нет решений", "противоречие");
            }
            else
            {
                double x = -C / B;
                return ($"x = {FormatNumber(x)}", "линейное уравнение (a = 0)");
            }
        }

        private string FormatNumber(double n)
        {
            if (double.IsNaN(n) || double.IsInfinity(n))
                return "не определено";
            string s = Math.Round(n, 4).ToString();
            if (s.EndsWith(".0"))
                s = s.Substring(0, s.Length - 2);
            return s;
        }
    }
}