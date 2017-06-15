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
	}
}