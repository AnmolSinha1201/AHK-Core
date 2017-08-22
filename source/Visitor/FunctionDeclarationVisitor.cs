using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
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

		public virtual functionHeadClass functionHead(string functionName, List<parameterInfoClass> functionParameters)
		{
			return new functionHeadClass(functionName, functionParameters);
		}

		public virtual functionDeclarationClass functionDeclaration(functionHeadClass functionHead, List<object> functionBody)
		{
			return new functionDeclarationClass(functionHead, functionBody);
		}
	}
}