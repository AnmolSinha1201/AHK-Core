using System.Collections.Generic;

namespace AHKCore.IndexedNodesFragment
{
	public class Variables
	{
		Dictionary<string, object> VariableList = new Dictionary<string, object>();

		public object this[string key]
		{
			get
			{
				if (VariableList.ContainsKey(key.ToLower()))
					return VariableList[key.ToLower()];
				return null;
			}
			set
			{ 
				VariableList[key.ToLower()] = value; 
			}
		}
	}
}