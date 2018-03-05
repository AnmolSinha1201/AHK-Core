using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class parameterInfoClass : BaseAHKNode
		{
			public variableClass variableName;
			public BaseAHKNode expression;
			public bool isVariadic;

			public parameterInfoClass(variableClass variableName)
			{
				this.variableName = variableName;
			}

			public parameterInfoClass(variableClass variableName, BaseAHKNode expression)
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
		}

		public class functionHeadClass : BaseAHKNode
		{
			public string functionName;
			public List<parameterInfoClass> functionParameters;

			public functionHeadClass(string functionName, List<parameterInfoClass> functionParameters)
			{
				this.functionName = functionName;
				this.functionParameters = functionParameters;
			}

			public override string ToString() => $"{functionName}({functionParameters.Flatten(", ")})";
		}

		public class functionDeclarationClass : BaseAHKNode
		{
			public functionHeadClass functionHead;
			public List<BaseAHKNode> functionBody;
			public BaseAHKNode returnType = null;

			public functionDeclarationClass(functionHeadClass functionHead, List<BaseAHKNode> functionBody)
			{
				this.functionHead = functionHead;
				this.functionBody = functionBody;
			}

			public override string ToString() => $"{functionHead}\n{{\n\t{functionBody.Flatten("\n\t")}\n}}";
		}
	}
}