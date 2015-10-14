﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects;
using DataServices;
using DataServices.Interfaces;
using Sleek_Bill.Common;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;
using System.Diagnostics;

namespace Sleek_Bill.Invoices
{
    public partial class AddEditInvoice : Form
    {
        IClient clientService = new ClientService();
        ICompany companyService = new CompanyService();
        IMaster masterService = new MasterService();
        IProduct productService = new ProductService();
        IInvoice invoiceService = new InvoiceService();
        List<InvoiceProduct> lstInvoiceProduct = new List<InvoiceProduct>();
        InvoiceProduct invoiceProduct = null;
        public string CompanyFolderName { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }

        private decimal totalValue = decimal.Zero;
        public decimal TotalValue
        {
            get
            {
                return totalValue;
            }
            set
            {
                totalValue = value;
            }
        }

        public string InvoiceFolderPath
        {
            get
            {
                return string.Format(ConfigurationManager.AppSettings["InvoicePath"], CompanyFolderName, ClientName);
            }
        }

        public AddEditInvoice()
        {
            InitializeComponent();
        }

        private void AddEditInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                Common.Common.SetLargeDialogCoordinate(this);
                SetDefaultData();
                BindClientNameDropdown();
                BindPaymentTermDropdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }

        private void LoadTaxDropDown()
        {
            List<Tax> lstTax = (from tax in masterService.GetAllTaxes()
                               .Where(v => v.Status == true)
                                select new Tax
                                {
                                    TaxId = tax.TaxId,
                                    TaxName = tax.TaxName + " (" + tax.TaxPercentage + ")"
                                }).ToList<Tax>();
            lstTax.Insert(0, new Tax(0, "-- Select Tax --"));
            cmbTax.DataSource = lstTax;
            cmbTax.DisplayMember = "TaxName";
            cmbTax.ValueMember = "TaxId";

        }

        private void SetDefaultData()
        {
            txtInvoiceNumber.Enabled = false;
            var lstInvoice = invoiceService.GetAllInvoices();

            if (lstInvoice != null && lstInvoice.Count == 0)
                txtInvoiceNumber.Text = Constants.CONSTANT_INOVICE_NO;
            else
            {
                int invoiceId = lstInvoice.Max(v => v.InvoiceId);
                txtInvoiceNumber.Text = Convert.ToString(++invoiceId);
            }

            var lstCompany = (from Company in companyService.GetAllCompany()
                             .Where(v => v.Status == true)
                              select Company).SingleOrDefault();
            CompanyFolderName = lstCompany.CompanyName;
        }

        private void BindProductDropdown()
        {
            List<Product> lstProduct = productService.GetAllProducts();
            lstProduct.Insert(0, new Product(0, "-- Select Product --"));
            cmbProduct.DataSource = lstProduct;
            cmbProduct.DisplayMember = "ProductName";
            cmbProduct.ValueMember = "ProductId";
        }

        private void BindPaymentTermDropdown()
        {
            List<PaymentTerms> lstPaymentTerms = masterService.GetAllPaymentTerms();
            cmbPaymentTerms.DataSource = lstPaymentTerms;
            cmbPaymentTerms.DisplayMember = "PaymentTermName";
            cmbPaymentTerms.ValueMember = "PaymentTerm";
        }

