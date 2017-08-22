using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual variableClass variable(variableClass context)
		{
			return context;
		}

        public virtual complexVariableClass complexVariable(complexVariableClass context)
		{
            return context;
		}

		public virtual variableAssignClass variableAssign(variableAssignClass context)
		{
			return context;
		}

		public virtual variableDeclarationClass variableDeclaration(variableDeclarationClass context)
		{
			return context;
		}
	}
}