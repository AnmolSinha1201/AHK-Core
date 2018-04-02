using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class functionCallClass : BaseAHKNode
		{
			public string functionName;
			public List<BaseAHKNode> functionParameterList;
			
			public functionCallClass(string functionName, List<BaseAHKNode> functionParameterList)
			{
				this.functionName = functionName;
				this.functionParameterList = functionParameterList;
			}

			public override string ToString() => $"{functionName} ({functionParameterList.Flatten(", ")})";
		}

		public class complexFunctionCallClass : BaseAHKNode
		{
			public string _this;
			public List<BaseAHKNode> chain;
			public BaseAHKNode function;
			public List<BaseAHKNode> functionParameterList;
			
			/*
				- If last element is a functionCall, it will be executed by traverser.
				- If it is a bracketUnwrap, execute it manually using functionParameterList.
				- This is done to avoid using extraInfo in explicitly.
			*/
			public complexFunctionCallClass(string _this, List<BaseAHKNode> varOrFuncChain, List<BaseAHKNode> functionParameterList)
			{
				this._this = _this;
				
				function = varOrFuncChain.Last();
				varOrFuncChain.RemoveAt(varOrFuncChain.Count - 1);

				switch (function)
				{
					case functionCallClass o:
						this.functionParameterList = o.functionParameterList;
					break;

					case dotUnwrapClass o:
						this.functionParameterList = ((functionCallClass)o.variableOrFunction).functionParameterList;
					break;

					default: //for bracketUnwrap
						this.functionParameterList = functionParameterList;
					break;
				}
				
				this.chain = varOrFuncChain;
			}

			public override string ToString() => $"{_this}{this.chain.Flatten()}" +
				(function.GetType() == typeof(functionCallClass)? function.ToString() : $"{function} ({functionParameterList})");
		}
	}
}