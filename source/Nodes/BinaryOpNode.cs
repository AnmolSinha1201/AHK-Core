using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
        public class binaryOpLink : ISearchable
        {
            public string op;
            public object expression;

            public binaryOpLink(string op, object expression)
            {
                this.op = op;
                this.expression = expression;
            }

            public override string ToString() =>(op == null? "" : op + " ") + expression;

            public List<object> Searchables
			{
				get {return new List<object>() {expression};}
			}
        }

        // the first item of the list should always be a head. A head's op = null
        public class binaryOperationClass : ISearchable
        {
            public List<binaryOpLink> binaryOpLinkList;

            public binaryOperationClass(List<binaryOpLink> binaryOpLinkList)
            {
                this.binaryOpLinkList = binaryOpLinkList;
            }

            public override string ToString() => binaryOpLinkList.Flatten(" ");

            public List<object> Searchables
			{
				get {return binaryOpLinkList.Cast<object>().ToList();}
			}
        }
	}
}