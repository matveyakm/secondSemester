// <copyright file="GameLogic.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace PairGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Core game logic for the Memory Game (Find the Pair).
    /// </summary>
    public class GameLogic
    {
        private readonly int n;
        private readonly int[,] values;
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="n">Grid size (must be even, n is between 2 and 12).</param>
        /// <exception cref="ArgumentException">Thrown if N is invalid.</exception>
        public GameLogic(int n)
        {
            if (n % 2 != 0 || n < 2 || n > 12)
            {
                throw new ArgumentException("N must be even number between 2 and 12", nameof(n));
            }

            this.n = n;
            this.values = new int[n, n];
            this.random = new Random();
            this.InitializeValues();
        }

        /// <summary>
        /// Gets the grid size N.
        /// </summary>
        public int N => this.n;

        /// <summary>
        /// Gets the total number of pairs.
        /// </summary>
        public int TotalPairs => (this.n * this.n) / 2;

        /// <summary>
        /// Gets the value at the specified position.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        /// <returns>The number at (row, col).</returns>
        public int GetValue(int row, int col) => this.values[row, col];

        /// <summary>
        /// Checks if two positions form a valid pair.
        /// </summary>
        /// <param name="row1">First row.</param>
        /// <param name="col1">First column.</param>
        /// <param name="row2">Second row.</param>
        /// <param name="col2">Second column.</param>
        /// <returns>True if values match and positions are different.</returns>
        public bool IsPair(int row1, int col1, int row2, int col2)
        {
            return this.values[row1, col1] == this.values[row2, col2]
                && (row1 != row2 || col1 != col2);
        }

        /// <summary>
        /// Initializes and shuffles the values grid.
        /// </summary>
        private void InitializeValues()
        {
            List<int> numbers = new List<int>();
            int totalPairs = this.TotalPairs;
            for (int i = 0; i < totalPairs; i++)
            {
                numbers.Add(i);
                numbers.Add(i);
            }

            for (int i = numbers.Count - 1; i > 0; i--)
            {
                int j = this.random.Next(i + 1);
                (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
            }

            int index = 0;
            for (int i = 0; i < this.n; i++)
            {
                for (int j = 0; j < this.n; j++)
                {
                    this.values[i, j] = numbers[index++];
                }
            }
        }
    }
}