using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract_3_var_5
{
    // Класс OperatorPremium, наследуется от класса Operator
    public class OperatorPremium : Operator
    {
        public bool HasConnectionFee { get; set; }

        // Конструктор класса
        public OperatorPremium(string name, double pricePerMinute, double radius,
                             double speed, int count, bool hasFee)
            : base(name, pricePerMinute, radius, speed, count)
        {
            HasConnectionFee = hasFee;
        }

        //метод для расчета качества
        public override double CalculateQuality()
        {
            double baseQuality = base.CalculateQuality(); // Получаем базовое качество из базового класса
            return HasConnectionFee ? 0.7 * baseQuality : 1.5 * baseQuality; // Рассчитываем качество с учетом платы за соединение
        }

        // метод для получения информации об операторе
        public override string GetInfo()
        {
            return base.GetInfo() + $"\nПлата за соединение: {(HasConnectionFee ? "Да" : "Нет")}"; 
        }

        // метод для создания нового объекта OperatorPremium
        public static OperatorPremium AddOperator(string name, double pricePerMinute, double radius, double speed, int count, bool hasFee)
        {
            return new OperatorPremium(name, pricePerMinute, radius, speed, count, hasFee);
        }
        // Метод для отображения информации о лучшем операторе в MessageBox
        public void ShowBestOperatorInfo()
        {
            string message = $"Лучший оператор: {Operatorname}\n" +
                           $"Качество (Qp): {CalculateQuality():F2}\n" +
                           $"Плата за соединение: {(HasConnectionFee ? "Да" : "Нет")}\n"
                           ;

            MessageBox.Show(message, "Информация об операторе",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Метод для вывода статистики
        public static void ShowOperatorsStatistics(List<OperatorPremium> operators)
        {
            // Если список операторов пуст
            if (!operators.Any())
            {
                MessageBox.Show("Нет данных об операторах", "Статистика",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Формируем строку со статистикой
            string stats = $"Всего операторов: {operators.Count}\n" +
                         $"Среднее качество: {operators.Average(op => op.CalculateQuality()):F2}\n" +
                         $"Макс. качество: {operators.Max(op => op.CalculateQuality()):F2}\n" +
                         $"Мин. качество: {operators.Min(op => op.CalculateQuality()):F2}\n" +
                         $"С платой за соединение: {operators.Count(op => op.HasConnectionFee)}\n" +
                         $"Без платы: {operators.Count(op => !op.HasConnectionFee)}";

            MessageBox.Show(stats, "Статистика операторов",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
