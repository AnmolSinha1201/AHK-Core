using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual STRINGClass STRING(STRINGClass context)
		{
			return context;
		}

		public virtual DOUBLEClass DOUBLE(DOUBLEClass context)
		{
			return context;
		}

		public virtual HEXClass HEX(HEXClass context)
		{
			return context;
		}

		public virtual INTClass INT(INTClass context)
		{
			return context;
		}
	}
}