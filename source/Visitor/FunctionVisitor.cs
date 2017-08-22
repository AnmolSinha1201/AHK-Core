using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
	  	public virtual functionCallClass functionCall(functionCallClass context)
		{
			return context;
		}

	  	public virtual complexFunctionCallClass complexFunctionCall(complexFunctionCallClass context)
		{
			return context;
		}
	}
}