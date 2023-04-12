using System.Runtime.Serialization.Formatters.Binary;

namespace DataSerialization
{
    public partial class MainForm : Form
    {
        private object dataGridView1;

        public object MessageBox { get; private set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        // Функція для збереження даних DataGridView у бінарний файл
        private void SaveBinaryButton_Click(object sender, EventArgs e)
        {
            // Створюємо об'єкт BinaryFormatter для серіалізації об'єкту
            BinaryFormatter formatter = new BinaryFormatter();

            // Створюємо файл для збереження
            using (FileStream stream = new FileStream("data.bin", FileMode.Create))
            {
                // Серіалізуємо дані DataGridView і записуємо у файл
                formatter.Serialize(stream, dataGridView1.DataSource);
            }

            MessageBox.Show("Data saved to binary file!");
        }

        // Функція для збереження даних DataGridView у текстовий файл
        private void SaveTextButton_Click(object sender, EventArgs e)
        {
            // Створюємо файл для збереження
            using (StreamWriter writer = new StreamWriter("data.txt"))
            {
                // Проходимо по всіх рядках DataGridView і записуємо дані у файл
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        writer.Write(cell.Value.ToString() + ",");
                    }
                    writer.WriteLine();
                }
            }

            MessageBox.Show("Data saved to text file!");
        }

        // Функція для відновлення даних DataGridView з бінарного файлу
        private void LoadBinaryButton_Click(object sender, EventArgs e)
        {
            // Створюємо об'єкт BinaryFormatter для десеріалізації об'єкту
            BinaryFormatter formatter = new BinaryFormatter();

            // Відкриваємо файл для читання
            using (FileStream stream = new FileStream("data.bin", FileMode.Open))
            {
                // Десеріалізуємо дані з файлу і присвоюємо їх властивості DataSource DataGridView
                dataGridView1.DataSource = formatter.Deserialize(stream);
            }

            MessageBox.Show("Data loaded from binary file!");
        }

        // Функція для відновлення даних DataGridView з текстового файлу
        private void LoadTextButton_Click(object sender, EventArgs e)
        {
            // Очищаємо DataGridView перед завантаженням нових даних
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Відкриваємо файл для читання
            using (StreamReader reader = new StreamReader("data.txt"))
            {
                // Отримуємо перший рядок файла, щоб визначити кількість стовпців у DataGridView
                string[] headers = reader.ReadLine().Split(',');
int numCols = headers.Length;
            // Додаємо стовпці до DataGridView  
            for (int i = 0; i < numCols; i++)
            {
    dataGridView1.Columns.Add("Column" + i, headers[i]);
}

// Читаємо решту рядків з файлу і заповнюємо DataGridView
while (!reader.EndOfStream)
{
    string[] values = reader.ReadLine().Split(',');
    dataGridView1.Rows.Add(values);
}
        }

        MessageBox.Show("Data loaded from text file!");
    }
}
}