using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		/*
			- IExtraInfo exposes extra user parameter of a class.
			- extraInfo is just a placeholder which can be anything.
			- Every class of AHKCore should implement this interface.
			- Value = null if nothing is to be exposed.
			- Users can use this to attach some extra information to AST nodes which they can later use. Interface simply helps write a filter function.
			- There is no restriction on type of extraInfo. It can be a simple string, or a complex delegate to be called. Users have to handle their own
				object and delegates. Be careful if you use multiple types as extraInfo.
		 */
		public interface IExtraInfo
		{
			BaseAHKNode extraInfo {get; set;}
		}

		/*
			- Basic interface to identify AHK nodes.
			- All AHKNodes have to implement this interface.
			- Helps by making code easier to read by reducing typecasting required (such as for getting extra info).
			- If any other interface is required to be added to all AHK nodes, we can simply add it to IAHKNode.
			- Can later be turned to abstract class if a common behavior is to be expected of all nodes.
		 */
		public abstract class BaseAHKNode: IExtraInfo
		{
			public BaseAHKNode extraInfo {get; set;}
		}
	}
}