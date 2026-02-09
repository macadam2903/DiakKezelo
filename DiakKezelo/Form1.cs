using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

namespace DiakKezelo
{
    public partial class Form1 : Form
    {
        // adatb
        string connectionString = "Server=localhost;Port=3307;Database=diak_adatbazis;Uid=root;Pwd=;";
        string selectedCsvPath = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudentsToGrid();
        }

        // openfiledialoh
        private void btnSelectCsv_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV fájlok (*.csv)|*.csv";
            openFileDialog.Title = "Válassz CSV fájlt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedCsvPath = openFileDialog.FileName;
                MessageBox.Show("Fájl kiválasztva: " + Path.GetFileName(selectedCsvPath));
            }
        }

        // readin
        private void btnLoadCsv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedCsvPath))
            {
                MessageBox.Show("Először válassz ki egy CSV fájlt!");
                return;
            }

            try
            {
                // csv read
                List<Student> students = new List<Student>();

                StreamReader reader = new StreamReader(selectedCsvPath);
                CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<Student>();
                foreach (var record in records)
                {
                    students.Add(record);
                }

                reader.Close();

                // adatb save
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                int count = 0;
                foreach (Student student in students)
                {
                    string query = "INSERT INTO Students (Name, Email, Age) VALUES (@Name, @Email, @Age)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@Age", student.Age);

                    cmd.ExecuteNonQuery();
                    count++;
                }

                conn.Close();

                MessageBox.Show(count + " diák sikeresen importálva!");
                LoadStudentsToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }

        // reg
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // check
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Minden mezőt ki kell tölteni!");
                return;
            }

            int age = 0;
            if (!int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Az életkor számnak kell lennie!");
                return;
            }

            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string query = "INSERT INTO Students (Name, Email, Age) VALUES (@Name, @Email, @Age)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Age", age);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Diák sikeresen regisztrálva!");

                
                txtName.Clear();
                txtEmail.Clear();
                txtAge.Clear();

                LoadStudentsToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }

        // grid refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudentsToGrid();
        }


        private void LoadStudentsToGrid()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string query = "SELECT * FROM Students ORDER BY Id DESC";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvStudents.DataSource = dt;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }
    }
}