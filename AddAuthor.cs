using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class AddAuthor : Form
    {
        private readonly Author author;
        public AddAuthor(Author author)
        {
            InitializeComponent();
            this.author = author;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            author.Firstname = txtName.Text;
            author.Surname = txtSurname.Text;
            author.YearOfBirthday = dateTimePickerBirthday.Value;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
