// <copyright file="BwtTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BWT.Test;

/// <summary>
/// NUnit-based tests for BWT.
/// </summary>
[TestFixture]
public class BwtTest
{
    /// <summary>
    /// Test-data: [original, BWT, index]
    /// </summary>
    private static readonly string[][] TestCases =
    {
        ["HELLOWORLD", "LHDRELWLOO", "3"],
        ["привет,мир", "тиврм,рпие", "7"],
        ["banana", "nnbaaa", "4"],
        ["just_a_test", "ta_ttues_sj", "5"]
    };

    [Test]
    public void AllTestCasesPass()
    {
        for (int i = 0; i < TestCases.Length; i++)
        {
            string original = TestCases[i][0];
            string expectedBwt = TestCases[i][1];
            int expectedIndex = int.Parse(TestCases[i][2]);

            var (actualBwt, actualIndex) = Bwt.Transform(original);
            Assert.That(actualBwt, Is.EqualTo(expectedBwt), $"BWT failed for input '{original}'");
            Assert.That(actualIndex, Is.EqualTo(expectedIndex), $"Index mismatch for input '{original}'");

            string decoded = Bwt.ReverseTransform(expectedBwt, expectedIndex);
            Assert.That(decoded, Is.EqualTo(original), $"ReverseTransform failed for input '{original}'");
        }
    }
}
