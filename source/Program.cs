using System;

namespace AHKCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var nodes = parser.parse("class qwe{var=123\nvar2=456}\nclass asd{var=123\nvar2=456}\nasd=asd");

            var indexer = new NodeIndexer();
            var indexed = indexer.IndexNodes(nodes);

            Console.WriteLine(indexed.Classes["qwe"].AutoExecute.Flatten());
        }
    }
}
