// <copyright file="Program.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

using LZW;

// Write dotnet run -- -c/-u FileName
// -c - if you want to compress file, -u - to decompress
if (string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]) || args.Length < 2)
{
    Console.WriteLine("Error when passing arguments");
    return;
}

switch (args[0])
{
    case "-c":
    {
        var compressionRatio = LzwCompressor.Compress(args[1]);
        Console.WriteLine("Compression ratio: " + compressionRatio);
        break;
    }

    case "-u":
    {
        LzwDecompressor.Decompress(args[1]);
        break;
    }

    default:
    {
        Console.WriteLine("Incorrect symbol");
        break;
    }
}