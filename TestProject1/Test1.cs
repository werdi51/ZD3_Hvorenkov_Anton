using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using Pract_3_var_5;

namespace TestProject1
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Pract_3_var_5;

    namespace OperatorTests
    {
        [TestClass]
        public class OperatorTests
        {
            [TestMethod]
            public void CalculateQuality()
            {
                // Создаем экземпляр класса Operator для тестирования
                var op = new Operator("Тест", 10, 100, 50, 1000);
                // Вычисляем ожидаемое значение качества вручную
                double expected = 100 * 100 / 10; // 1000

                // Вызываем метод CalculateQuality для получения фактического значения
                double actual = op.CalculateQuality();

                // Сравниваем ожидаемое и фактическое значения
                Assert.AreEqual(expected, actual);
            }
        }

        [TestClass]
        public class OperatorPremiumTests
        {
            [TestMethod]
            public void CalculateQualityTrue()
            {
                // Создаем экземпляр OperatorPremium с HasConnectionFee = true
                var op = new OperatorPremium("Тест", 10, 100, 50, 1000, true);
                // Вычисляем ожидаемое значение с учетом коэффициента 0.7
                double expected = 0.7 * (100 * 100 / 10); // 700

                // Вызываем метод CalculateQuality для получения фактического значения
                double actual = op.CalculateQuality();

                // Сравниваем ожидаемое и фактическое значения
                Assert.AreEqual(expected, actual);
            }

            [TestMethod]
            public void CalculateQualityFalse()
            {
                // Создаем экземпляр OperatorPremium с HasConnectionFee = false
                var op = new OperatorPremium("Тест", 10, 100, 50, 1000, false);
                // Вычисляем ожидаемое значение с учетом коэффициента 1.5
                double expected = 1.5 * (100 * 100 / 10); // 1500

                // Вызываем метод CalculateQuality для получения фактического значения
                double actual = op.CalculateQuality();

                // Сравниваем ожидаемое и фактическое значения
                Assert.AreEqual(expected, actual);
            }
        }

        [TestClass]
        public class ValidationTests
        {
            [TestMethod]
            public void InputCorrect()
            {
                // Задаем корректные значения для полей ввода
                string name = "МТС";
                double price = 2.5;
                double radius = 500;
                double speed = 100;
                int count = 10000;

                // Вызываем метод InputCorrect для проверки корректности введенных данных
                string result = Validation.InputCorrect(name, price, radius, speed, count);

                // Проверяем, что возвращаемое значение пусто, т.е. ошибок нет
                Assert.AreEqual("", result);
            }
        }
    }
}
