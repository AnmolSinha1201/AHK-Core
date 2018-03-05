using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class dotUnwrapClass : ISearchable, IAHKNode
		{
			public IAHKNode variableOrFunction;

			public dotUnwrapClass(IAHKNode variableOrFunction)
			{
				this.variableOrFunction = variableOrFunction;
			}

			public override string ToString() => "." + variableOrFunction;

			public List<IAHKNode> Searchables
			{
				get {return new List<IAHKNode>() {variableOrFunction};}
			}

			public IAHKNode extraInfo {get; set;}
		}

		public class bracketUnwrapClass : ISearchable, IExtraInfo
		{
			public IAHKNode expression;

			public bracketUnwrapClass(IAHKNode expression)
			{
				this.expression = expression;
			}

			public override string ToString() => "[" + expression + "]";

			public List<IAHKNode> Searchables
			{
				get {return new List<IAHKNode>() {expression};}
			}

			public IAHKNode extraInfo {get; set;}
		}
	}
}