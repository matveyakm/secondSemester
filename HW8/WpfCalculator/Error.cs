// <copyright file="Error.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace WpfCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// <see cref="Error"/>.
    /// </summary>
    public class Error
    {
        private string message;
        private bool isError;

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        public Error()
        {
            this.isError = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// <param name="message"></param>
        /// </summary>
        public Error(string message)
        {
            this.message = message;
            this.isError = true;
        }

        /// <summary>
        /// <see cref="GetMessage"/>.
        /// </summary>
        /// <returns>string.</returns>
        public string GetMessage()
        {
            return this.message;
        }

        /// <summary>
        /// <see cref="GetStatus"/>.
        /// </summary>
        /// <returns>bool.</returns>
        public bool GetStatus()
        {
            return this.isError;
        }
    }
}
