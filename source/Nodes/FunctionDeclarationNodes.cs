using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class parameterInfoClass : ISearchable, IExtraInfo
		{
			public variableClass variableName;
			public object expression;
			public bool isVariadic;

			public parameterInfoClass(variableClass variableName)
			{
				this.variableName = variableName;
			}

			public parameterInfoClass(variableClass variableName, object expression)
			{
				this.variableName = variableName;
				this.expression = expression;
			}

			public parameterInfoClass(variableClass variableName, bool isVariadic)
			{
				this.variableName = variableName;
				this.isVariadic = isVariadic;
			}

			public override string ToString() => variableName + (expression != null? " = " + expression : (isVariadic? "*" : ""));

			public List<object> Searchables
			{
				get {return new List<object>{variableName, expression};}
			}

			public object extraInfo {get; set;}
		}

		public class functionHeadClass : ISearchable, IExtraInfo
		{
			public string functionName;
			public List<parameterInfoClass> functionParameters;

			public functionHeadClass(string functionName, List<parameterInfoClass> functionParameters)
			{
				this.functionName = functionName;
				this.functionParameters = functionParameters;
			}

			public override string ToString() => $"{functionName}({functionParameters.Flatten(", ")})";

			public List<object> Searchables
			{
				get {return functionParameters.Cast<object>().ToList();}
			}

			public object extraInfo {get; set;}
		}

		public class functionDeclarationClass : ISearchable, IExtraInfo
		{
			public functionHeadClass functionHead;
			public List<object> functionBody;

			public functionDeclarationClass(functionHeadClass functionHead, List<object> functionBody)
			{
				this.functionHead = functionHead;
				this.functionBody = functionBody;
			}

			public override string ToString() => $"{functionHead}\n{{\n\t{functionBody.Flatten("\n\t")}\n}}";

			public List<object> Searchables
			{
				get 
				{
					var retList = new List<object>();
					retList.Add(functionHead);
					return retList.Concat(functionBody).ToList();
				}
			}

			public object extraInfo {get; set;}
		}
	}
}