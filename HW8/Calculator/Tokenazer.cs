// <copyright file="Tokenazer.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace Calculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// <see cref="Tokenazer"/>.
    /// </summary>
    public class Tokenazer
    {
        private List<Token> tokens;
        private State state;
        private string buffer = string.Empty;
        private Token.Type bufferTokenType;
        private Error error;
        private bool isPoint;
        private bool isOperator;
        private bool isDigit;
        private bool isLetter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tokenazer"/> class.
        /// </summary>
        public Tokenazer()
        {
            this.error = new Error();
            this.tokens = new List<Token>();
            this.bufferTokenType = Token.Type.INT_LITERAL;
            this.state = State.S0;
        }

        /*
        S0 - Стартовое
        S1 - Токенизация оператора
        S2 - Запись целого числа
        S3 - Запись числа с плавующей точкой
        S4 - Токенизация записанного числа
        */
        private enum State
        {
            S0,
            S1,
            S2,
            S3,
            S4,
        }

        /// <summary>
        /// <see cref="AddSymbol"/>.
        /// </summary>
        /// <param name="symbol">symbol for processing</param>
        public void AddSymbol(char symbol)
        {
            string validOperators = "+-*/^";
            this.isDigit = char.IsDigit(symbol);
            this.isLetter = char.IsLetter(symbol);
            this.isPoint = symbol == '.';
            this.isOperator = validOperators.Contains(symbol);

            if (!(this.isDigit || this.isLetter || this.isPoint || this.isOperator))
            {
                this.error = new Error("Unknown symbol");
            }

            switch (this.state)
            {
                case State.S0:
                    if (this.isOperator)
                    {
                        this.state = State.S1;
                    }
                    else if (this.isDigit)
                    {
                        this.state = State.S2;
                    }
                    else if (this.isPoint)
                    {
                        this.error = new Error("Unexpected symbol:" + symbol);
                    }

                    break;
                case State.S1:
                    if (this.isDigit)
                    {
                        this.state = State.S2;
                    }
                    else if (this.isLetter)
                    {
                        this.state = State.S4;
                    }
                    else if (this.isPoint)
                    {
                        this.error = new Error("Unexpected symbol:" + symbol);
                    }

                    break;
                case State.S2:
                    this.bufferTokenType = Token.Type.INT_LITERAL;
                    if (this.isPoint)
                    {
                        this.state = State.S3;
                    }
                    else if (this.isOperator)
                    {
                        this.state = State.S4;
                    }
                    else if (this.isLetter)
                    {
                        this.error = new Error("Unexpected symbol:" + symbol);
                    }

                    break;
                case State.S3:
                    this.bufferTokenType = Token.Type.FLOAT_LITERAL;
                    if (this.isOperator)
                    {
                        this.state = State.S4;
                    }
                    else if (this.isPoint)
                    {
                        this.error = new Error("Unexpected symbol:" + symbol);
                    }

                    break;
                case State.S4:
                    if (this.isOperator)
                    {
                        this.state = State.S1;
                    }
                    else if (this.isDigit)
                    {
                        this.state = State.S2;
                    }
                    else if (this.isPoint || this.isOperator)
                    {
                        this.error = new Error("Unexpected symbol:" + symbol);
                    }

                    break;
                default:
                    break;
            }

            switch (this.state)
            {
                case State.S1:
                    this.Tokenize(symbol.ToString());
                    break;
                case State.S2:
                case State.S3:
                    this.buffer += symbol;
                    break;
                case State.S4:
                    this.tokens.Add(new Token(this.buffer, this.bufferTokenType));
                    this.buffer = string.Empty;
                    this.Culculate();
                    this.Tokenize(symbol.ToString());
                    break;
            }
        }

        /// <summary>
        /// <see cref="Complite"/>.
        /// </summary>
        public void Complete()
        {
            if (this.buffer.Length > 0)
            {
                this.tokens.Add(new Token(this.buffer, this.bufferTokenType));
                this.buffer = string.Empty;
                this.Culculate();
                this.state = State.S1;
            }
        }

        /// <summary>
        /// <see cref="GetResult"/>.
        /// </summary>
        /// <returns>Return result processing</returns>
        public string GetResult()
        {
            if (this.error.GetStatus())
            {
                this.tokens.Clear();
                return this.error.GetMessage();
            }

            string res = string.Empty;
            foreach (Token token in this.tokens)
            {
                res += token.GetStrValue();
            }

            return res + this.buffer;
        }

        /// <summary>
        /// Token processing
        /// </summary>
        /// <param name="symbols">symbol for token</param>
        private void Tokenize(string symbols)
        {
            if (this.isOperator)
            {
                this.tokens.Add(new Token(symbols, Token.Type.OPERATOR));
            }
        }

        /// <summary>
        /// Calculate the math operations +,-,*,/,^
        /// </summary>
        private void Culculate()
        {
            double res = 0f;
            int index = this.tokens.FindIndex(x => x.GetTokenType() == Token.Type.OPERATOR);
            if (index > 0)
            {
                var strToken = this.tokens.ElementAt(index).GetStrValue();
                this.tokens.Remove(this.tokens.ElementAt(index));
                var item1 = double.Parse(this.tokens.ElementAt(0).GetStrValue());
                var item2 = double.Parse(this.tokens.ElementAt(1).GetStrValue());

                if (strToken == "+")
                {
                    res = item1 + item2;
                }
                else if (strToken == "-")
                {
                    res = item1 - item2;
                }
                else if (strToken == "*")
                {
                    res = item1 * item2;
                }
                else if (strToken == "/")
                {
                    res = this.Division(item1, item2);
                }
                else if (strToken == "^")
                {
                    res = Math.Pow(item1, item2);
                }

                this.tokens.Clear();
                this.tokens.Add(new Token(res.ToString(), Token.Type.INT_LITERAL));
            }
        }

        /// <summary>
        /// Division processing
        /// </summary>
        /// <param name="x">dividend of division</param>
        /// <param name="y">divider of division</param>
        /// <returns>return result of division</returns>
        private double Division(double x, double y)
        {
            double z = 0f;
            if (y == 0f)
            {
                this.error = new Error("Division by zero!");
            }
            else
            {
                z = x / y;
            }

            return z;
        }
    }
}