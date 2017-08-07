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

		#region complexVariable
		public class complexVariableClass
		{
			public string defaultValue, _this;
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

				this.defaultValue = $"{_this}{this.chain.FlattenAsChain()}{variableChain.FlattenAsChain()}";
			}

			public override string ToString() => defaultValue;
		}

        public virtual complexVariableClass complexVariable(string _this, List<object> varOrFuncChain)
		{
            return new complexVariableClass(_this, varOrFuncChain);
		}
		#endregion

		#region variableAssign
		public class variableAssignClass
		{
			public string defaultValue, op;
			public complexVariableClass variable;
			public object expression;

			public variableAssignClass(complexVariableClass variable, string op, object expression)
			{
				this.variable = variable;
				this.op = op;
				this.expression = expression;
				this.defaultValue = $"{variable} {op} {expression}";
			}

			public override string ToString() => defaultValue;
		}

		public virtual variableAssignClass variableAssign(complexVariableClass variable, string op, object expression)
		{
			return new variableAssignClass(variable, op, expression);
		}
		#endregion

		#region variableDeclaration
		public class variableDeclarationClass
		{
			public string defaultValue;
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
				this.defaultValue = (variableScope == scope.SCOPE_GLOBAL || variableScope == scope.SCOPE_SUPERGLOBAL ? "global " : "") + variableName;
			}

			public override string ToString() => defaultValue;
		}

		public virtual variableDeclarationClass variableDeclaration(variableClass variableName, variableDeclarationClass.scope variableScope)
		{
			return new variableDeclarationClass(variableName, variableScope);
		}
		#endregion
	}
}