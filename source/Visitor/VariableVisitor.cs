using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        #region variable
        public class variableClass
		{
			public string variableName, defaultValue;

			public variableClass(string variableName)
			{
				this.variableName = variableName;
				this.defaultValue = variableName;
			}

			public override string ToString() => defaultValue;
		}

        public virtual variableClass variable(string variableName)
		{
            return new variableClass(variableName);
		}
        #endregion
	}
}