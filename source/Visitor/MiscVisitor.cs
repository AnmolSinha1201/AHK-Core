using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		#region STRING
		public class STRINGClass
		{
			public string STRING;
			
			public STRINGClass(string STRING)
			{
				this.STRING = STRING;
			}

			public override string ToString() => STRING;
		}

		public virtual STRINGClass STRING(string STRING)
		{
			return new STRINGClass(STRING);
		}
		#endregion

		#region DOUBLE
		public class DOUBLEClass
		{
			public string DOUBLE;
			
			public DOUBLEClass(string DOUBLE)
			{
				this.DOUBLE = DOUBLE;
			}

			public override string ToString() => DOUBLE;
		}

		public virtual DOUBLEClass DOUBLE(string DOUBLE)
		{
			return new DOUBLEClass(DOUBLE);
		}
		#endregion

		#region HEX
		public class HEXClass
		{
			public string HEX;
			
			public HEXClass(string HEX)
			{
				this.HEX = HEX;
			}

			public override string ToString() => HEX;
		}

		public virtual HEXClass HEX(string HEX)
		{
			return new HEXClass(HEX);
		}
		#endregion

		#region INT
		public class INTClass
		{
			public string INT;
			
			public INTClass(string INT)
			{
				this.INT = INT;
			}

			public override string ToString() => INT;
		}

		public virtual INTClass INT(string INT)
		{
			return new INTClass(INT);
		}
		#endregion
	}
}