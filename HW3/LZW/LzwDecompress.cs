// <copyright file="LzwDecompress.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LZW;

/// <summary>
/// to decompress file.
/// </summary>
public class LzwDecompress
{
    /// <summary>
    /// to decompress file.
    /// </summary>
    /// <param name="filePath">file to decompress.</param>
    public static void Decompress(string filePath)
    {
        var data = File.ReadAllBytes(filePath);
        var codes = ConvertByteStreamToIntValues(data);
        var decompressedData = Decode(codes);

        var decompressedFilePath = filePath[..^7];
        File.WriteAllBytes(decompressedFilePath, decompressedData);
    }

    /// <summary>
    /// to convert byte sequence to int array of codes.
    /// </summary>
    /// <param name="inputBytes">input sequence to convert.</param>
    /// <returns>int array of codes.</returns>
    public static int[] ConvertByteStreamToIntValues(byte[] inputBytes)
    {
        var outputValues = new List<int>();
        var currentValue = 0;
        var bitOffset = 0;

        foreach (var currentByte in inputBytes)
        {
            currentValue |= (currentByte & 0x7F) << bitOffset;

            if ((currentByte & 0x80) == 0)
            {
                outputValues.Add(currentValue);
                currentValue = 0;
                bitOffset = 0;
            }
            else
            {
                bitOffset += 7;
            }
        }

        return outputValues.ToArray();
    }

    /// <summary>
    /// to decode data.
    /// </summary>
    /// <param name="inputCodes">array of codes to decode.</param>
    /// <returns>byte sequence of codes.</returns>
    public static byte[] Decode(int[] inputCodes)
    {
        var dictionary = new Dictionary<int, List<byte>>();
        var decompressed = new List<byte>();
        var dictCounter = 256;

        for (var i = 0; i < 256; i++)
        {
            dictionary[i] = [(byte)i];
        }

        var previousCode = inputCodes[0];
        decompressed.AddRange(dictionary[previousCode]);

        for (var i = 1; i < inputCodes.Length; i++)
        {
            var currentCode = inputCodes[i];
            List<byte> currentBytes;

            if (currentCode == dictCounter)
            {
                currentBytes = [..dictionary[previousCode], dictionary[previousCode][0]];
            }
            else if (dictionary.TryGetValue(currentCode, out var value))
            {
                currentBytes = new List<byte>(value);
            }
            else
            {
                throw new InvalidOperationException("Incorrect code in compressed data");
            }

            decompressed.AddRange(currentBytes);

            var newSequence = new List<byte>(dictionary[previousCode])
            {
                currentBytes[0],
            };
            dictionary[dictCounter] = newSequence;

            dictCounter++;
            previousCode = currentCode;
        }

        return decompressed.ToArray();
    }
}