// <copyright file="LzwDecompressorTests.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LZW.Tests;

using System.Text;

/// <summary>
/// test for LzwDecompressor.
/// </summary>
[TestFixture]
public class LzwDecompressorTests : LzwTest
{
    /// <summary>
    /// test for equality of files after compression and decompression.
    /// </summary>
    [Test]
    public void LzwDecompressor_Decompress_AfterCompression_RestoresOriginalFile()
    {
        const string originalContent = "This is a test string for LZW compression.";
        var originalFilePath = this.CreateTestFile(Encoding.UTF8.GetBytes(originalContent));
        var originalData = File.ReadAllBytes(originalFilePath);

        LzwCompressor.Compress(originalFilePath);
        var compressedFile = originalFilePath + ".zipped";

        LzwDecompressor.Decompress(compressedFile);

        var decompressedFile = compressedFile[..^7];
        Assert.That(File.Exists(decompressedFile), Is.True);

        var decompressedData = File.ReadAllBytes(decompressedFile);
        Assert.That(decompressedData, Is.EqualTo(originalData));
    }

    /// <summary>
    /// test for equality of byte files after compression and decompression.
    /// </summary>
    [Test]
    public void LzwDecompressor_Decompress_WithBinaryData_RestoresOriginalData()
    {
        // Arrange
        var originalData = new byte[] { 0x00, 0x01, 0x02, 0x00, 0x01, 0x02, 0x00, 0x01, 0x02 };
        var originalFilePath = this.CreateTestFile(originalData, "binary.bin");

        LzwCompressor.Compress(originalFilePath);
        var compressedFile = originalFilePath + ".zipped";

        LzwDecompressor.Decompress(compressedFile);

        var decompressedFile = compressedFile[..^7];
        var decompressedData = File.ReadAllBytes(decompressedFile);
        Assert.That(decompressedData, Is.EqualTo(originalData));
    }

    /// <summary>
    /// test equality of single character files after compression and decompression.
    /// </summary>
    [Test]
    public void LzwDecompressor_Decompress_WithSingleCharacterFile_RestoresCorrectly()
    {
        var originalData = new string('X', 50);
        var originalFilePath = this.CreateTestFile(Encoding.UTF8.GetBytes(originalData), "single_char.txt");

        LzwCompressor.Compress(originalFilePath);
        var compressedFile = originalFilePath + ".zipped";

        LzwDecompressor.Decompress(compressedFile);

        var decompressedFile = compressedFile[..^7];
        var decompressedData = File.ReadAllBytes(decompressedFile);
        Assert.That(decompressedData, Is.EqualTo(originalData));
    }

    /// <summary>
    /// test for throwing file not found exception while decompressing non-existing file.
    /// </summary>
    [Test]
    public void Decompress_NonExistentFile_ThrowsFileNotFoundException()
    {
        // Arrange
        var nonExistentFile = Path.Combine(this.testDirectory, "nonexistent.zipped");

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => LzwDecompressor.Decompress(nonExistentFile));
    }
}