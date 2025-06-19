using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract_3_var_5
{
    public partial class Form1 : Form
    {
        // Список для хранения объектов OperatorPremium
        private List<OperatorPremium> OPList = new List<OperatorPremium>();
        // Словарь для быстрого поиска операторов по имени
        private Dictionary<string, OperatorPremium> OPDict = new Dictionary<string, OperatorPremium>();

        // Имя файла
        string OperatorsFile = "Operators.txt";

        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы
            dataGridView1.AutoGenerateColumns = false; // Отключаем автоматическую генерацию колонок
            InitializeDataGridViewColumns(); // Инициализируем колонки DataGridView вручную

        }

        // Метод для инициализации колонок DataGridView
        private void InitializeDataGridViewColumns()
        {
            dataGridView1.Columns.Add("NameOperator", "Название оператора");
            dataGridView1.Columns.Add("Radius", "Площадь покрытия");
            dataGridView1.Columns.Add("MinutePrice", "Цена минуты");
            dataGridView1.Columns.Add("ConnectSpeed", "Скорость соединения");
            dataGridView1.Columns.Add("AbonentsCount", "Количество абонентов");
            dataGridView1.Columns.Add("Connection", "Плата за соединение");
            dataGridView1.Columns.Add("Quality", "Качество ");
        }

        // Метод для обновления данных в DataGridView
        void GridUpdate()
        {
            dataGridView1.Rows.Clear(); // Очищаем все строки в DataGridView

            // LINQ запрос для сортировки операторов по качеству (по убыванию)
            var sortedOperators = from op in OPList
                                  orderby op.CalculateQuality() descending // Сортируем по убыванию качества
                                  select op;

            foreach (var temp in sortedOperators)
            {
                int index = dataGridView1.Rows.Add(); // Добавляем новую строк
                var row = dataGridView1.Rows[index]; 

                row.Cells["NameOperator"].Value = temp.Operatorname;
                row.Cells["Radius"].Value = temp.Radius;
                row.Cells["MinutePrice"].Value = temp.MinutePrice;
                row.Cells["ConnectSpeed"].Value = temp.ConnectSpeed;
                row.Cells["AbonentsCount"].Value = temp.AbonentsCount;
                row.Cells["Connection"].Value = temp.HasConnectionFee ? "Да" : "Нет"; // Преобразуем bool в строку
                row.Cells["Quality"].Value = temp.Quality.ToString("F2"); // Преобразуем double в строку
            }


        }

        // Метод для обновления статистики операторов
        private void UpdateStatistics()
        {
            OperatorPremium.ShowOperatorsStatistics(OPList); //метод для отображения статистики
        }

        // кнопка Добавить оператора
        private void AddOperatorButt(object sender, EventArgs e)
        {
            // Проверяем корректность введенных данных с помощью метода
            string ans = Validation.InputCorrect(textBox1.Text, (double)numericUpDown1.Value,
                (double)numericUpDown2.Value, (double)numericUpDown3.Value, (int)numericUpDown4.Value);

            // Если ошибок нет 
            if (ans == "")
            {
                bool con = radioButton1.Checked; // Получаем значение radio button (наличие платы за соединение)


                AddOperator(textBox1.Text,
                    (double)numericUpDown1.Value,
                    (double)numericUpDown2.Value,
                    (double)numericUpDown3.Value,
                    (int)numericUpDown4.Value,
                    con); 

                GridUpdate(); // Обновляем DataGridView
                ClearInputs(); // Очищаем поля ввода
            }
            else
            {
                MessageBox.Show(ans, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // добавление оператора
        private void AddOperator(OperatorPremium op)
        {
            OPList.Add(op); // Добавляем оператора в список
            OPDict[op.Operatorname] = op; 
        }

        //добавление оператора
        private void AddOperator(string name, double price, double radius, double speed, int count, bool connect)
        {
            var op = new OperatorPremium(name, price, radius, speed, count, connect); // Создаем новый объект
            AddOperator(op); // Вызываем перегруженный метод для добавления
        }

        // метод удаления
        private void RemoveOperator(string name)
        {
            // Ищем оператора в списке по имени
            var opToRemove = OPList.FirstOrDefault(op => op.Operatorname == name);
            if (opToRemove != null)
            {
                OPList.Remove(opToRemove); // Удаляем из списка
                OPDict.Remove(name); // Удаляем из словаря
            }
        }

        // метод удаления
        private void RemoveOperator(OperatorPremium op)
        {
            OPList.Remove(op); // Удаляем из списка
            OPDict.Remove(op.Operatorname); // Удаляем из словаря
        }

        // Метод для очистки полей ввода
        private void ClearInputs()
        {
            textBox1.Text = "";
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown3.Value = 1;
            numericUpDown4.Value = 1;
            radioButton1.Checked = true;
        }

        // добавить оператора
        private void добавитьОператораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true; // Отображаем панель с полями ввода
        }

        // удаление выбраного
        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string operatorName = dataGridView1.SelectedRows[0].Cells["NameOperator"].Value.ToString();
                RemoveOperator(operatorName); // Удаляем оператора
                GridUpdate(); // Обновляем DataGridView
            }
        }

        // поиск лучшего
        private void btnFindBest_Click(object sender, EventArgs e)
        {
            // Используем LINQ для поиска оператора с наивысшим качеством
            var bestOperator = OPList.OrderByDescending(op => op.CalculateQuality())
                                   .FirstOrDefault();

            // Вызываем метод ShowBestOperatorInfo для отображения информации о лучшем операторе
            bestOperator?.ShowBestOperatorInfo();
        }

        // Сохранение
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveOperatorsToFile(); // Вызываем метод для сохранения операторов в файл
        }

        // Метод для сохранения операторов в файл
        private void SaveOperatorsToFile()
        {
            try
            {
                // Открываем файл для записи
                using (StreamWriter writer = new StreamWriter(OperatorsFile))
                {
                    // Перебираем всех операторов в списке
                    foreach (var op in OPList)
                    {
                        // Записываем данные оператора в фай
                        writer.WriteLine($"{op.Operatorname};{op.MinutePrice};{op.Radius};" +
                                       $"{op.ConnectSpeed};{op.AbonentsCount};" +
                                       $"{(op.HasConnectionFee ? 1 : 0)}"); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для загрузки операторов из файла
        private void LoadOperatorsFromFile()
        {
            // Если файл не существует, выходим из метода
            if (!File.Exists(OperatorsFile)) return;

            try
            {
                // Очищаем списки перед загрузкой новых данных
                OPList.Clear();
                OPDict.Clear();

                foreach (string line in File.ReadAllLines(OperatorsFile))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Разбиваем строку на части по разделителю ';'
                    string[] parts = line.Split(';');
                    // Если количество частей не равно 6 ио пропускаем строку
                    if (parts.Length != 6) continue;

                    var op = new OperatorPremium(
                        parts[0], // Name
                        double.Parse(parts[1]), // MinutePrice
                        double.Parse(parts[2]), // Radius
                        double.Parse(parts[3]), // ConnectSpeed
                        int.Parse(parts[4]), // AbonentsCount
                        parts[5] == "1"); // HasConnectionFee (преобразуем строку в bool)

                    // Добавляем оператора в списки
                    OPList.Add(op);
                    OPDict[op.Operatorname] = op;
                }

                GridUpdate(); // Обновляем DataGridView
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, показываем MessageBox с сообщением об ошибке
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // кнопка загрузки
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadOperatorsFromFile(); // Вызываем метод для загрузки операторов из файла
        }

        // кнопка вывода статистики
        private void вывестиСтатистикуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatistics(); // Вызываем метод для обновления статистики операторов
        }

        // Удаление по даблклику
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Если клик был по заголовку (e.RowIndex < 0), выходим из метода
            if (e.RowIndex < 0) return;

            // Получаем объект OperatorPremium, привязанный к выбранной строке
            string operatorName = dataGridView1.Rows[e.RowIndex].Cells["NameOperator"].Value.ToString(); // Получаем имя оператора из ячейки
            OperatorPremium selectedOperator = OPList.FirstOrDefault(op => op.Operatorname == operatorName); // Ищем оператора в списке

            // Если оператор найден
            if (selectedOperator != null)
            {

                var result = MessageBox.Show(
                    $"Вы точно хотите удалить оператора {selectedOperator.Operatorname}?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                // Если пользователь нажал "Да"
                if (result == DialogResult.Yes)
                {
                    RemoveOperator(selectedOperator);
                    GridUpdate(); 
                    MessageBox.Show("Оператор успешно удален", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Не удалось найти оператора для удаления."); 
            }

        }
    }

}
