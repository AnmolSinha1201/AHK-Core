using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		INTClass INT(INTClass context)
		{
			return visitor.INT(context);
		}

		HEXClass HEX(HEXClass context)
		{
			return visitor.HEX(context);
		}

		DOUBLEClass DOUBLE(DOUBLEClass context)
		{
			return visitor.DOUBLE(context);
		}

		STRINGClass STRING(STRINGClass context)
		{
			return visitor.STRING(context);
		}
	}
}