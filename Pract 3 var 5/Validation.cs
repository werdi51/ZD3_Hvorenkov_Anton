using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_3_var_5
{
    // Класс для валидации входных данных
    public class Validation
    {
        // Метод для проверки корректности введенных данных
        public static string InputCorrect(string Name, double PricePerMinute, double radius, double ConnectSpeed, int AbonentsCount)
        {
            StringBuilder Error = new StringBuilder(); // Используем StringBuilder для формирования сообщения об ошибке

            // Проверяем каждое поле на корректность
            if (String.IsNullOrWhiteSpace(Name))
            {
                Error.AppendLine("Поле названия пустое"); // Добавляем сообщение об ошибке
            }

            if (PricePerMinute <= 0)
            {
                Error.AppendLine("Стоимость минуты не может быть меньше или равна 0"); // Добавляем сообщение об ошибке
            }
            else if (PricePerMinute >= 10000)
            {
                Error.AppendLine("Стоимость минуты не может быть больше 10000"); // Добавляем сообщение об ошибке
            }

            if (radius <= 0)
            {
                Error.AppendLine("Площадь охвата не может быть меньше или равна 0"); // Добавляем сообщение об ошибке
            }
            else if (radius >= 10000)
            {
                Error.AppendLine("Площадь охвата не может быть больше 10000"); // Добавляем сообщение об ошибке
            }

            if (ConnectSpeed <= 0)
            {
                Error.AppendLine("Скорость соединения не может быть меньше или равна 0"); // Добавляем сообщение об ошибке
            }
            else if (ConnectSpeed >= 10000)
            {
                Error.AppendLine("Скорость соединения не может быть больше 10000"); // Добавляем сообщение об ошибке
            }

            if (AbonentsCount <= 0)
            {
                Error.AppendLine("Количество абонентов не может быть меньше или равно 0"); // Добавляем сообщение об ошибке
            }
            else if (AbonentsCount > 10000000)
            {
                Error.AppendLine("Количество абонентов не может быть больше 10 000 000"); // Добавляем сообщение об ошибке
            }

            return Error.ToString(); // Возвращаем строку с сообщениями об ошибках
        }
    }
}
