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
			public string STRING, defaultValue;
			
			public STRINGClass(string STRING)
			{
				this.STRING = STRING;
				this.defaultValue = STRING;
			}

			public override string ToString() => defaultValue;
		}

		public virtual STRINGClass STRING(string STRING)
		{
			return new STRINGClass(STRING);
		}
		#endregion

		#region DOUBLE
		public class DOUBLEClass
		{
			public string DOUBLE, defaultValue;
			
			public DOUBLEClass(string DOUBLE)
			{
				this.DOUBLE = DOUBLE;
				this.defaultValue = DOUBLE;
			}

			public override string ToString() => defaultValue;
		}

		public virtual DOUBLEClass DOUBLE(string DOUBLE)
		{
			return new DOUBLEClass(DOUBLE);
		}
		#endregion

		#region HEX
		public class HEXClass
		{
			public string HEX, defaultValue;
			
			public HEXClass(string HEX)
			{
				this.HEX = HEX;
				this.defaultValue = HEX;
			}

			public override string ToString() => defaultValue;
		}

		public virtual HEXClass HEX(string HEX)
		{
			return new HEXClass(HEX);
		}
		#endregion

		#region INT
		public class INTClass
		{
			public string INT, defaultValue;
			
			public INTClass(string INT)
			{
				this.INT = INT;
				this.defaultValue = INT;
			}

			public override string ToString() => defaultValue;
		}

		public virtual INTClass INT(string INT)
		{
			return new INTClass(INT);
		}
		#endregion
	}
}