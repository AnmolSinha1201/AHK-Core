using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual variableClass variable(string variableName)
		{
			return new variableClass(variableName);
		}

        public virtual complexVariableClass complexVariable(string _this, List<object> varOrFuncChain)
		{
            return new complexVariableClass(_this, varOrFuncChain);
		}

		public virtual variableAssignClass variableAssign(complexVariableClass variable, string op, object expression)
		{
			return new variableAssignClass(variable, op, expression);
		}

		public virtual variableDeclarationClass variableDeclaration(variableClass variableName, variableDeclarationClass.scope variableScope)
		{
			return new variableDeclarationClass(variableName, variableScope);
		}
	}
}