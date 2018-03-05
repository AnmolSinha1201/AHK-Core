using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class STRINGClass : IAHKNode
		{
			public string STRING;
			
			public STRINGClass(string STRING)
			{
				this.STRING = STRING;
			}

			public override string ToString() => STRING;

			public IAHKNode extraInfo {get; set;}
		}

		public class DOUBLEClass : IAHKNode
		{
			public string DOUBLE;
			
			public DOUBLEClass(string DOUBLE)
			{
				this.DOUBLE = DOUBLE;
			}

			public override string ToString() => DOUBLE;

			public IAHKNode extraInfo {get; set;}
		}

		public class HEXClass : IAHKNode
		{
			public string HEX;
			
			public HEXClass(string HEX)
			{
				this.HEX = HEX;
			}

			public override string ToString() => HEX;

			public IAHKNode extraInfo {get; set;}
		}

		public class INTClass : IAHKNode
		{
			public string INT;
			
			public INTClass(string INT)
			{
				this.INT = INT;
			}

			public override string ToString() => INT;

			public IAHKNode extraInfo {get; set;}
		}
	}
}