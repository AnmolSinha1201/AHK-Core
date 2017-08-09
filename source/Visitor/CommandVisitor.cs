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
			public List<object> commandBlockList;

			public commandBlockClass(List<object> commandBlockList)
			{
				this.commandBlockList = commandBlockList;
			}

			public override string ToString() => commandBlockList.Flatten("\n");
		}

		public virtual commandBlockClass commandBlock(List<object> commandBlockList)
		{
			return new commandBlockClass(commandBlockList);
		}
		#endregion
	}
}