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
    public partial class Form1 : Form
    {
        string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BookLibrary;Integrated Security=True;";
        DataTable dt = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadDB_Click(object sender, EventArgs e)
        {
            UploadDB("select * from Authors", dataGridView1);
            UploadDB("select B.Id, B.Title, B.Price, B.Pages, A.Surname+' '+ A.Fistname Fullname  from Books B join Authors A on A.Id=B.AuthorId", dataGridView2);
        }
        private void UploadDB(string query, DataGridView dataGridView)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataReader reader = null;
                dt = new DataTable();
                SqlCommand command=new SqlCommand(query,conn);
                try
                {
                    conn.Open();
                    reader=command.ExecuteReader();
                    int line = 0;
                    //dt.Columns.Add("Id");
                    //dt.Columns.Add("Fistname");
                    //dt.Columns.Add("Surname");
                    //dt.Columns.Add("Year of birthday");
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dt.Columns.Add(reader.GetName(i));
                            }
                        }
                        DataRow row = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        dt.Rows.Add(row);
                        line++;
                      
                    }
                    //DataRow third = dt.NewRow();
                    //third[0] = 5;
                    //third[1] = "Vika";
                    //third[2] = "Sushko";
                    //third[3] = new DateTime(1985, 5, 3);
                    //dt.Rows.Add(third);
                    dataGridView.DataSource = dt;
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

        private void btnAuthor_Click(object sender, EventArgs e)
        {
            Author author = new Author();
            AddAuthor authorForm = new AddAuthor(author);
            if(authorForm.ShowDialog() == DialogResult.OK)
            {
                AddAuthorToDb(author);
                UploadDB("select * from Authors", dataGridView1);
            }
        }

        private void AddAuthorToDb(Author author)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = $"insert into Authors values(N'{author.Firstname}',N'{author.Surname}', N'{author.YearOfBirthday.ToString("yyyy-MM-d")}')";
                SqlCommand command = new SqlCommand(query, conn);
                try
                {
                    conn?.Open();
                    int count = command.ExecuteNonQuery();
                    MessageBox.Show($"було вставлено {count} рядків");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                {                    
                    conn?.Close();
                }
            }
            }

        private void btnBook_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            AddBook addBook = new AddBook(book);
            if (addBook.ShowDialog() == DialogResult.OK)
            {
                AddBookToDb(book);
                UploadDB("select B.Id, B.Title, B.Price, B.Pages, A.Surname+' '+ A.Fistname Fullname  from Books B join Authors A on A.Id=B.AuthorId", dataGridView2);

            }
        }

        private void AddBookToDb(Book book)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = $"insert into Books values(N'{book.Title}',N'{book.Price}', N'{book.YearOfPublish}',N'{book.Pages}', N'{book.AuthorId}')";
                SqlCommand command = new SqlCommand(query, conn);
                try
                {
                    conn?.Open();
                    int count = command.ExecuteNonQuery();
                    MessageBox.Show($"було вставлено {count} рядків");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
    }
}
