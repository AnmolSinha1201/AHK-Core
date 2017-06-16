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
	}
}