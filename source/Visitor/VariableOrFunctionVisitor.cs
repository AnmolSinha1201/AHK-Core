using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        #region dotUnwrap
        public class dotUnwrapClass
		{
			public object variableOrFunction;
			public string defaultValue;

			public dotUnwrapClass(object variableOrFunction)
			{
				this.variableOrFunction = variableOrFunction;
				this.defaultValue = "." + variableOrFunction;
			}

			public override string ToString() => defaultValue;
		}

        public virtual dotUnwrapClass dotUnwrap(object variableOrFunction)
        {
            return new dotUnwrapClass(variableOrFunction);
        }
        #endregion

        #region bracketUnwrap
		public class bracketUnwrapClass
		{
			public object expression;
			public string defaultValue;

			public bracketUnwrapClass(object expression)
			{
				this.expression = expression;
				this.defaultValue = "[" + expression + "]";
			}

			public override string ToString() => defaultValue;
		}

        public virtual bracketUnwrapClass bracketUnwrap(object expression)
        {
            return new bracketUnwrapClass(expression);
        }
        #endregion
	}
}