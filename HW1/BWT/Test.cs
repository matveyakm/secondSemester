namespace BWT;

/// <summary>
/// tests for BWT.
/// </summary>
public class Test
{
    /// <summary>
    /// complete all tests.
    /// </summary>
    /// <returns>true if all tests were completed.</returns>
    public static bool TestsComplete()
    {
        string[][] testInputs =
        [
            ["HELLOWORLD", "LHDRELWLOO", "3"],
            ["привет,мир", "тиврм,рпие", "7"],
            ["banana", "nnbaaa", "4"],
            ["just_a_test", "ta_ttues_sj", "5"]
        ];

        for (var i = 0; i < testInputs.Length; ++i)
        {
            bool isTransformationCorrect = Bwt.Transform(testInputs[i][0]) == (testInputs[i][1], Convert.ToInt32(testInputs[i][2]));
            bool isReverseTransformationCorrect = Bwt.ReverseTransform(testInputs[i][1], Convert.ToInt32(testInputs[i][2])) == testInputs[i][0];
            if (!(isTransformationCorrect && isReverseTransformationCorrect))
            {
                Console.WriteLine($"Test {i + 1} failed");
                return false;
            }
        }

        return true;
    }
}