        private void BindClientNameDropdown()
        {
            List<Client> lstClient = clientService.GetAllClients();
            lstClient.Insert(0, new Client(0, "-- Select Client --"));
            cmbClient.DataSource = lstClient;
            cmbClient.DisplayMember = "ClientName";
            cmbClient.ValueMember = "ClientId";
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex != 0)
            {
                int productId = Convert.ToInt32(cmbProduct.SelectedValue);
                Product product = productService.GetProduct(productId);
                txtDescription.Text = product.Description;
                txtQuantity.Text = Convert.ToString(1);
                txtUnitPrice.Text = Convert.ToString(product.UnitPrice);
                cmbTax.SelectedValue = product.TaxId;
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<Tax> lstTax = masterService.GetAllTaxes();
            invoiceProduct = new InvoiceProduct();
            invoiceProduct.ProductId = Convert.ToInt32(cmbProduct.SelectedValue);
            invoiceProduct.ProductName = cmbProduct.Text.Trim();
            invoiceProduct.Description = txtDescription.Text.Trim();
            invoiceProduct.Quantity = String.IsNullOrEmpty(txtQuantity.Text) ? 0 : Convert.ToInt32(txtQuantity.Text);
            invoiceProduct.UnitPrice = String.IsNullOrEmpty(txtUnitPrice.Text) ? decimal.Zero : Math.Round(Convert.ToDecimal(txtUnitPrice.Text), 2);
            invoiceProduct.TotalPrice = Math.Round(invoiceProduct.Quantity * invoiceProduct.UnitPrice, 2);
            invoiceProduct.TaxId = Convert.ToInt32(cmbTax.SelectedValue);
            var objTax = (from tax in lstTax
                      .Where(v => v.TaxId == invoiceProduct.TaxId)
                          select tax).SingleOrDefault();

            invoiceProduct.TaxValue = Math.Round((invoiceProduct.Quantity * invoiceProduct.UnitPrice) * objTax.TaxPercentage * (decimal)0.01, 2);
            lstInvoiceProduct.Add(invoiceProduct);
            BindInvoiceProductGrid();
            ClearProductControls();
            CalculateInvoice();
        }

        private void CalculateInvoice()
        {
            pnlSubtotal.Visible = true;
            pnlTaxValue.Visible = true;
            decimal subTotal = decimal.Zero;
            decimal serviceTax = decimal.Zero;

            foreach (InvoiceProduct product in lstInvoiceProduct)
            {
                subTotal = Math.Round(subTotal + product.TotalPrice, 2);
                serviceTax = Math.Round(serviceTax + product.TaxValue, 2);
            }

            TotalValue = subTotal + serviceTax;
            decimal discPercentage = String.IsNullOrEmpty(txtDiscount.Text) ? decimal.Zero : Math.Round(Convert.ToDecimal(txtDiscount.Text), 2);
            decimal discountValue = Math.Round(TotalValue * discPercentage * (decimal)0.01, 2);
            TotalValue = TotalValue - discountValue;
            decimal shippingAndPackaging = String.IsNullOrEmpty(txtShippingnPackaging.Text) ? decimal.Zero : Math.Round(Convert.ToDecimal(txtShippingnPackaging.Text), 2);
            TotalValue = TotalValue + shippingAndPackaging;
            lblSubtotalValue.Text = subTotal.ToString();
            lblTaxValue.Text = serviceTax.ToString();
            lblDiscountValue.Text = "- " + discountValue.ToString();

            if (chkRoundOff.Checked)
            {
                decimal roundedValue = Math.Round(TotalValue);
                decimal value = roundedValue - TotalValue;
                lblRoundedOffValue.Text = Convert.ToString(value);
                lblTotalValue.Text = Convert.ToString(roundedValue);
            }
            else
            {
                lblTotalValue.Text = Convert.ToString(TotalValue);
            }

            if (chkMarkInvoicePaid.Checked)
            {
                txtAmountPaid.Text = (TotalValue - shippingAndPackaging).ToString();
            }
        }

        private void BindInvoiceProductGrid()
        {
            int srNo = 0;
            var columns = (from prod in lstInvoiceProduct
                           select new
                           {
                               No = ++srNo,
                               ProductName = prod.ProductName,
                               Description = prod.Description,
                               Quantity = prod.Quantity,
                               UnitPrice = prod.UnitPrice,
                               Value = prod.TotalPrice,
                               Tax = prod.TaxValue
                           }).ToList();
            dgvInvoice.DataSource = columns;
        }

        private void ClearProductControls()
        {
            cmbProduct.SelectedIndex = 0;
            txtDescription.Text = String.Empty;
            txtQuantity.Text = String.Empty;
            txtUnitPrice.Text = String.Empty;
            cmbTax.SelectedIndex = 0;
        }

