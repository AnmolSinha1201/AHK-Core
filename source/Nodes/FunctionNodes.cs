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
			public string _this, functionName;
			public List<BaseAHKNode> functionParameterList, chain;
			
			public complexFunctionCallClass(string _this, List<BaseAHKNode> varOrFuncChain, List<BaseAHKNode> functionParameterList)
			{
				this._this = _this;
				
				var temp = (functionCallClass)varOrFuncChain[varOrFuncChain.Count - 1];
				functionName = temp.functionName;
				this.functionParameterList = temp.functionParameterList;
				varOrFuncChain.RemoveAt(varOrFuncChain.Count - 1);
				this.chain = varOrFuncChain;
			}

			public override string ToString() => $"{_this}{this.chain.Flatten()}" +
				$"{this.functionName} ({this.functionParameterList.Flatten(", ")})";
		}
	}
}