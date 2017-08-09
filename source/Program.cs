using System;

namespace AHKCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            Console.WriteLine(parser.Test()?.Flatten());
        }
    }
}
