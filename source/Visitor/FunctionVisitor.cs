using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        public virtual functionCallClass functionCall(string functionName, List<object> functionParameterList)
		{
            return new functionCallClass(functionName, functionParameterList);
		}

        public virtual complexFunctionCallClass complexFunctionCall(string _this, List<object> varOrFuncChain, List<object> functionParameterList)
		{
            return new complexFunctionCallClass(_this, varOrFuncChain, functionParameterList);
		}
	}
}