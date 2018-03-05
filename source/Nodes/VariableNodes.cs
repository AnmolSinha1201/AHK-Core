using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class variableClass : IAHKNode
		{
			public string variableName;

			public variableClass(string variableName)
			{
				this.variableName = variableName;
			}

			public override string ToString() => variableName;

			public IAHKNode extraInfo {get; set;}
		}

		public class complexVariableClass : IAHKNode
		{
			public string _this;
			public List<IAHKNode> chain, variableChain;
			
			public complexVariableClass(string _this, List<IAHKNode> varOrFuncChain)
			{
				this._this = _this;
				
				int i = 0;
				variableChain = new List<IAHKNode>();

				/*
					no need to check for i = 0. dotUnwrap can only be present after i = 1 (example : a.c["n"])
					i = 0 means entire chain is the name (example : class["name"])
				*/
				for (i = varOrFuncChain.Count - 1; i > 0 && varOrFuncChain[i].GetType() != typeof(dotUnwrapClass); i--);
				variableChain.AddRange(varOrFuncChain.GetRange(i, varOrFuncChain.Count - i));
				varOrFuncChain.RemoveRange(i, varOrFuncChain.Count - i);
				this.chain = varOrFuncChain;
			}

			public override string ToString() => $"{_this}{this.chain.Flatten()}{variableChain.Flatten()}";

			public IAHKNode extraInfo {get; set;}
		}

		public class variableAssignClass : IAHKNode
		{
			public string op;
			public complexVariableClass variable;
			public IAHKNode expression;

			public variableAssignClass(complexVariableClass variable, string op, IAHKNode expression)
			{
				this.variable = variable;
				this.op = op;
				this.expression = expression;
			}

			public override string ToString() => $"{variable} {op} {expression}";

			public IAHKNode extraInfo {get; set;}
		}

		public class variableDeclarationClass : IAHKNode
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

			public IAHKNode extraInfo {get; set;}
		}
	}
}