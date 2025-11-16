// <copyright file="CalculatorForm.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

/// <summary>
///  Main form for Calculator
/// </summary>
namespace Calculator
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// <see cref="CalculatorForm"/>.
    /// </summary>
    public partial class CalculatorForm : Form
    {
        /// <summary>
        ///  <see cref="tokenazer"/>.
        /// </summary>
        private Tokenazer tokenazer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorForm"/> class
        /// </summary>
        public CalculatorForm()
        {
            this.InitializeComponent();
            this.tokenazer = new Tokenazer();
        }

        /// <summary>
        /// Event handler for any keys except 'C'
        /// </summary>
        /// <param name="sender">object of button</param>
        /// <param name="e">button arguments</param>
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string content = button.Text;
            char symbol = char.Parse(content);
            if (symbol == '=')
            {
                this.tokenazer.Complete();
            }
            else
            {
                this.tokenazer.AddSymbol(symbol);
            }

            this.RefreshView();
        }

        /// <summary>
        /// Event handler for C
        /// </summary>
        /// <param name="sender">object of button</param>
        /// <param name="e">button arguments</param>
        private void ButtonClick_C(object sender, EventArgs e)
        {
            this.tokenazer = new Tokenazer();
            var viewMidel = this.resultBox;
            viewMidel.Clear();
        }

        /// <summary>
        /// <see cref="RefreshView"/>.
        /// </summary>
        private void RefreshView()
        {
            var viewMidel = this.resultBox;
            string res = this.tokenazer.GetResult();
            viewMidel.Clear();
            viewMidel.Text = res;
        }
    }
}
