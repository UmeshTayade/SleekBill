using System;
using System.Drawing;
using System.Windows.Forms;
using Sleek_Bill.Common;
using BusinessObjects;
using DataServices.Interfaces;
using DataServices;
using System.Collections.Generic;
using Sleek_Bill.Controls;
using System.IO;

namespace Sleek_Bill_Company
{
    public partial class AddCompanyDetails : Form
    {
        private IMaster masterService = new MasterService();
        private ICompany companyService = new CompanyService();
        public string ImagePath { get; set; }

        public AddCompanyDetails()
        {
            InitializeComponent();
        }

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
                    string fileName = openFileDialog1.SafeFileName;
                    Image image = Image.FromFile(openFileDialog1.FileName);
                    string absolutePath = Path.GetDirectoryName(Application.ExecutablePath);
                    
                    if (absolutePath.EndsWith("\\bin\\Debug"))
                    {
                        absolutePath = absolutePath.Replace("\\bin\\Debug", "");
                    }

                    string imagePath = Path.Combine(absolutePath, Constants.CONSTANT_IMAGES, Constants.CONSTANT_CompanyLogo);

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    imagePath = Path.Combine(imagePath, fileName);
                    ImagePath = imagePath;
                    image.Save(imagePath);
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
                btnRemove.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing image" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Company company = new Company();
                company.CompanyName = txtCompanyName.Text.Trim();
                company.Country = txtCountry.Text.Trim();
                company.Address = txtAddress.Text.Trim();
                company.City = txtCity.Text.Trim();
                company.State = cmbState.SelectedValue.ToString();
                company.CompanyPhone = txtCompanyPhone.Text.Trim();
                company.Email = txtEmail.Text.Trim();
                company.Website = txtWebsite.Text.Trim();
                company.TIN = txtTIN.Text.Trim();
                company.ServiceTaxNo = txtServiceTaxNo.Text.Trim();
                company.AdditionalDetails = txtAdditionalDetails.Text.Trim();
                company.PAN = txtPAN.Text.Trim();
                company.Currency = String.Empty;
                company.Logo = ImagePath;
                company.Status = true;
                int companyId = companyService.AddCompany(company);
                CustomMessageBox.Show(string.Format(Constants.SUCCESSFULL_ADD_MESSAGE, Constants.CONSTANT_COMPANY, txtCompanyName.Text),
                                                    Constants.CONSTANT_INFORMATION,
                                                    CustomMessageBox.eDialogButtons.OK,
                                                    CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Success));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

        }

        private void AddCompanyDetails_Load(object sender, EventArgs e)
        {
            Common.SetFormCoordinate(this);
            LoadStates();
        }

        private void LoadStates()
        {
            List<State> lstState = masterService.GetAllStates();
            lstState.Insert(0, new State("DF", "-- Select State --"));
            cmbState.DataSource = lstState;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "StateCode";
        }
    }
}
