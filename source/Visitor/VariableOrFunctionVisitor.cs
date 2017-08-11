using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        #region dotUnwrap
        public class dotUnwrapClass : ISearchable
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
		}

        public virtual dotUnwrapClass dotUnwrap(object variableOrFunction)
        {
            return new dotUnwrapClass(variableOrFunction);
        }
        #endregion

        #region bracketUnwrap
		public class bracketUnwrapClass : ISearchable
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
		}

        public virtual bracketUnwrapClass bracketUnwrap(object expression)
        {
            return new bracketUnwrapClass(expression);
        }
        #endregion
	}
}