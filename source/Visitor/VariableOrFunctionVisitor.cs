using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        public virtual dotUnwrapClass dotUnwrap(dotUnwrapClass context)
        {
            return context;
        }

        public virtual bracketUnwrapClass bracketUnwrap(bracketUnwrapClass context)
        {
            return context;
        }
	}
}