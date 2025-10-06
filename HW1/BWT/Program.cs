// <copyright file="Program.cs" company="matveyakm">
// Copyright (c) matveyakm. All rights reserved.
// </copyright>

using BWT;

// Write dotnet run -- '1 word' if you want transform word / '2 word position' - if you want reverse transform
switch (args[0])
{
    case "1":
    {
        var answer = Bwt.Transform(args[1]);
        Console.WriteLine($"{answer.TransformWord}: {answer.Position}");
        break;
    }

    case "2":
    {
        var answer = Bwt.ReverseTransform(args[1], Convert.ToInt32(args[2]));
        Console.WriteLine($"{answer}");
        break;
    }

    default:
    {
        Console.WriteLine("Unknown character.");
        break;
    }
}