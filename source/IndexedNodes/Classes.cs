using System.Collections.Generic;
using static AHKCore.Nodes;

namespace AHKCore.IndexedNodesFragment
{
	class Classes
	{
		Dictionary<string, IndexedNodes> ClassList = new Dictionary<string, IndexedNodes>();

		public IndexedNodes this[string key]
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