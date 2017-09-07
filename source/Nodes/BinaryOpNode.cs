using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class binaryOperationLinkClass : ISearchable
		{
			public string op;
			public object extraInfo, expression;

			public binaryOperationLinkClass(string op, object expression)
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
			public List<binaryOperationLinkClass> binaryOperationLinkList;

			public binaryOperationClass(List<binaryOperationLinkClass> binaryOpLinkList)
			{
				this.binaryOperationLinkList = binaryOpLinkList;
			}

			public override string ToString() => binaryOperationLinkList.Flatten(" ");

			public List<object> Searchables
			{
				get {return binaryOperationLinkList.Cast<object>().ToList();}
			}
		}
	}
}