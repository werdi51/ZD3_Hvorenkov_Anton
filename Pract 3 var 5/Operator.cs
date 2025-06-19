using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_3_var_5
{
    // Класс Operator базовый класс
    public class Operator
    {
        // Свойства класса
        public string Operatorname { get; set; }
        public double MinutePrice { get; set; }
        public double Radius { get; set; }
        public double ConnectSpeed { get; set; }
        public int AbonentsCount { get; set; }

        // Добавляем свойство для качества 
        public double Quality => CalculateQuality();

        // Конструктор класса
        public Operator(string name, double pricePerMinute, double radius, double speed, int count)
        {
            Operatorname = name;
            MinutePrice = pricePerMinute;
            Radius = radius;
            ConnectSpeed = speed;
            AbonentsCount = count;
        }

        // Виртуальный метод для расчета качества
        public virtual double CalculateQuality()
        {
            return 100 * Radius / MinutePrice; // Формула Q
        }

        // Виртуальный метод для получения информации об операторе
        public virtual string GetInfo()
        {
            return $"Оператор: {Operatorname}\nКачество: {Quality:F2}";
        }
    }
}
