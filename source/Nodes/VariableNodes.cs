using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class variableClass : BaseAHKNode
		{
			public string variableName;

			public variableClass(string variableName)
			{
				this.variableName = variableName;
			}

			public override string ToString() => variableName;
		}

		public class complexVariableClass : BaseAHKNode
		{
			public string _this;
			public List<BaseAHKNode> chain;
			public variableClass variable;
			
			public complexVariableClass(string _this, List<BaseAHKNode> varOrFuncChain)
			{
				this._this = _this;
				
				variable = (variableClass)varOrFuncChain.Last();
				varOrFuncChain.RemoveAt(varOrFuncChain.Count - 1);
				this.chain = varOrFuncChain;
			}

			public override string ToString() => $"{_this}{this.chain.Flatten()}{variable}";
		}

		public class variableAssignClass : BaseAHKNode
		{
			public string op;
			public complexVariableClass complexVariable;
			public BaseAHKNode expression;

			public variableAssignClass(complexVariableClass variable, string op, BaseAHKNode expression)
			{
				this.complexVariable = variable;
				this.op = op;
				this.expression = expression;
			}

			public override string ToString() => $"{complexVariable} {op} {expression}";
		}

		public class variableDeclarationClass : BaseAHKNode
		{
			public variableClass variableName;
			public enum scope
			{
				SCOPE_LOCAL,
				SCOPE_GLOBAL,
				SCOPE_SUPERGLOBAL
			}
			public scope variableScope;

			public variableDeclarationClass(variableClass variableName, scope variableScope)
			{
				this.variableName = variableName;
				this.variableScope = variableScope;
			}

			public override string ToString() =>
			(variableScope == scope.SCOPE_GLOBAL || variableScope == scope.SCOPE_SUPERGLOBAL ? "global " : "") + variableName;
		}
	}
}