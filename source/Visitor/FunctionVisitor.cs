using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        #region functionCall
		public class functionCallClass : ISearchable
		{
			public string functionName;
			public List<object> functionParameterList;
			
			public functionCallClass(string functionName, List<object> functionParameterList)
			{
				this.functionName = functionName;
				this.functionParameterList = functionParameterList;
			}

			public override string ToString() => $"{functionName} ({functionParameterList.Flatten(", ")})";

			public List<object> Searchables
			{
				get {return functionParameterList;}
			}
		}

        public virtual functionCallClass functionCall(string functionName, List<object> functionParameterList)
		{
            return new functionCallClass(functionName, functionParameterList);
		}
        #endregion

		#region complexFunctionCall
		public class complexFunctionCallClass : ISearchable
		{
			public string _this;
			public List<object> functionParameterList, chain, functionChain;
			
			public complexFunctionCallClass(string _this, List<object> varOrFuncChain, List<object> functionParameterList)
			{
				this._this = _this;
				functionChain = new List<object>();
				
				if (varOrFuncChain[varOrFuncChain.Count - 1].GetType() == typeof(functionCallClass))
				{
					var temp = (functionCallClass)varOrFuncChain[varOrFuncChain.Count - 1];
					functionChain.Add(temp.functionName);
					this.functionParameterList = temp.functionParameterList;
					varOrFuncChain.RemoveAt(varOrFuncChain.Count - 1);
				}
				else
				{
					int i = 0;

					/*
						no need to check for i = 0. dotUnwrap can only be present after i = 1 (example : a.c["n"])
						i = 0 means entire chain is the name (example : class["name"])
					 */
					for (i = varOrFuncChain.Count - 1; i > 0 && varOrFuncChain[i].GetType() != typeof(dotUnwrapClass); i--);
					functionChain.AddRange(varOrFuncChain.GetRange(i, varOrFuncChain.Count - i));
					this.functionParameterList = functionParameterList;
					varOrFuncChain.RemoveRange(i, varOrFuncChain.Count - i);
				}
				this.chain = varOrFuncChain;
			}

			public override string ToString() => $"{_this}{this.chain.Flatten()}" +
				$"{functionChain.Flatten()} ({this.functionParameterList.Flatten(", ")})";

			public List<object> Searchables
			{
				get {return chain.Concat(functionChain).Concat(functionParameterList).ToList();}
			}
		}

        public virtual complexFunctionCallClass complexFunctionCall(string _this, List<object> varOrFuncChain, List<object> functionParameterList)
		{
            return new complexFunctionCallClass(_this, varOrFuncChain, functionParameterList);
		}
		#endregion
	}
}