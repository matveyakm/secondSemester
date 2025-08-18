// <copyright file="LzwTest.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LZW.Tests;

using System.Text;
using LZW;

/// <summary>
/// tests for LZW.
/// </summary>
public class LzwTest
{
    /// <summary>
    /// test that data equal after compress and decompress.
    /// </summary>
    [Test]
    public void LZW_DataAfterCompressAndDecompress_ShouldEqual()
    {
        var testData = Encoding.ASCII.GetBytes("fsjgnfisgns ihnxwueihx8behremx0rgmwgwzemg");

        var codes = LzwCompress.Encode(testData);
        var compressedData = LzwCompress.ConvertIntArrayToByteStream(codes);

        var codesAfterCompress = LzwDecompress.ConvertByteStreamToIntValues(compressedData);
        var decompressData = LzwDecompress.Decode(codesAfterCompress);

        Assert.That(testData, Is.EqualTo(decompressData));
    }

    /// <summary>
    /// test for correct encoding array of bytes.
    /// </summary>
    [Test]
    public void LzwCompress_Encode_NormalArrayOfBytes()
    {
        var testData = new byte[256];
        var expectedResult = new int[256];
        for (var i = 0; i < 256; ++i)
        {
            testData[i] = (byte)i;
            expectedResult[i] = i;
        }

        var codes = LzwCompress.Encode(testData);

        Assert.That(codes, Is.EqualTo(expectedResult));
    }

    /// <summary>
    /// test for correct encoding array of repeating bytes.
    /// </summary>
    [Test]
    public void LzwCompress_Encode_ArrayOfRepeatingBytes()
    {
        var testData = Encoding.ASCII.GetBytes("kkkkkkkkkrrrrrmmmmmm");
        int[] expectedResult = [107, 256, 257, 257, 114, 260, 260, 109, 263, 264];
        var codes = LzwCompress.Encode(testData);

        Assert.That(codes, Is.EqualTo(expectedResult));
    }

    /// <summary>
    /// test for correct byte sequence to int array.
    /// </summary>
    [Test]
    public void LZWDecode_TransformByteSequenceToIntArray_IntArray()
    {
        byte[] testData = [236, 167, 3, 137, 153, 6, 251, 89, 161, 190, 1, 251, 251, 1, 190, 9, 233, 11, 235, 104];
        int[] expectedResult = [54252, 101513, 11515, 24353, 32251, 1214, 1513, 13419];

        var transformedData = LzwDecompress.ConvertByteStreamToIntValues(testData);

        Assert.That(transformedData, Is.EqualTo(expectedResult));
    }

    /// <summary>
    /// test for correct transform int array to byte sequence.
    /// </summary>
    [Test]
    public void LzwCompress_TransformIntArrayToByteSequence_ArrayOfBytes()
    {
        int[] testData = [242342, 3252, 5262, 4621, 7532, 623, 87654, 4351];
        byte[] expectedResult = [166, 229, 14, 180, 25, 142, 41, 141, 36, 236, 58, 239, 4, 230, 172, 5, 255, 33];

        var transformedData = LzwCompress.ConvertIntArrayToByteStream(testData);

        Assert.That(transformedData, Is.EqualTo(expectedResult));
    }
}