using System;

namespace AHKCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var nodes = parser.parse("var1=1<<1+1");

            var indexer = new NodeIndexer();
            var indexed = indexer.IndexNodes(nodes);

            Console.WriteLine(indexed.Classes["qwe"].AutoExecute.Flatten());
        }
    }
}
