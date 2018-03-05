using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class functionCallClass : ISearchable, IAHKNode
		{
			public string functionName;
			public List<IAHKNode> functionParameterList;
			
			public functionCallClass(string functionName, List<IAHKNode> functionParameterList)
			{
				this.functionName = functionName;
				this.functionParameterList = functionParameterList;
			}

			public override string ToString() => $"{functionName} ({functionParameterList.Flatten(", ")})";

			public List<IAHKNode> Searchables
			{
				get {return functionParameterList;}
			}

			public IAHKNode extraInfo {get; set;}
		}

		public class complexFunctionCallClass : ISearchable, IAHKNode
		{
			public string _this, functionName;
			public List<IAHKNode> functionParameterList, chain;
			
			public complexFunctionCallClass(string _this, List<IAHKNode> varOrFuncChain, List<IAHKNode> functionParameterList)
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

			public List<IAHKNode> Searchables
			{
				get {return chain.Concat(functionParameterList).ToList();}
			}

			public IAHKNode extraInfo {get; set;}
		}
	}
}