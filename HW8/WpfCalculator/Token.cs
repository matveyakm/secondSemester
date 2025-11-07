// <copyright file="Token.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace WpfCalculator
{
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="Token"/>.
    /// </summary>
    internal class Token
    {
        private string strValue;
        private Type tokenType;
        private Error error;


        /// <summary>
        /// <see cref="Type">Тип токена</see>.
        /// OPERATOR         - бинарный оператор
        /// LEFT_PARANTHESIS - левая скобка
        /// RIGHT_PARANTHESIS- праввая скобка
        /// INT_LITERAL      - целое число
        /// FLOAT_LITERAL    - число с плавующей точкой
        /// FUNCTION         - функция
        /// SEPARATOR        - разделитель аргументов функции.
        /// </summary>
        public enum Type
        {
            OPERATOR,
            INT_LITERAL,
            FLOAT_LITERAL,
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// <param p="symbols"></param>
        /// <param name="type"></param>
        /// <param name="operatorAssociativity"></param>
        /// </summary>
        public Token(string symbols, Type type)
        {
            this.strValue = symbols;
            this.tokenType = type;
            this.error = new Error();
        }


        /// <summary>
        /// <see cref="GetTokenType"/>.
        /// </summary>
        /// <returns>Type</returns>
        public Type GetTokenType()
        {
            return this.tokenType;
        }

        /// <summary>
        /// <see cref="GetStrValue"/>.
        /// </summary>
        /// <returns>string.</returns>
        public string GetStrValue()
        {
            return this.strValue;
        }
    }
}