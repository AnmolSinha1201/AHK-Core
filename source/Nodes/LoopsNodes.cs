using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class breakBlockClass
		{
			public override string ToString() => "break";
		}

		public class continueBlockClass
		{
			public override string ToString() => "continue";
		}

		public class loopLoopClass : ISearchable
		{
			public object count;
			public List<object> loopBody;

			public loopLoopClass(object count, List<object> loopBody)
			{
				this.count = count;
				this.loopBody = loopBody;
			}

			public override string ToString() => $"loop{(count == null? "" : ", " + count)}\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";

			public List<object> Searchables
			{
				get {return loopBody;}
			}
		}

		public class whileLoopClass : ISearchable
		{
			public object condition;
			public List<object> loopBody;

			public whileLoopClass(object condition, List<object> loopBody)
			{
				this.condition = condition;
				this.loopBody = loopBody;
			}

			public override string ToString() => $"while ({condition})\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";

			public List<object> Searchables
			{
				get 
				{
					var retList = new List<object>();
					retList.Add(condition);
					return retList.Concat(loopBody).ToList();
				}
			}
		}

		public class foreachLoopClass : ISearchable
		{
			public variableClass key, value;
			public object iterationObject;
			public List<object> loopBody;

			public foreachLoopClass(variableClass key, variableClass value, object iterationObject, List<object> loopBody)
			{
				this.key = key;
				this.value = value;
				this.iterationObject = iterationObject;
				this.loopBody = loopBody;
			}

			public override string ToString() => 
			$"for {key}{(value == null ? "" : ", " + value)} in {iterationObject}\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";

			public List<object> Searchables
			{
				get 
				{
					var retList = new List<object>();
					retList.Add(key);
					retList.Add(value);
					retList.Add(iterationObject);
					return retList.Concat(loopBody).ToList();
				}
			}
		}
	}
}