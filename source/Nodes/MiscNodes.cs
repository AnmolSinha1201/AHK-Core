using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class STRINGClass : IExtraInfo
		{
			public string STRING;
			
			public STRINGClass(string STRING)
			{
				this.STRING = STRING;
			}

			public override string ToString() => STRING;

			public object extraInfo {get; set;}
		}

		public class DOUBLEClass : IExtraInfo
		{
			public string DOUBLE;
			
			public DOUBLEClass(string DOUBLE)
			{
				this.DOUBLE = DOUBLE;
			}

			public override string ToString() => DOUBLE;

			public object extraInfo {get; set;}
		}

		public class HEXClass : IExtraInfo
		{
			public string HEX;
			
			public HEXClass(string HEX)
			{
				this.HEX = HEX;
			}

			public override string ToString() => HEX;

			public object extraInfo {get; set;}
		}

		public class INTClass : IExtraInfo
		{
			public string INT;
			
			public INTClass(string INT)
			{
				this.INT = INT;
			}

			public override string ToString() => INT;

			public object extraInfo {get; set;}
		}
	}
}