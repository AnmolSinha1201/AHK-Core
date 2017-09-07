using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class commandBlockClass : ISearchable
		{
			public List<object> commandBlockList;
			public object extraInfo;

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
	}
}