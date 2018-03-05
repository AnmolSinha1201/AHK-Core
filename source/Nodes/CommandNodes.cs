using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class commandBlockClass : BaseAHKNode
		{
			public List<BaseAHKNode> commandBlockList;

			public commandBlockClass(List<BaseAHKNode> commandBlockList)
			{
				this.commandBlockList = commandBlockList;
			}

			public override string ToString() => commandBlockList.Flatten("\n");
		}

		public class commandClass: BaseAHKNode
		{
			public string command;

			public commandClass(string command)
			{
				this.command = command;
			}

			public override string ToString() => command;
		}
	}
}