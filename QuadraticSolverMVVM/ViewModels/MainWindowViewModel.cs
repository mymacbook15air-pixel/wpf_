using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QuadraticSolverMVVM.Models;

namespace QuadraticSolverMVVM.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string a = "1";
        private string b = "-3";
        private string c = "2";
        private string result = "Введите коэффициенты и нажмите Решить";
        private string details = "";

        public string A
        {
            get => a;
            set
            {
                a = value;
                OnPropertyChanged();
            }
        }

        public string B
        {
            get => b;
            set
            {
                b = value;
                OnPropertyChanged();
            }
        }

        public string C
        {
            get => c;
            set
            {
                c = value;
                OnPropertyChanged();
            }
        }

        public string Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        public string Details
        {
            get => details;
            set
            {
                details = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SolveCommand { get; }

        public MainWindowViewModel()
        {
            SolveCommand = new RelayCommand(Solve);
        }

        private void Solve()
        {
            try
            {
                double aVal = double.Parse(A);
                double bVal = double.Parse(B);
                double cVal = double.Parse(C);

                var equation = new QuadraticEquation(aVal, bVal, cVal);
                var (resultText, detailsText) = equation.Solve();

                Result = resultText;
                Details = detailsText;
            }
            catch (FormatException)
            {
                Result = "Ошибка: введите корректные числа";
                Details = "используйте цифры и точку";
            }
            catch (Exception ex)
            {
                Result = $"Ошибка: {ex.Message}";
                Details = "";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
