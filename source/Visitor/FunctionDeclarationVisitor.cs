using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual parameterInfoClass parameterInfo(parameterInfoClass context)
		{
			return context;
		}

		public virtual functionHeadClass functionHead(functionHeadClass context)
		{
			return context;
		}

		public virtual functionDeclarationClass functionDeclaration(functionDeclarationClass context)
		{
			return context;
		}
	}
}