using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class variableClass : IExtraInfo
		{
			public string variableName;

			public variableClass(string variableName)
			{
				this.variableName = variableName;
			}

			public override string ToString() => variableName;

			public object extraInfo {get; set;}
		}

		public class complexVariableClass : ISearchable, IExtraInfo
		{
			public string _this;
			public List<object> chain, variableChain;
			
			public complexVariableClass(string _this, List<object> varOrFuncChain)
			{
				this._this = _this;
				
				int i = 0;
				variableChain = new List<object>();

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

			public List<object> Searchables
			{
				get {return chain.Concat(variableChain).ToList();}
			}

			public object extraInfo {get; set;}
		}

		public class variableAssignClass : ISearchable, IExtraInfo
		{
			public string op;
			public complexVariableClass variable;
			public object expression;

			public variableAssignClass(complexVariableClass variable, string op, object expression)
			{
				this.variable = variable;
				this.op = op;
				this.expression = expression;
			}

			public override string ToString() => $"{variable} {op} {expression}";

			public List<object> Searchables
			{
				get {return new List<object>() {variable, expression};}
			}

			public object extraInfo {get; set;}
		}

		public class variableDeclarationClass : IExtraInfo
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

			public object extraInfo {get; set;}
		}
	}
}