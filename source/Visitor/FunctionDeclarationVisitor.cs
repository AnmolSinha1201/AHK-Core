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
	}
}