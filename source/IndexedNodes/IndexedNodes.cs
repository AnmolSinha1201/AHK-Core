using System.Collections.Generic;
using AHKCore.IndexedNodesFragment;
using static AHKCore.Nodes;

namespace AHKCore
{
	/*
		- AutoExecute will contain everything else which could not be indexed, i.e. eveything other than
			functionDeclarations, classes.
		- Variables is mainly used for state management.
	 */
	public class IndexedNode
	{
		public Classes Classes = new Classes();
		public Functions Functions = new Functions();
		public Variables Variables = new Variables();
		public List<BaseAHKNode> AutoExecute = new List<BaseAHKNode>();
	}
}