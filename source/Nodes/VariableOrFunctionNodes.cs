using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class dotUnwrapClass : ISearchable, IExtraInfo
		{
			public object variableOrFunction;

			public dotUnwrapClass(object variableOrFunction)
			{
				this.variableOrFunction = variableOrFunction;
			}

			public override string ToString() => "." + variableOrFunction;

			public List<object> Searchables
			{
				get {return new List<object>() {variableOrFunction};}
			}

			public object extraInfo {get; set;}
		}

		public class bracketUnwrapClass : ISearchable, IExtraInfo
		{
			public object expression;

			public bracketUnwrapClass(object expression)
			{
				this.expression = expression;
			}

			public override string ToString() => "[" + expression + "]";

			public List<object> Searchables
			{
				get {return new List<object>() {expression};}
			}

			public object extraInfo {get; set;}
		}
	}
}