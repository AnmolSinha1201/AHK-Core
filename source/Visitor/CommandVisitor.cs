using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		#region commandBlock
		public class commandBlockClass
		{
			public string defaultValue;
			public List<object> commandBlockList;

			public commandBlockClass(List<object> commandBlockList)
			{
				this.commandBlockList = commandBlockList;
				this.defaultValue = commandBlockList.FlattenAsChain("\n");
			}

			public override string ToString() => defaultValue;
		}

		public virtual commandBlockClass commandBlock(List<object> commandBlockList)
		{
			return new commandBlockClass(commandBlockList);
		}
		#endregion
	}
}