using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		public virtual INTClass INT(INTClass context)
		{
			return visitor.INT(context);
		}

		public virtual HEXClass HEX(HEXClass context)
		{
			return visitor.HEX(context);
		}

		public virtual DOUBLEClass DOUBLE(DOUBLEClass context)
		{
			return visitor.DOUBLE(context);
		}

		public virtual STRINGClass STRING(STRINGClass context)
		{
			return visitor.STRING(context);
		}
	}
}