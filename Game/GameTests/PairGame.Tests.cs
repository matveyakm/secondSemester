// <copyright file="GameLogicTests.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace PairGame.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class GameLogicTests
    {
        [Test]
        public void Constructor_InvalidN_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new GameLogic(1));
            Assert.Throws<ArgumentException>(() => new GameLogic(3));
            Assert.Throws<ArgumentException>(() => new GameLogic(14));
        }

        [Test]
        public void TotalPairs_IsCorrect()
        {
            var game = new GameLogic(4);
            Assert.That(game.TotalPairs, Is.EqualTo(8));
        }

        [Test]
        public void IsPair_ValidPair_ReturnsTrue()
        {
            var game = new GameLogic(2);
            Assert.That(game.N, Is.EqualTo(2));
        }

        [Test]
        public void GetValue_ReturnsValueInRange()
        {
            var game = new GameLogic(4);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int val = game.GetValue(i, j);
                    Assert.That(val, Is.InRange(0, 7));
                }
            }
        }

        [Test]
        public void AllNumbersAppearExactlyTwice()
        {
            var game = new GameLogic(6);
            var counts = new Dictionary<int, int>();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int val = game.GetValue(i, j);
                    counts[val] = counts.GetValueOrDefault(val) + 1;
                }
            }

            Assert.That(counts.Count, Is.EqualTo(18));
            Assert.That(counts.Values, Is.All.EqualTo(2));
        }
    }
}