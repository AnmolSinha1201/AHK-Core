using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class breakBlockClass: BaseAHKNode
		{
			public override string ToString() => "break";
		}

		public class continueBlockClass: BaseAHKNode
		{
			public override string ToString() => "continue";
		}

		public class loopLoopClass : BaseAHKNode
		{
			public BaseAHKNode count;
			public List<BaseAHKNode> loopBody;

			public loopLoopClass(BaseAHKNode count, List<BaseAHKNode> loopBody)
			{
				this.count = count;
				this.loopBody = loopBody;
			}

			public override string ToString() => $"loop{(count == null? "" : ", " + count)}\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";
		}

		public class whileLoopClass : BaseAHKNode
		{
			public BaseAHKNode condition;
			public List<BaseAHKNode> loopBody;

			public whileLoopClass(BaseAHKNode condition, List<BaseAHKNode> loopBody)
			{
				this.condition = condition;
				this.loopBody = loopBody;
			}

			public override string ToString() => $"while ({condition})\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";
		}

		public class foreachLoopClass : BaseAHKNode
		{
			public variableClass key, value;
			public BaseAHKNode iterationObject;
			public List<BaseAHKNode> loopBody;

			public foreachLoopClass(variableClass key, variableClass value, BaseAHKNode iterationObject, List<BaseAHKNode> loopBody)
			{
				this.key = key;
				this.value = value;
				this.iterationObject = iterationObject;
				this.loopBody = loopBody;
			}

			public override string ToString() => 
			$"for {key}{(value == null ? "" : ", " + value)} in {iterationObject}\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";
		}
	}
}