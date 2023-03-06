using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class AddBook : Form
    {
        private readonly Book book;
        string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookLibrary;Integrated Security=True;";
        public AddBook(Book book)
        {
            InitializeComponent();
            this.book = book;           
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                book.Title = txtTitle.Text;
                book.Price=double.Parse(txtPrice.Text);
                book.YearOfPublish = int.Parse(txtYear.Text);
                int pages;
                if (int.TryParse(txtPages.Text, out pages))
                    book.Pages = pages;
                else
                    MessageBox.Show("Error!");
            }
            book.AuthorId = (int)comboBox1.SelectedValue;
            DialogResult = DialogResult.OK;
        }

        private void AddBook_Load(object sender, EventArgs e)
        {
            List<Author> authors = new List<Author>();
            using (SqlConnection conn = new SqlConnection(connStr)) {
                SqlDataReader reader = null;
                string query = "select * from Authors";
                SqlCommand command = new SqlCommand(query, conn);
                try
                {
                    conn?.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Author author = new Author();
                        author.Id = reader.GetInt32(0);
                        author.Firstname = reader.GetString(1);
                        author.Surname = reader.GetString(2);
                        author.YearOfBirthday = reader.GetDateTime(3);
                        authors.Add(author);
                    }
                    comboBox1.DataSource = null;
                    comboBox1.DisplayMember = "Fullname";
                    comboBox1.ValueMember = "Id";
                    comboBox1.DataSource = authors;

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    reader?.Close();
                    conn?.Close();
                }
                    }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
