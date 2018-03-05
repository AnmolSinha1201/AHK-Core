using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class commandBlockClass : IAHKNode
		{
			public List<IAHKNode> commandBlockList;

			public commandBlockClass(List<IAHKNode> commandBlockList)
			{
				this.commandBlockList = commandBlockList;
			}

			public override string ToString() => commandBlockList.Flatten("\n");

			public IAHKNode extraInfo {get; set;}
		}

		public class commandClass: IAHKNode
		{
			public string command;

			public commandClass(string command)
			{
				this.command = command;
			}

			public override string ToString() => command;

			public IAHKNode extraInfo {get; set;}
		}
	}
}