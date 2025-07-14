namespace BWT;

internal class Program
{
    private static void Main(string[] args)
    {
        if (!Test.TestsComplete())
        {
            Console.WriteLine("Something went wrong.");
            return;
        }

        if (args.Length == 2)
        {
            Console.WriteLine("Processing with parameters obtained from run settings.");
            Console.WriteLine("String to code: " + args[1]);
            var (resultString, originalStringIndex) = BWT.Transform(args[1]);
            Console.WriteLine($"{resultString} {originalStringIndex}");
        }
        else if (args.Length == 3)
        {
            Console.WriteLine("Processing with parameters obtained from run settings.");
            Console.WriteLine($"String to decode: {args[1]}  ({args[2]})");
            Console.WriteLine($"{BWT.ReverseTransform(args[1], Convert.ToInt32(args[2]))}");
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
                            var (resultString, originalStringIndex) = BWT.Transform(inputString);
                            Console.WriteLine($"Result of direct BWT: {resultString} ({originalStringIndex})");
                            break;
                        }

                    case "2":
                        {
                            Console.WriteLine("Enter a result of direct BWT (separated by space):");

                            var input = Console.ReadLine();

                            var BWTResult = input.Split();
                            if (BWTResult.Length != 2)
                            {
                                Console.WriteLine("You can't add not 2 arguments");
                                return;
                            }

                            if (!int.TryParse(BWTResult[1], out int index))
                            {
                                Console.WriteLine("Second variable should be number");
                                return;
                            }

                            Console.WriteLine($"Result of reverse transformation: {BWT.ReverseTransform(BWTResult[0], index)}");
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
    }
}