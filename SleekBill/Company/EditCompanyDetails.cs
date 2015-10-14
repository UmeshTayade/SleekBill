using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects;
using DataServices;
using DataServices.Interfaces;
using System.IO;
using System.Drawing;
using Sleek_Bill.Common;
using Sleek_Bill.Controls;

namespace Sleek_Bill.Company
{
    public partial class EditCompanyDetails : Form
    {
        private IMaster masterService = new MasterService();
        private ICompany companyService = new CompanyService();
        public string ImageName { get; set; }
        public int CompanyID { get; set; }

        public EditCompanyDetails()
        {
            InitializeComponent();
        }

        private void ShowActiveCompanyDetails()
        {
            BusinessObjects.Company activeCompany = (from company in companyService.GetAllCompany()
                                                        .Where(v => v.Status == true)
                                                         select company).SingleOrDefault();
            SetControlsWithData(activeCompany);
        }

        private void SetControlsWithData(BusinessObjects.Company activeCompany)
        {
            if (activeCompany == null)
                return;

            CompanyID = activeCompany.CompanyId;
            txtCompanyName.Text = activeCompany.CompanyName;
            txtAddress.Text = activeCompany.Address;
            txtCity.Text = activeCompany.City;
            cmbState.SelectedValue = activeCompany.State;
            txtCompanyPhone.Text = activeCompany.CompanyPhone;
            txtEmail.Text = activeCompany.Email;
            txtWebsite.Text = activeCompany.Website;
            txtTIN.Text = activeCompany.TIN;
            txtServiceTaxNo.Text = activeCompany.ServiceTaxNo;
            txtAdditionalDetails.Text = activeCompany.AdditionalDetails;
            txtPAN.Text = activeCompany.PAN;
            cmbCurrency.SelectedValue = activeCompany.Currency;

            string absolutePath = Path.GetDirectoryName(Application.ExecutablePath);

            if (absolutePath.EndsWith("\\bin\\Debug"))
            {
                absolutePath = absolutePath.Replace("\\bin\\Debug", "");
            }

            string imagePath = Path.Combine(absolutePath, Constants.CONSTANT_IMAGES, Constants.CONSTANT_CompanyLogo);
            ImageName = activeCompany.Logo;
            Image image = Image.FromFile(Path.Combine(imagePath, activeCompany.Logo), true);
            pnlLogo.Controls.Clear();
            PictureBox pbLogo = new PictureBox();
            pbLogo.Image = image;
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.Size = pnlLogo.Size;
            pnlLogo.Controls.Add(pbLogo);
            btnRemove.Visible = true;            
        }

        private void EditCompanyDetails_Load(object sender, EventArgs e)
        {
            Common.Common.SetFormCoordinate(this);
            LoadStates();
            ShowActiveCompanyDetails();
        }

        private void LoadStates()
        {
            List<State> lstState = masterService.GetAllStates();
            lstState.Insert(0, new State("DF", "-- Select State --"));
            cmbState.DataSource = lstState;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "StateCode";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BusinessObjects.Company company = new BusinessObjects.Company();
                company.CompanyId = CompanyID;
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
                company.Logo = ImageName;
                company.Status = true;
                companyService.UpdateCompany(company);
                CustomMessageBox.Show(string.Format(Constants.SUCCESSFULL_SAVE_MESSAGE, txtCompanyName.Text),
                                                                  Constants.CONSTANT_INFORMATION,
                                                                  Sleek_Bill.Controls.CustomMessageBox.eDialogButtons.OK,
                                                                  CustomImages.GetDialogImage(Sleek_Bill.Controls.CustomImages.eCustomDialogImages.Success));
            }
            catch(Exception ex){
                MessageBox.Show("Error : "+ex.Message);
            }
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
                    string imageName = openFileDialog1.SafeFileName;
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

                    imagePath = Path.Combine(imagePath, imageName);
                    ImageName = imageName;
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
    }
}
