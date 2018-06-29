using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class STRINGClass : BaseAHKNode
		{
			public string STRING;
			
			public STRINGClass(string STRING)
			{
				// remove quotes
				this.STRING = STRING.Substring(1, STRING.Length - 2);
			}

			public override string ToString() => STRING;
		}

		public class DOUBLEClass : BaseAHKNode
		{
			public double DOUBLE;
			
			public DOUBLEClass(string DOUBLE)
			{
				this.DOUBLE = double.Parse(DOUBLE);
			}

			public override string ToString() => (DOUBLE % 1) <= double.Epsilon * 100 ? ((int)DOUBLE).ToString() : DOUBLE.ToString();
		}

		public class HEXClass : BaseAHKNode
		{
			public Int64 HEX;
			
			public HEXClass(string HEX)
			{
				this.HEX = Int64.Parse(HEX);
			}

			public override string ToString() => HEX.ToString();
		}

		public class INTClass : BaseAHKNode
		{
			public Int64 INT;
			
			public INTClass(string INT)
			{
				this.INT = Int64.Parse(INT);
			}

			public override string ToString() => INT.ToString();
		}
	}
}