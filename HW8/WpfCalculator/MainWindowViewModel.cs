// <copyright file="MainWindowViewModel.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace WpfCalculator
{
        using System.ComponentModel;

        /// <summary>
        /// <see cref="MainWindowViewModel"/>.
        /// </summary>
        public class MainWindowViewModel : INotifyPropertyChanged
        {
            private string localFormulaText = string.Empty;

            /// <inheritdoc/>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// <see cref="localFormulaText"/>Gets or sets .
            /// </summary>
            public string FormulaText
            {
                get => this.localFormulaText;
                set
                {
                    if (this.localFormulaText != value)
                    {
                        this.localFormulaText = value;
                        this.OnPropertyChanged(nameof(this.FormulaText));
                    }
                }
            }

            /// <summary>
            /// <see cref="UpdateFormulaText"/>.
            /// <param name="symbol"></param>
            /// </summary>
            public void UpdateFormulaText(string symbol)
            {
                if (symbol != "." && this.FormulaText == "0")
                {
                    this.FormulaText = string.Empty;
                }

                this.FormulaText += symbol;
            }

            /// <summary>
            /// <see cref="ClearAll"/>.
            /// </summary>
            public void ClearAll()
            {
                this.FormulaText = string.Empty;
            }

            /// <summary>
            /// <see cref="OnPropertyChanged"/>.
            /// <param name="propertyName"></param>
            /// </summary>
            protected void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

}
