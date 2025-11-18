// <copyright file="LzwCompressor.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

namespace LZW;

/// <summary>
/// to compress file.
/// </summary>
public static class LzwCompressor
{
    /// <summary>
    /// to compress file.
    /// </summary>
    /// <param name="filePath">file to compress.</param>
    /// <returns>compression ratio.</returns>
    public static float Compress(string filePath)
    {
        var data = File.ReadAllBytes(filePath);
        var fileLength = data.Length;

        var codesOfFileData = Encode(data);

        var compressedData = ConvertIntArrayToByteStream(codesOfFileData);
        var compressedFileLength = compressedData.Length;

        var compressedFilePath = filePath + ".zipped";
        File.WriteAllBytes(compressedFilePath, compressedData);

        return (float)fileLength / compressedFileLength;
    }

    /// <summary>
    /// to encode byte sequence.
    /// </summary>
    /// <param name="inputBytes">byte sequence to encode.</param>
    /// <returns>array of codes.</returns>
    private static int[] Encode(byte[] inputBytes)
    {
        var dictionary = new Bor();
        var nextCode = dictionary.WordCount;

        var buffer = new List<byte> { inputBytes[0] };
        var resultCodes = new List<int>();

        for (var i = 1; i < inputBytes.Length; i++)
        {
            var currentByte = inputBytes[i];
            var extendedSequence = new List<byte>(buffer) { currentByte };

            if (dictionary.Contains(extendedSequence) != -1)
            {
                buffer = extendedSequence;
            }
            else
            {
                resultCodes.Add(dictionary.Contains(buffer));

                dictionary.Add(extendedSequence, nextCode);
                nextCode++;

                buffer = [currentByte];
            }
        }

        resultCodes.Add(dictionary.Contains(buffer));

        return resultCodes.ToArray();
    }

    /// <summary>
    /// convert int array to byte sequence.
    /// </summary>
    /// <param name="inputData">data to convert.</param>
    /// <returns>byte sequence.</returns>
    private static byte[] ConvertIntArrayToByteStream(int[] inputData)
    {
        var byteCollection = new List<byte>();

        for (var i = 0; i < inputData.Length; i++)
        {
            long current = inputData[i];
            bool moreBytes;

            do
            {
                var temp = (byte)(current & 0x7F);
                current >>= 7;
                moreBytes = current != 0;

                if (moreBytes)
                {
                    temp |= 0x80;
                }

                byteCollection.Add(temp);
            }
            while (moreBytes);
        }

        return byteCollection.ToArray();
    }
}