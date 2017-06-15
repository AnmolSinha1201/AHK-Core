using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        #region functionCall
		public class functionCallClass
		{
			public string functionName, defaultValue;
			public List<object> functionParameterList;
			
			public functionCallClass(string functionName, List<object> functionParameterList)
			{
				this.functionName = functionName;
				this.functionParameterList = functionParameterList;
				this.defaultValue = $"{functionName} ({functionParameterList.FlattenAsFunctionParam()})";
			}

			public override string ToString() => defaultValue;
		}

        public virtual functionCallClass functionCall(string functionName, List<object> functionParameterList)
		{
            return new functionCallClass(functionName, functionParameterList);
		}
        #endregion

		#region complexFunctionCall
		public class complexFunctionCallClass
		{
			public string defaultValue, _this;
			public List<object> functionParameterList, chain;
			// type = string for func(param) and List<object> for class["name"](param)
			public object functionName; 
			
			public complexFunctionCallClass(string _this, List<object> varOrFuncChain, List<object> functionParameterList)
			{
				this._this = _this;
				
				if (varOrFuncChain[varOrFuncChain.Count - 1].GetType() == typeof(functionCallClass))
				{
					var temp = (functionCallClass)varOrFuncChain[varOrFuncChain.Count - 1];
					this.functionName = temp.functionName;
					this.functionParameterList = temp.functionParameterList;
					varOrFuncChain.RemoveAt(varOrFuncChain.Count - 1);
				}
				else
				{
					int i = 0;
					functionName = new List<object>();

					/*
						no need to check for i = 0. dotUnwrap can only be present after i = 1 (example : a.c["n"])
						i = 0 means entire chain is the name (example : class["name"])
					 */
					for (i = varOrFuncChain.Count - 1; i > 0 && varOrFuncChain[i].GetType() != typeof(dotUnwrapClass); i--);
					((List<object>)functionName).AddRange(varOrFuncChain.GetRange(i, varOrFuncChain.Count - i));
					this.functionParameterList = functionParameterList;
					varOrFuncChain.RemoveRange(i, varOrFuncChain.Count - i);
				}
				this.chain = varOrFuncChain;

				this.defaultValue = $"{_this}{this.chain.FlattenAsChain()}" +
				$"{(functionName.GetType() == typeof(string) ? functionName : ((List<object>)functionName).FlattenAsChain())} ({this.functionParameterList.FlattenAsFunctionParam()})";
			}

			public override string ToString() => defaultValue;
		}

        public virtual complexFunctionCallClass complexFunctionCall(string _this, List<object> varOrFuncChain, List<object> functionParameterList)
		{
            return new complexFunctionCallClass(_this, varOrFuncChain, functionParameterList);
		}
		#endregion
	}
}