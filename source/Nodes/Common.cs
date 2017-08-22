using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		/*
			- ISearchable exposes searchable parameters (variableName, op etc.) of a class (variableAssign) in the form of list.
			- Every class of AHKCore should implement this interface, except the classes which can not be subdivided ('break', 'NAME', 'variable').
			- In case no searchable parameter is to be exposed, return an empty 'List<object>'.
			- This would be used extensively by 'OfTypeRecusive<T>()'.
			- Read Only so external classes don't mess with it accidently. Messing with this would also affect 'OfTypeRecursive<T>()'.
			- Classes implementing 'ISearchable' must flatten their own lists, i.e. if classes (like complexVariable) contain one or more list,
				they must flatten them and return a single list, not a list of lists (chain and variableChain as one list instead of list of
				two lists. 'List.Concat()' comes handy here). This is done because Lists themselves do not implement 'ISearchable', 
				and would throw an error while recursing.
		 */
		public interface ISearchable
		{
			List<object> Searchables
			{
				get;
			}
		}
	}
}