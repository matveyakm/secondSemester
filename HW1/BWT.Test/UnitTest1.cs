using NUnit.Framework;

namespace BWT.Tests;

/// <summary>
/// NUnit-based tests for BWT.
/// </summary>
[TestFixture]
public class BWTTests
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

            var (actualBwt, actualIndex) = BWT.Transform(original);
            Assert.That(actualBwt, Is.EqualTo(expectedBwt), $"BWT failed for input '{original}'");
            Assert.That(actualIndex, Is.EqualTo(expectedIndex), $"Index mismatch for input '{original}'");

            string decoded = BWT.ReverseTransform(expectedBwt, expectedIndex);
            Assert.That(decoded, Is.EqualTo(original), $"ReverseTransform failed for input '{original}'");
        }
    }
}
