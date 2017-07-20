using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		#region parameterInfo
        public class parameterInfoClass
		{
			public variableClass variableName;
			public object expression;
			public string defaultValue;
			public bool isVariadic;

			public parameterInfoClass(variableClass variableName)
			{
				this.variableName = variableName;
				this.defaultValue = variableName.ToString();
			}

			public parameterInfoClass(variableClass variableName, object expression)
			{
				this.variableName = variableName;
				this.expression = expression;
				this.defaultValue = variableName + " = " + expression;
			}

			public parameterInfoClass(variableClass variableName, bool isVariadic)
			{
				this.variableName = variableName;
				this.isVariadic = isVariadic;
				this.defaultValue = variableName + "*";
			}

			public override string ToString() => defaultValue;
		}

		public virtual parameterInfoClass parameterInfo(variableClass variableName)
		{
			return new parameterInfoClass(variableName);
		}

		public virtual parameterInfoClass parameterInfo(variableClass variableName, object expression)
		{
			return new parameterInfoClass(variableName, expression);
		}

		public virtual parameterInfoClass parameterInfo(variableClass variableName, bool isVariadic)
		{
			return new parameterInfoClass(variableName, isVariadic);
		}
		#endregion

		#region functionHead
		public class functionHeadClass
		{
			public string defaultValue, functionName;
			public List<parameterInfoClass> functionParameters;

			public functionHeadClass(string functionName, List<parameterInfoClass> functionParameters)
			{
				this.functionName = functionName;
				this.functionParameters = functionParameters;
				this.defaultValue = $"{functionName}({functionParameters.FlattenAsFunctionParam()})";
			}

			public override string ToString() => defaultValue;
		}

		public virtual functionHeadClass functionHead(string functionName, List<parameterInfoClass> functionParameters)
		{
			return new functionHeadClass(functionName, functionParameters);
		}
		#endregion

		#region functionDeclaration
		public class functionDeclarationClass
		{
			public string defaultValue;
			public functionHeadClass functionHead;
			public List<object> functionBody;

			public functionDeclarationClass(functionHeadClass functionHead, List<object> functionBody)
			{
				this.functionHead = functionHead;
				this.functionBody = functionBody;
				
				this.defaultValue = $"{functionHead}\n{{{functionBody.FlattenAsChain("\n\t")}\n}}";
			}

			public override string ToString() => defaultValue;
		}

		public virtual functionDeclarationClass functionDeclaration(functionHeadClass functionHead, List<object> functionBody)
		{
			return new functionDeclarationClass(functionHead, functionBody);
		}
		#endregion
	}
}