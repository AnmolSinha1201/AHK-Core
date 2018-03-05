using System.Collections.Generic;
using static AHKCore.Nodes;

namespace AHKCore.IndexedNodesFragment
{
	public class Classes
	{
		Dictionary<string, IndexedNode> ClassList = new Dictionary<string, IndexedNode>();

		public IndexedNode this[string key]
		{
			get
			{
				if (ClassList.ContainsKey(key.ToLower()))
					return ClassList[key.ToLower()];
				return null;
			}
			set
			{
				ClassList[key.ToLower()] = value;
			}
		}
	}
}