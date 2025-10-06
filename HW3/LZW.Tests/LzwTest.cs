// <copyright file="LzwTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LZW.Tests;

using System.Text;
using LZW;

/// <summary>
/// settings for lzw tests.
/// </summary>
[TestFixture]
public class LzwTest
{
    /// <summary>
    /// directory with test file.
    /// </summary>
    protected string testDirectory;

    /// <summary>
    /// Assert file are equal.
    /// </summary>
    /// <param name="expectedFile">expected file.</param>
    /// <param name="actualFile">file.</param>
    protected static void AssertFilesAreEqual(string expectedFile, string actualFile)
    {
        Assert.Multiple(() =>
        {
            Assert.That(File.Exists(expectedFile), Is.True);
            Assert.That(File.Exists(actualFile), Is.True);
        });

        var expectedSize = new FileInfo(expectedFile).Length;
        var actualSize = new FileInfo(actualFile).Length;

        Assert.That(actualSize, Is.EqualTo(expectedSize));

        var expectedData = File.ReadAllBytes(expectedFile);
        var actualData = File.ReadAllBytes(actualFile);
        Assert.That(actualData, Is.EqualTo(expectedData));
    }

    /// <summary>
    /// to initialize test directory.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        this.testDirectory = Path.Combine(Path.GetTempPath(), "LzwTest_" + Guid.NewGuid());
        Directory.CreateDirectory(this.testDirectory);
    }

    /// <summary>
    /// to delete temporary directory.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(this.testDirectory))
        {
            Directory.Delete(this.testDirectory, true);
        }
    }

    /// <summary>
    /// to create new file.
    /// </summary>
    /// <param name="data">data to write in file.</param>
    /// <param name="fileName">file name.</param>
    /// <returns>filepath.</returns>
    protected string CreateTestFile(byte[] data, string fileName = "test.txt")
    {
        var filePath = Path.Combine(this.testDirectory, fileName);
        File.WriteAllBytes(filePath, data);
        return filePath;
    }


}