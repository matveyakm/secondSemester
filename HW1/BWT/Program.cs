// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using BWT;

if (args.Length == 2)
{
    Console.WriteLine("Processing with parameters obtained from run settings.");
    Console.WriteLine("String to code: " + args[1]);
    var (resultString, originalStringIndex) = Bwt.Transform(args[1]);
    Console.WriteLine($"{resultString} {originalStringIndex}");
}
else if (args.Length == 3)
{
    Console.WriteLine("Processing with parameters obtained from run settings.");
    Console.WriteLine($"String to decode: {args[1]}  ({args[2]})");
    Console.WriteLine($"{Bwt.ReverseTransform(args[1], Convert.ToInt32(args[2]))}");
}
else
{
    Console.WriteLine("Use this keys to work with BWT:\n1 - Direct BWT\n2 - Reverse BWT\n3 - Exit");
    bool continueProcessing = true;
    while (continueProcessing)
    {
        switch (Console.ReadLine())
        {
            case "1":
            {
                Console.WriteLine("Enter a string: ");
                var inputString = Console.ReadLine();
                var (resultString, originalStringIndex) = Bwt.Transform(inputString);
                Console.WriteLine($"Result of direct BWT: {resultString} ({originalStringIndex})");
                break;
            }

            case "2":
            {
                Console.WriteLine("Enter a result of direct BWT (separated by space):");

                var input = Console.ReadLine();

                var bwtResult = input!.Split();
                if (bwtResult.Length != 2)
                {
                    Console.WriteLine("You can't add not 2 arguments");
                    return;
                }

                if (!int.TryParse(bwtResult[1], out var index))
                {
                    Console.WriteLine("Second variable should be number");
                    return;
                }

                Console.WriteLine($"Result of reverse transformation: {Bwt.ReverseTransform(bwtResult[0], index)}");
                break;
            }

            case "3":
            {
                continueProcessing = false;
                break;
            }

            default:
            {
                Console.WriteLine("Invalid input");
                break;
            }
        }
    }
}