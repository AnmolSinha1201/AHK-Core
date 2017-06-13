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
			public string functionName, defaultValue, _this;
			public List<object> functionParameterList, chain;
			public object originalDescription;
			
			public complexFunctionCallClass(string _this, List<object> varOrFuncChain, List<object> functionParameterList)
			{
				this._this = _this;
				
				if (varOrFuncChain[varOrFuncChain.Count - 1].GetType() == typeof(functionCallClass))
				{
					var temp = (functionCallClass)varOrFuncChain[varOrFuncChain.Count - 1];
					this.functionName = temp.functionName;
					this.functionParameterList = temp.functionParameterList;
					this.originalDescription = temp;
				}
				else
				{
					var temp = (bracketUnwrapClass)varOrFuncChain[varOrFuncChain.Count - 1];
					this.functionName = temp.expression.ToString();
					this.functionParameterList = functionParameterList;
					this.originalDescription = temp;
				}
				varOrFuncChain.RemoveAt(varOrFuncChain.Count - 1);
				this.chain = varOrFuncChain;

				this.defaultValue = $"{_this}{this.chain.FlattenAsChain()}{functionName} ({this.functionParameterList.FlattenAsFunctionParam()})";
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