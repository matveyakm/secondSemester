// <copyright file="LzwCompressorTests.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LZW.Tests;

using System.Text;

/// <summary>
/// tests for LzwCompressor.
/// </summary>
[TestFixture]
public class LzwCompressorTests : LzwTest
{
    /// <summary>
    /// test for valid compression ratio.
    /// </summary>
    [Test]
    public void LzwCompressor_Compress_WithTextData_ReturnsValidCompressionRatio()
    {
        const string testData = "ABABABABABABABAB";
        var filePath = this.CreateTestFile(Encoding.UTF8.GetBytes(testData));
        var originalSize = new FileInfo(filePath).Length;

        var ratio = LzwCompressor.Compress(filePath);

        var compressedFile = filePath + ".zipped";
        Assert.That(File.Exists(compressedFile), Is.True);

        var compressedSize = new FileInfo(compressedFile).Length;
        Assert.That(ratio, Is.EqualTo((float)originalSize / compressedSize));
        Assert.That(ratio, Is.GreaterThan(1.0f));
    }

    /// <summary>
    /// test for valid compress with single character.
    /// </summary>
    [Test]
    public void LzwCompressor_Compress_WithSingleCharacter_ReturnsCompressedFile()
    {
        var testData = new string('A', 100);
        var filePath = this.CreateTestFile(Encoding.UTF8.GetBytes(testData));

        var ratio = LzwCompressor.Compress(filePath);

        var compressedFile = filePath + ".zipped";
        Assert.Multiple(() =>
        {
            Assert.That(File.Exists(compressedFile), Is.True);
            Assert.That(ratio, Is.GreaterThan(1.0f));
        });
    }

    /// <summary>
    /// test for valid compress with random bytes.
    /// </summary>
    [Test]
    public void LzwCompressor_Compress_WithRandomData_CreatesCompressedFile()
    {
        var random = new Random();
        var randomData = new byte[1000];
        random.NextBytes(randomData);
        var filePath = this.CreateTestFile(randomData, "random.bin");

        var ratio = LzwCompressor.Compress(filePath);

        var compressedFile = filePath + ".zipped";
        Assert.Multiple(() =>
        {
            Assert.That(File.Exists(compressedFile), Is.True);
            Assert.That(ratio, Is.GreaterThan(0));
        });
    }

    /// <summary>
    /// test for throwing file not found exception while compress non-existent file.
    /// </summary>
    [Test]
    public void LzwCompressor_Compress_WithNonExistentFile_ThrowsException()
    {
        // Arrange
        var nonExistentFile = Path.Combine(this.testDirectory, "nonexistent.txt");

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => LzwCompressor.Compress(nonExistentFile));
    }
}