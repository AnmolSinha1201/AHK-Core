using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class STRINGClass
		{
			public string STRING;
			public object extraInfo;
			
			public STRINGClass(string STRING)
			{
				this.STRING = STRING;
			}

			public override string ToString() => STRING;
		}

		public class DOUBLEClass
		{
			public string DOUBLE;
			public object extraInfo;
			
			public DOUBLEClass(string DOUBLE)
			{
				this.DOUBLE = DOUBLE;
			}

			public override string ToString() => DOUBLE;
		}

		public class HEXClass
		{
			public string HEX;
			public object extraInfo;
			
			public HEXClass(string HEX)
			{
				this.HEX = HEX;
			}

			public override string ToString() => HEX;
		}

		public class INTClass
		{
			public string INT;
			public object extraInfo;
			
			public INTClass(string INT)
			{
				this.INT = INT;
			}

			public override string ToString() => INT;
		}
	}
}