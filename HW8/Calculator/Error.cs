// <copyright file="Error.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace Calculator
{
    /// <summary>
    /// <see cref="Error"/>.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Error indicator
        /// </summary>
        private readonly bool isError;

        /// <summary>
        /// Error message
        /// </summary>
        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        public Error()
        {
            this.isError = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="message"> Error message </param>
        public Error(string message)
        {
            this.message = message;
            this.isError = true;
        }

        /// <summary>
        /// <see cref="GetMessage"> Return error message </see>
        /// </summary>
        /// <returns>return message of error</returns>
        public string GetMessage()
        {
            return this.message;
        }

        /// <summary>
        /// <see cref="GetStatus"/>.
        /// </summary>
        /// <returns>return error indicator</returns>
        public bool GetStatus()
        {
            return this.isError;
        }
    }
}
