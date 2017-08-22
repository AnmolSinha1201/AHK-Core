using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual STRINGClass STRING(string STRING)
		{
			return new STRINGClass(STRING);
		}

		public virtual DOUBLEClass DOUBLE(string DOUBLE)
		{
			return new DOUBLEClass(DOUBLE);
		}

		public virtual HEXClass HEX(string HEX)
		{
			return new HEXClass(HEX);
		}

		public virtual INTClass INT(string INT)
		{
			return new INTClass(INT);
		}
	}
}