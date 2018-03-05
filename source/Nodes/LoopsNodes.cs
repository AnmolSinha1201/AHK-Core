using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class breakBlockClass: IAHKNode
		{
			public override string ToString() => "break";
			public IAHKNode extraInfo {get; set;}
		}

		public class continueBlockClass: IAHKNode
		{
			public override string ToString() => "continue";
			public IAHKNode extraInfo {get; set;}
		}

		public class loopLoopClass : ISearchable, IAHKNode
		{
			public IAHKNode count;
			public List<IAHKNode> loopBody;

			public loopLoopClass(IAHKNode count, List<IAHKNode> loopBody)
			{
				this.count = count;
				this.loopBody = loopBody;
			}

			public override string ToString() => $"loop{(count == null? "" : ", " + count)}\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";

			public List<IAHKNode> Searchables
			{
				get {return loopBody;}
			}

			public IAHKNode extraInfo {get; set;}
		}

		public class whileLoopClass : ISearchable, IAHKNode
		{
			public IAHKNode condition;
			public List<IAHKNode> loopBody;

			public whileLoopClass(IAHKNode condition, List<IAHKNode> loopBody)
			{
				this.condition = condition;
				this.loopBody = loopBody;
			}

			public override string ToString() => $"while ({condition})\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";

			public List<IAHKNode> Searchables
			{
				get 
				{
					var retList = new List<IAHKNode>();
					retList.Add(condition);
					return retList.Concat(loopBody).ToList();
				}
			}

			public IAHKNode extraInfo {get; set;}
		}

		public class foreachLoopClass : ISearchable, IAHKNode
		{
			public variableClass key, value;
			public IAHKNode iterationObject;
			public List<IAHKNode> loopBody;

			public foreachLoopClass(variableClass key, variableClass value, IAHKNode iterationObject, List<IAHKNode> loopBody)
			{
				this.key = key;
				this.value = value;
				this.iterationObject = iterationObject;
				this.loopBody = loopBody;
			}

			public override string ToString() => 
			$"for {key}{(value == null ? "" : ", " + value)} in {iterationObject}\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";

			public List<IAHKNode> Searchables
			{
				get 
				{
					var retList = new List<IAHKNode>();
					retList.Add(key);
					retList.Add(value);
					retList.Add(iterationObject);
					return retList.Concat(loopBody).ToList();
				}
			}

			public IAHKNode extraInfo {get; set;}
		}
	}
}