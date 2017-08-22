using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        public virtual dotUnwrapClass dotUnwrap(object variableOrFunction)
        {
            return new dotUnwrapClass(variableOrFunction);
        }

        public virtual bracketUnwrapClass bracketUnwrap(object expression)
        {
            return new bracketUnwrapClass(expression);
        }
	}
}