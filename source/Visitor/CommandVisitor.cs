using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		#region commandBlock
		public class commandBlockClass : ISearchable
		{
			public List<object> commandBlockList;

			public commandBlockClass(List<object> commandBlockList)
			{
				this.commandBlockList = commandBlockList;
			}

			public override string ToString() => commandBlockList.Flatten("\n");

			public List<object> Searchables
			{
				get {return commandBlockList;}
			}
		}

		public virtual commandBlockClass commandBlock(List<object> commandBlockList)
		{
			return new commandBlockClass(commandBlockList);
		}
		#endregion
	}
}