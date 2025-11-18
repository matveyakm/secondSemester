// <copyright file="Token.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace Calculator
{
    /// <summary>
    /// <see cref="Token"/>.
    /// </summary>
    internal class Token
    {
        private readonly Error error;
        private string strValue;
        private Type tokenType;

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param p="symbols">symbol of token</param>
        /// <param name="type">type of token</param>
        public Token(string symbols, Type type)
        {
            this.strValue = symbols;
            this.tokenType = type;
            this.error = new Error();
        }

        /// <summary>
        /// <see cref="Type">Тип токена</see>.
        /// OPERATOR         - бинарный оператор
        /// INT_LITERAL      - целое число
        /// FLOAT_LITERAL    - число с плавующей точкой
        /// </summary>
        public enum Type
        {
            OPERATOR,
            INT_LITERAL,
            FLOAT_LITERAL,
        }

        /// <summary>
        /// <see cref="GetTokenType"/>.
        /// </summary>
        /// <returns>Return token type</returns>
        public Type GetTokenType()
        {
            return this.tokenType;
        }

        /// <summary>
        /// <see cref="GetStrValue"/>.
        /// </summary>
        /// <returns>Return token value</returns>
        public string GetStrValue()
        {
            return this.strValue;
        }
    }
}