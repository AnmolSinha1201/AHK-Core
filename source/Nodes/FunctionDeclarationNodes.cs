using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class parameterInfoClass : ISearchable, IAHKNode
		{
			public variableClass variableName;
			public IAHKNode expression;
			public bool isVariadic;

			public parameterInfoClass(variableClass variableName)
			{
				this.variableName = variableName;
			}

			public parameterInfoClass(variableClass variableName, IAHKNode expression)
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

			public List<IAHKNode> Searchables
			{
				get {return new List<IAHKNode>{variableName, expression};}
			}

			public IAHKNode extraInfo {get; set;}
		}

		public class functionHeadClass : ISearchable, IAHKNode
		{
			public string functionName;
			public List<parameterInfoClass> functionParameters;

			public functionHeadClass(string functionName, List<parameterInfoClass> functionParameters)
			{
				this.functionName = functionName;
				this.functionParameters = functionParameters;
			}

			public override string ToString() => $"{functionName}({functionParameters.Flatten(", ")})";

			public List<IAHKNode> Searchables
			{
				get {return functionParameters.Cast<IAHKNode>().ToList();}
			}

			public IAHKNode extraInfo {get; set;}
		}

		public class functionDeclarationClass : ISearchable, IAHKNode
		{
			public functionHeadClass functionHead;
			public List<IAHKNode> functionBody;
			public IAHKNode returnType = null;

			public functionDeclarationClass(functionHeadClass functionHead, List<IAHKNode> functionBody)
			{
				this.functionHead = functionHead;
				this.functionBody = functionBody;
			}

			public override string ToString() => $"{functionHead}\n{{\n\t{functionBody.Flatten("\n\t")}\n}}";

			public List<IAHKNode> Searchables
			{
				get 
				{
					var retList = new List<IAHKNode>();
					retList.Add(functionHead);
					return retList.Concat(functionBody).ToList();
				}
			}

			public IAHKNode extraInfo {get; set;}
		}
	}
}