using System;
using System.Drawing;
using System.Windows.Forms;
using Sleek_Bill.Common;

namespace Sleek_Bill_Company
{
    public partial class AddCompanyDetails : Form
    {
        public AddCompanyDetails()
        {
            InitializeComponent();
        }

        OpenFileDialog fd1 = new OpenFileDialog();
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "d:\\";
            openFileDialog1.Filter = "image files|*.jpg;*.png;*.gif;*.icon;.*;";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pnlLogo.Controls.Clear();
                    PictureBox pbLogo = new PictureBox();
                    Image image = Image.FromFile(openFileDialog1.FileName);
                    pbLogo.Image = image;
                    pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbLogo.Size = pnlLogo.Size;
                    pnlLogo.Controls.Add(pbLogo);
                    btnRemove.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                pnlLogo.Controls.Clear();
                btnRemove.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing image" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCompanyName.Text))
                MessageBox.Show(string.Format(Constants.SUCCESSFULL_MESSAGE, txtCompanyName.Text));
        }

        private void AddCompanyDetails_Load(object sender, EventArgs e)
        {
            Common.SetFormCoordinate(this);            
        }
    }
}
