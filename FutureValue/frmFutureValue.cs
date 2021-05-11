//Mohammad Jokar-Konavi 
//May 10, 2021
//Exercise 7-2


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FutureValue
{
    public partial class frmFutureValue : Form
    {
        public frmFutureValue()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                //Using the IsValidData method for data validation
                if (this.IsValidData())
                {
                    decimal monthlyInvestment =
                        Convert.ToDecimal(txtMonthlyInvestment.Text);
                    decimal yearlyInterestRate =
                        Convert.ToDecimal(txtInterestRate.Text);
                    int years = Convert.ToInt32(txtYears.Text);

                    int months = years * 12;
                    decimal monthlyInterestRate = yearlyInterestRate / 12 / 100;

                    decimal futureValue = this.CalculateFutureValue(
                        monthlyInvestment, monthlyInterestRate, months);

                    txtFutureValue.Text = futureValue.ToString("c");
                    txtMonthlyInvestment.Focus();

                }
            }


            //catch (FormatException)  //It handles any FormatException
            //{
            //    MessageBox.Show(
            //        "Invalid values. Please check all entries.",
            //        "Entry Error");
            //}
            //catch (OverflowException)   //It handles any OverflowException
            //{
            //    MessageBox.Show(
            //        "Overflow error. Please enter smaller values.",
            //        "Entry Error");
            //}
            catch (Exception ex)   // It handles any other exception
            {
                MessageBox.Show(
                    ex.Message + "\n\n" +
                    ex.GetType().ToString() + "\n" +
                    ex.StackTrace, "Exception");
            }
        }


        // This method calls the three other validation methods
        private bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";

            // Validating the monthly investment text box
            errorMessage += this.IsDecimal(txtMonthlyInvestment.Text, txtMonthlyInvestment.Tag.ToString());
            errorMessage += this.IsWithinRange(txtMonthlyInvestment.Text,
                                          txtMonthlyInvestment.Tag.ToString(),
                                          1, 1000);

            // Validating the yearly interest rate text box
            errorMessage += this.IsDecimal(txtInterestRate.Text,
                                      txtInterestRate.Tag.ToString());
            errorMessage += this.IsWithinRange(txtInterestRate.Text,
                                          txtInterestRate.Tag.ToString(), 1, 20);

            // Validating the number of years text box
            errorMessage += this.IsInt32(txtYears.Text, txtYears.Tag.ToString());
            errorMessage += this.IsWithinRange(txtYears.Text, txtYears.Tag.ToString(),
                                          1, 40);

            if (errorMessage != "")
            {
                success = false;
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }


        // Validation method for monthly investment & interest rate text boxes
        private string IsDecimal(string value, string name)
        {
            string msg = "";
            if (!Decimal.TryParse(value, out _))
            {
                msg += name + " must be a valid decimal value.\n";
            }
            return msg;
        }


        //Validation method for number of years text box
        private string IsInt32(string value, string name)
        {
            string msg = "";
            if (!Int32.TryParse(value, out _))
            {
                msg += name + " must be a valid integer value.\n";
            }
            return msg;
        }


        //Validation method for the range of all values
        private string IsWithinRange(string value, string name, decimal min,
            decimal max)
        {
            string msg = "";
            if (Decimal.TryParse(value, out decimal number))
            {
                if (number < min || number > max)
                {
                    msg += name + " must be between " + min + " and " + max + ".\n";
                }
            }
            return msg;
        }

        private decimal CalculateFutureValue(decimal monthlyInvestment,
           decimal monthlyInterestRate, int months)
        {
            decimal futureValue = 0m;
            for (int i = 0; i < months; i++)
            {
                futureValue = (futureValue + monthlyInvestment)
                    * (1 + monthlyInterestRate);
            }
            //// A throw statement added
            //throw new Exception("An unknown error");

            return futureValue;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
