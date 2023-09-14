using System;
using System.Windows.Forms;

namespace CSC2036_InvoiceTotal_ErrorCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const decimal SalesTaxPct = 7.75m;

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                decimal productTotal = Convert.ToDecimal(txtProductTotal.Text);
                // More code here

                decimal discountPercent = .0m;

                if (productTotal < 100)
                    discountPercent = .0m;
                else if (productTotal >= 100 && productTotal < 250)
                    discountPercent = .1m;
                else if (productTotal >= 250)
                    discountPercent = .25m;

                decimal discountAmount = productTotal * discountPercent;
                decimal subtotal = productTotal - discountAmount;
                decimal tax = subtotal * SalesTaxPct / 100;
                decimal total = subtotal + tax;

                txtDiscountAmount.Text = discountAmount.ToString("c");
                txtSubtotal.Text = subtotal.ToString("c");
                txtTax.Text = tax.ToString("c");
                txtTotal.Text = total.ToString("c");

                txtProductTotal.Focus();
            }
        }

        public bool IsValidData()
        {
            // Note the return statement returns a boolean expression with 3 AND cases
            return
                IsPresent(txtProductTotal, "Product total") &&
                IsDecimal(txtProductTotal, "Product total") &&
                IsWithinRange(txtProductTotal, "Product total", 0, 1000000);
        }

        public bool IsPresent(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public bool IsDecimal(TextBox textBox, string name)
        {
            decimal number = 0m;
            if (Decimal.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(name + " must be a decimal number.", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        public bool IsWithinRange(TextBox textBox, string name,
            decimal min, decimal max)
        {
            decimal number = Convert.ToDecimal(textBox.Text);
            if (number <= min || number >= max)
            {
                MessageBox.Show(name + " must be between " + min +
                    " and " + max + ".", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