        private void btnPreviewInvoice_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(InvoiceFolderPath, string.Format("Invoice{0}.pdf", txtInvoiceNumber.Text));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            string docName = string.Format("Invoice{0}.pdf", txtInvoiceNumber.Text);
            GenerateInvoicePDF();
        }

        private void GenerateInvoicePDF()
        {
            try
            {
                Document document = new Document(PageSize.A4, 20f, 20f, 10f, 0f);

                if (!Directory.Exists(InvoiceFolderPath))
                {
                    Directory.CreateDirectory(InvoiceFolderPath);
                }

                PdfWriter.GetInstance(document, new FileStream(string.Format(InvoiceFolderPath + @"/Invoice{0}.pdf", txtInvoiceNumber.Text), FileMode.Create));
                document.Open();
                //Tables for Logo and Invoice Details
                PdfPTable outerHeader = new PdfPTable(2);
                outerHeader.DefaultCell.BorderWidth = 0;
                outerHeader.TotalWidth = 750f;
                outerHeader.WidthPercentage = 100;
                outerHeader.SpacingAfter = 20f;
                outerHeader.HorizontalAlignment = Element.ALIGN_LEFT;
                PdfPTable pdfCompanyTable = CreateCompanyGrid();
                PdfPTable pdfInvoiceTable = CreateInvoiceGrid();
                outerHeader.AddCell(pdfCompanyTable);
                outerHeader.AddCell(pdfInvoiceTable);

                //Tables for Billing and Shipping Address
                PdfPTable outerAddress = new PdfPTable(2);
                outerAddress.DefaultCell.BorderWidth = 0;
                outerAddress.TotalWidth = 750f;
                outerAddress.WidthPercentage = 100;
                outerAddress.SpacingAfter = 10f;
                outerAddress.HorizontalAlignment = Element.ALIGN_LEFT;
                PdfPTable pdfBillingAddressTable = CreateBillingAddressGrid();
                outerAddress.AddCell(pdfBillingAddressTable);
                PdfPTable pdfShippingAddressTable = CreateShippingAddressGrid();
                outerAddress.AddCell(pdfShippingAddressTable);

                //Product Table
                PdfPTable pdfProductTable = CreateProductsGrid();

                //Tables for Authorized Signatory and Invoice Amounts
                PdfPTable outerInvoiceDetails = new PdfPTable(2);
                outerInvoiceDetails.DefaultCell.BorderWidth = 0;
                outerInvoiceDetails.TotalWidth = 750f;
                outerInvoiceDetails.WidthPercentage = 100;
                outerInvoiceDetails.SpacingAfter = 20f;
                outerInvoiceDetails.HorizontalAlignment = Element.ALIGN_LEFT;
                PdfPTable pdfAuthorizedSignatoryTable = CreateAuthorizedSignatoryGrid();
                outerInvoiceDetails.AddCell(pdfAuthorizedSignatoryTable);
                PdfPTable pdfinvoiceAmountTable = CreateInvoiceAmountDetailsGrid();
                outerInvoiceDetails.AddCell(pdfinvoiceAmountTable);
                PdfPTable pdfNotes = CreateNotesGrid();
                //Adding all tables to the document
                document.Add(outerHeader);
                document.Add(outerAddress);
                document.Add(pdfProductTable);
                document.Add(outerInvoiceDetails);
                document.Add(pdfNotes);
                document.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error previwing Invoice :" + ex.Message);
            }
        }

        private PdfPTable CreateNotesGrid()
        {
            PdfPTable pdfTable = new PdfPTable(1);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.TotalWidth = 400f;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 0;
            pdfTable.SpacingAfter = 10;
            AddCustomCell(pdfTable, "Note:", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.BOLD, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, txtNoteForClient.Text, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 12, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, String.Empty, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 12, iTextSharp.text.Font.BOLD, 20f, PdfPCell.ALIGN_LEFT);

            return pdfTable;
        }

        private PdfPTable CreateInvoiceAmountDetailsGrid()
        {
            PdfPTable pdfTable = new PdfPTable(2);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.TotalWidth = 400f;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 0;
            pdfTable.SpacingAfter = 20;
            AddCustomCell(pdfTable, "Subtotal", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, lblSubtotalValue.Text, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_RIGHT);
            AddCustomCell(pdfTable, "Service Tax ()", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, lblTaxValue.Text, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_RIGHT);

            if (chkDiscount.Checked)
            {
                AddCustomCell(pdfTable, "Discount (" + txtDiscount.Text + ")", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, lblDiscountValue.Text, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_RIGHT);
            }

            AddCustomCell(pdfTable, "Shipping & Packaging", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, txtShippingnPackaging.Text, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_RIGHT);

            if (chkRoundOff.Checked)
            {
                AddCustomCell(pdfTable, "Rounded off", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, lblRoundedOffValue.Text, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_RIGHT);
            }

            AddCustomCell(pdfTable, "Total Invoice", 1, 1, BaseColor.WHITE, new BaseColor(51, 102, 153), 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, lblTotalValue.Text, 1, 1, BaseColor.WHITE, new BaseColor(51, 102, 153), 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_RIGHT);
            AddCustomCell(pdfTable, Common.Common.ConvertDecimalToWord(lblTotalValue.Text), 2, 2, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, String.Empty, 1, 2, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_RIGHT);

            return pdfTable;
        }

        private PdfPTable CreateAuthorizedSignatoryGrid()
        {
            PdfPTable pdfTable = new PdfPTable(1);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.TotalWidth = 350f;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 0;
            pdfTable.SpacingAfter = 20;
            AddCustomCell(pdfTable, "Authorized Signatory ", 1, 1, BaseColor.BLACK, BaseColor.WHITE, 12, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, String.Empty, 8, 1, BaseColor.BLACK, BaseColor.WHITE, 12, iTextSharp.text.Font.NORMAL, 20f, PdfPCell.ALIGN_LEFT);

            return pdfTable;
        }

        private PdfPTable CreateShippingAddressGrid()
        {
            var clientDetails = clientService.GetClient(Convert.ToInt32(cmbClient.SelectedValue));
            var stateDetails = masterService.GetAllStates();

            PdfPTable pdfTable = new PdfPTable(1);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.TotalWidth = 400f;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 0;

            if (clientDetails.ShipToDifferentAddress)
            {
                AddCustomCell(pdfTable, "Ship To: ", 1, 1, BaseColor.BLACK, BaseColor.WHITE, 12, iTextSharp.text.Font.BOLD, 20f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, clientDetails.ClientName, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, clientDetails.ShippingAddress, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
                var stateNames = stateDetails.Where(v => v.StateCode == clientDetails.ShippingStateCode).SingleOrDefault();
                string shippingAddress = string.Concat(clientDetails.ShippingCity, ",", stateNames.StateName, ",", clientDetails.ShippingZip, ",", clientDetails.Country);
                AddCustomCell(pdfTable, shippingAddress, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, "Phone: " + clientDetails.Phone, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, String.Empty, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
            }
            else
            {
                AddCustomCell(pdfTable, "Ship To: ", 1, 1, BaseColor.BLACK, BaseColor.WHITE, 12, iTextSharp.text.Font.BOLD, 20f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, clientDetails.ClientName, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, clientDetails.BillingAddress, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
                var stateNames = stateDetails.Where(v => v.StateCode == clientDetails.StateCode).SingleOrDefault();
                string billingAddress = string.Concat(clientDetails.City, ",", stateNames.StateName, ",", clientDetails.Zip, ",", clientDetails.Country);
                AddCustomCell(pdfTable, billingAddress, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, "Phone: " + clientDetails.Phone, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
                AddCustomCell(pdfTable, String.Empty, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
            }

            return pdfTable;
        }

        private PdfPTable CreateBillingAddressGrid()
        {
            var clientDetails = clientService.GetClient(Convert.ToInt32(cmbClient.SelectedValue));
            var stateDetails = masterService.GetAllStates();

            PdfPTable pdfTable = new PdfPTable(1);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.TotalWidth = 350f;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 0;
            AddCustomCell(pdfTable, "Bill To: ", 1, 1, BaseColor.BLACK, BaseColor.WHITE, 12, iTextSharp.text.Font.BOLD, 20f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, clientDetails.ClientName, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, clientDetails.BillingAddress, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
            var stateNames = stateDetails.Where(v => v.StateCode == clientDetails.StateCode).SingleOrDefault();
            string billingAddress = string.Concat(clientDetails.ShippingCity, ",", stateNames.StateName, ",", clientDetails.ShippingZip, ",", clientDetails.Country);
            AddCustomCell(pdfTable, billingAddress, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, "Phone: " + clientDetails.Phone, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);

            if (!String.IsNullOrEmpty(clientDetails.OtherClientDetails))
            {
                AddCustomCell(pdfTable, clientDetails.OtherClientDetails, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
            }

            if (!String.IsNullOrEmpty(clientDetails.TIN))
            {
                AddCustomCell(pdfTable, "TIN: " + clientDetails.TIN, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);
            }

            AddCustomCell(pdfTable, String.Empty, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 10, iTextSharp.text.Font.NORMAL, 15f, PdfPCell.ALIGN_LEFT);

            return pdfTable;
        }

        private PdfPTable CreateInvoiceGrid()
        {
            PdfPTable pdfTable = new PdfPTable(2);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.TotalWidth = 400f;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 0;
            pdfTable.SpacingBefore = 20;
            AddCustomCell(pdfTable, "Invoice", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 24, iTextSharp.text.Font.NORMAL, 50f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, txtInvoiceNumber.Text, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 24, iTextSharp.text.Font.NORMAL, 50f, PdfPCell.ALIGN_RIGHT);
            AddCustomCell(pdfTable, "Amount Due", 1, 1, BaseColor.WHITE, new BaseColor(51, 102, 153), 14, iTextSharp.text.Font.NORMAL, 40f, PdfPCell.ALIGN_LEFT);

            decimal amountDue = decimal.Zero;
            if (chkMarkInvoicePaid.Checked)
            {
                decimal amountPaid = String.IsNullOrEmpty(txtAmountPaid.Text) ? decimal.Zero : Convert.ToDecimal(txtAmountPaid.Text);
                amountDue = TotalValue - amountPaid;
            }
            else
            {
                amountDue = TotalValue;
            }

            AddCustomCell(pdfTable, amountDue.ToString(), 1, 1, BaseColor.WHITE, new BaseColor(51, 102, 153), 14, iTextSharp.text.Font.NORMAL, 40f, PdfPCell.ALIGN_RIGHT);
            AddCustomCell(pdfTable, "Invoice Date", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 25f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, DateTime.Now.Date.ToString("MMMM dd,yyyy"), 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 25f, PdfPCell.ALIGN_RIGHT);
            AddCustomCell(pdfTable, "Due Date", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 25f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, Convert.ToDateTime(dtpDueDate.Text).ToString("MMMM dd,yyyy"), 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 25f, PdfPCell.ALIGN_RIGHT);
            AddCustomCell(pdfTable, "P.O. Number", 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 25f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, txtPONumber.Text, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 25f, PdfPCell.ALIGN_RIGHT);
            AddCustomCell(pdfTable, String.Empty, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 25f, PdfPCell.ALIGN_LEFT);
            AddCustomCell(pdfTable, String.Empty, 1, 1, BaseColor.DARK_GRAY, BaseColor.WHITE, 14, iTextSharp.text.Font.NORMAL, 25f, PdfPCell.ALIGN_RIGHT);

            return pdfTable;
        }

        private PdfPTable CreateCompanyGrid()
        {
            PdfPTable pdfTable = new PdfPTable(1);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.TotalWidth = 350f;
            pdfTable.WidthPercentage = 100;
            pdfTable.DefaultCell.BorderWidth = 0;
            AddCompanyInfo(pdfTable);

            return pdfTable;
        }

        private void AddCompanyInfo(PdfPTable pdfTable)
        {
            try
            {
                string companyLogoFolderPath = Common.Common.GetCompanyLogoFolderPath();
                var company = companyService.GetAllCompany().SingleOrDefault();

                if (!String.IsNullOrEmpty(company.Logo))
                {
                    string logoUrl = Path.Combine(companyLogoFolderPath, company.Logo);
                    Image image = Image.GetInstance(logoUrl);
                    //Resize image depend upon your need
                    image.ScaleToFit(140f, 120f);
                    //Give space before image
                    image.SpacingBefore = 10f;
                    //Give some space after the image
                    image.SpacingAfter = 10f;
                    image.Alignment = Element.ALIGN_LEFT;
                    PdfPCell imageCell = new PdfPCell(image);
                    imageCell.Border = 0;
                    pdfTable.AddCell(imageCell);
                }
                else
                {
                    AddCustomCell(pdfTable, company.CompanyName, 1, 1, new BaseColor(51, 102, 153), BaseColor.WHITE, 40, iTextSharp.text.Font.NORMAL, 40f, PdfPCell.ALIGN_LEFT);
                }

                AddCompanyInfoCell(pdfTable, company.CompanyName, 1);
                AddCompanyInfoCell(pdfTable, company.Address, 1);
                string companyAddress = string.Concat(company.City, ",", company.State, ",", company.Zip, ",", company.Country);
                AddCompanyInfoCell(pdfTable, companyAddress, 1);
                AddCompanyInfoCell(pdfTable, "Phone: " + company.CompanyPhone, 1);
                AddCompanyInfoCell(pdfTable, "Email: " + company.Email, 1);
                AddCompanyInfoCell(pdfTable, company.AdditionalDetails, 1);

                if (!String.IsNullOrEmpty(company.TIN))
                {
                    AddCompanyInfoCell(pdfTable, "TIN: " + company.TIN, 1);
                }

                if (!String.IsNullOrEmpty(company.ServiceTaxNo))
                {
                    AddCompanyInfoCell(pdfTable, "Service Tax No: " + company.ServiceTaxNo, 1);
                }

                if (!String.IsNullOrEmpty(company.PAN))
                {
                    AddCompanyInfoCell(pdfTable, "PAN: " + company.PAN, 1);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private PdfPTable CreateProductsGrid()
        {
            PdfPTable pdfTable = new PdfPTable(5);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 0;
            pdfTable.TotalWidth = 545f;
            pdfTable.LockedWidth = true;
            pdfTable.SpacingAfter = 10f;
            float[] widths = new float[] { 50f, 220f, 75f, 100f, 100f };
            pdfTable.SetWidths(widths);
            int count = 0;
            //Adding Header row
            foreach (DataGridViewColumn column in dgvInvoice.Columns)
            {
                ++count;
                if (count == 1 || count == 2 || count == 5 || count == 9)
                    continue;

                AddHeaderCell(pdfTable, column.HeaderText, 1);
            }


            //Adding DataRow
            foreach (DataGridViewRow row in dgvInvoice.Rows)
            {
                count = 0;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    ++count;
                    if (count == 1 || count == 2 || count == 5 || count == 9)
                        continue;

                    AddCell(pdfTable, cell.Value.ToString(), 1);
                }
            }

            return pdfTable;
        }

        private void AddHeaderCell(PdfPTable table, string text, int rowspan)
        {
            BaseFont bfTimes = BaseFont.CreateFont("c:\\windows\\fonts\\cour.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.WHITE);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.FixedHeight = 20f;
            cell.NoWrap = false;
            cell.BackgroundColor = new BaseColor(51, 102, 153);
            cell.Rowspan = rowspan;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
        }

        private void AddCustomCell(PdfPTable table, string text, int rowspan, int colSpan, BaseColor fontColor, BaseColor backgroundColor, int fontSize, int fontStyle, float fixedHeight, int horizontalAlignemnt)
        {
            BaseFont bfTimes = BaseFont.CreateFont("c:\\windows\\fonts\\cour.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, fontSize, fontStyle, fontColor);
            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.Border = 0;
            cell.NoWrap = false;
            cell.FixedHeight = fixedHeight;
            cell.BackgroundColor = backgroundColor;
            cell.Rowspan = rowspan;
            cell.Colspan = colSpan;
            cell.HorizontalAlignment = horizontalAlignemnt;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
        }

        private void AddCompanyInfoCell(PdfPTable table, string text, int rowspan)
        {
            BaseFont bfTimes = BaseFont.CreateFont("c:\\windows\\fonts\\cour.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.DARK_GRAY);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.FixedHeight = 20f;
            cell.NoWrap = false;
            cell.Rowspan = rowspan;
            cell.Border = 0;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
        }

        private void AddCell(PdfPTable table, string text, int rowspan)
        {
            BaseFont bfTimes = BaseFont.CreateFont("c:\\windows\\fonts\\cour.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.DARK_GRAY);

            PdfPCell cell = new PdfPCell(new Phrase(text, times));
            cell.Rowspan = rowspan;
            cell.NoWrap = false;
            cell.FixedHeight = 20f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            table.AddCell(cell);
        }

        private void cmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClient.SelectedIndex != 0)
            {
                ClientName = cmbClient.Text;
                var clientDetails = clientService.GetClient(Convert.ToInt32(cmbClient.SelectedValue));

                if (clientDetails == null)
                {
                    MessageBox.Show("Error :" + "Loading client details failed");
                }
                else
                {
                    var stateDetails = masterService.GetAllStates();
                    ClientId = clientDetails.ClientId;

                    if (clientDetails.ShipToDifferentAddress)
                    {
                        var stateName = stateDetails.Where(v => v.StateCode == clientDetails.ShippingStateCode).SingleOrDefault();
                        lblShippingAddress.Text = String.Concat("Ship To: ", clientDetails.ShippingAddress, ", ", clientDetails.ShippingZip,
                                                            " ", clientDetails.ShippingCity, ", ", stateName.StateName);
                    }
                    else
                    {
                        var stateName = stateDetails.Where(v => v.StateCode == clientDetails.StateCode).SingleOrDefault();
                        lblShippingAddress.Text = String.Concat("Ship To: ", clientDetails.BillingAddress, ", ", clientDetails.Zip,
                                                            " ", clientDetails.City, ", ", stateName.StateName);
                    }

                    BindProductDropdown();
                    LoadTaxDropDown();
                }
            }
            else
            {
                cmbProduct.DataSource = null;
                cmbTax.DataSource = null;
                txtQuantity.Text = String.Empty;
                txtUnitPrice.Text = String.Empty;
                btnAdd.Enabled = false;
            }
        }

        private void chkDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiscount.Checked)
            {
                txtDiscount.Enabled = true;
                pnlDiscount.Visible = true;
            }
            else
            {
                txtDiscount.Text = String.Empty;
                txtDiscount.Enabled = false;
                pnlDiscount.Visible = false;
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateInvoice();
        }

        private void cbShippingnPackaging_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShippingnPackaging.Checked)
            {
                txtShippingnPackaging.Enabled = true;
                lblShippingAndPackValue.Text = String.Format("0.00");
                pnlShipping.Visible = true;
            }
            else
            {
                txtShippingnPackaging.Text = String.Empty;
                txtShippingnPackaging.Enabled = true;
                pnlShipping.Visible = false;
            }
        }

        private void txtShippingnPackaging_TextChanged(object sender, EventArgs e)
        {
            lblShippingAndPackValue.Text = txtShippingnPackaging.Text;
            CalculateInvoice();
        }

        private void chkMarkInvoicePaid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMarkInvoicePaid.Checked)
            {
                lblPaymentType.Visible = true;
                cmbPaymentType.Visible = true;
                lblAmountPaid.Visible = true;
                txtAmountPaid.Visible = true;
                lblNotes.Visible = true;
                txtNotes.Visible = true;
                cmbPaymentType.SelectedIndex = 0;
            }
            else
            {
                lblPaymentType.Visible = false;
                cmbPaymentType.Visible = false;
                lblAmountPaid.Visible = false;
                txtAmountPaid.Visible = false;
                lblNotes.Visible = false;
                txtNotes.Visible = false;
            }

            CalculateInvoice();
        }

        private void chkRoundOff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRoundOff.Checked)
            {
                pnlRoundedOff.Visible = true;

            }
            else
            {
                pnlRoundedOff.Visible = false;
                lblTotalValue.Text = Convert.ToString(TotalValue);
            }

            CalculateInvoice();
        }

        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1)
                return;

            if (dgvInvoice.Columns[e.ColumnIndex].Name == "Delete")
            {
                lstInvoiceProduct.RemoveAt(e.ColumnIndex - 1);
                BindInvoiceProductGrid();
            }
        }

        private void cmbPaymentTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dtIssueDate = Convert.ToDateTime(dtpIssueDate.Text);
            int days = 0;

            if (cmbPaymentTerms.SelectedIndex == 0)
            {
                dtpDueDate.Text = DateTime.Now.Date.ToShortDateString();
                dtpDueDate.Enabled = true;
            }
            else if (cmbPaymentTerms.SelectedIndex == 1)
            {
                dtpDueDate.Text = dtIssueDate.ToShortDateString();
                dtpDueDate.Enabled = false;
            }
            else
            {
                days = Convert.ToInt32(cmbPaymentTerms.SelectedValue);
                dtpDueDate.Text = dtIssueDate.Date.AddDays(days).ToShortDateString();
                dtpDueDate.Enabled = false;
            }
        }

        private void btnPreviewInvoice_Click_1(object sender, EventArgs e)
        {
            GenerateInvoicePDF();
            PreviewInvoice previewInvoice = new PreviewInvoice();
            previewInvoice.InvoiceId = String.IsNullOrEmpty(txtInvoiceNumber.Text) ? 0 : Convert.ToInt32(txtInvoiceNumber.Text);
            previewInvoice.InvoiceFolderPath = InvoiceFolderPath;
            previewInvoice.StartPosition = FormStartPosition.CenterParent;
            previewInvoice.ShowDialog(this);
        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            int invoiceId = string.IsNullOrEmpty(txtInvoiceNumber.Text) ? 0 : Convert.ToInt32(txtInvoiceNumber.Text);
            var invoiceDetails = invoiceService.GetInvoice(invoiceId);
            var comapnyDetails = (from company in companyService.GetAllCompany()
                                 .Where(v => v.Status == true)
                                  select company).SingleOrDefault();

            Invoice invoice = new Invoice();
            invoice.InvoiceId = invoiceId;
            invoice.CompanyId = comapnyDetails.CompanyId;
            invoice.ClientId = ClientId;
            invoice.IssueDate = string.IsNullOrEmpty(dtpIssueDate.Text) ? DateTime.Now.Date : Convert.ToDateTime(dtpIssueDate.Text).Date;
            invoice.PurchaseOrderNo = string.IsNullOrEmpty(txtPONumber.Text) ? String.Empty : txtPONumber.Text;
            invoice.PaymentTermId = Convert.ToInt32(cmbPaymentTerms.SelectedValue);
            invoice.DueDate = string.IsNullOrEmpty(dtpDueDate.Text) ? DateTime.Now.Date : Convert.ToDateTime(dtpDueDate.Text).Date;
            invoice.Discount = string.IsNullOrEmpty(txtDiscount.Text) ? decimal.Zero : Convert.ToDecimal(txtDiscount.Text);
            invoice.RoundOffTotal = chkRoundOff.Checked;
            invoice.MarkInvoicePaid = chkMarkInvoicePaid.Checked;
            invoice.PaymentTypeId = Convert.ToInt32(cmbPaymentType.SelectedValue);
            invoice.AmountPaid = string.IsNullOrEmpty(txtAmountPaid.Text) ? decimal.Zero : Convert.ToDecimal(txtAmountPaid.Text);
            invoice.Notes = string.IsNullOrEmpty(txtNotes.Text) ? String.Empty : txtNotes.Text;
            invoice.TotalAmount = TotalValue;
            invoice.NotesForClient = string.IsNullOrEmpty(txtNoteForClient.Text) ? String.Empty : txtNoteForClient.Text;
            invoice.PrivateNotes = string.IsNullOrEmpty(txtprivateNotes.Text) ? String.Empty : txtprivateNotes.Text;

            if (invoiceDetails == null)
            {
                invoiceService.AddInvoice(invoice);
            }
            else
            {
                invoiceService.UpdateInvoice(invoice);
            }

        }
    }
}
