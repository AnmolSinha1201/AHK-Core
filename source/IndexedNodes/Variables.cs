using System.Collections.Generic;

namespace AHKCore.IndexedNodesFragment
{
	public class Variables
	{
		Dictionary<string, VariableValue> VariableList = new Dictionary<string, VariableValue>();

		public VariableValue this[string key]
		{
			get
			{
				if (VariableList.ContainsKey(key.ToLower()))
					return VariableList[key.ToLower()];
				return VariableList[key.ToLower()] = new VariableValue();
			}
			set
			{
				VariableList[key.ToLower()] = new VariableValue() {Value = value}; 
			}
		}

		public bool Exists(string variableName)
		{
			return VariableList.ContainsKey(variableName.ToLower());
		}

		public class VariableValue: AHKCore.Nodes.BaseAHKNode
		{
			public object Value;
		}
	}
}