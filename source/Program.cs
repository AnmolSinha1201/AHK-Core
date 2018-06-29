using System;

namespace AHKCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var nodes = parser.parse("var1=123\nvar2=\"text2\"\nvar=var1&&var2");

            var indexer = new NodeIndexer();
            var indexed = indexer.IndexNodes(nodes);

            Console.WriteLine(indexed.Classes["qwe"].AutoExecute.Flatten());
        }
    }
}
