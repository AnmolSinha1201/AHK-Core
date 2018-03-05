using System.Collections.Generic;
using AHKCore.IndexedNodesFragment;

namespace AHKCore
{
	/*
		- AutoExecute will contain everything else which could not be indexed, i.e. eveything other than
			functionDeclarations, classes.
		- Variables is mainly used for state management.
		- TODO: Make IndexedNodes compatible with NodeTraverser's objectDispatcher.
	 */
	class IndexedNodes
	{
		public Classes Classes = new Classes();
		public Functions Functions = new Functions();
		public Variables Variables = new Variables();
		public List<object> AutoExecute = new List<object>();
	}
}