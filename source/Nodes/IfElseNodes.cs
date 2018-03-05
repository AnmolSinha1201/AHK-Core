using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class ifElseBlockClass : BaseAHKNode
		{
			public ifBlockClass ifBlock;
			public elseBlockClass elseBlock;

			public ifElseBlockClass(ifBlockClass ifBlock, elseBlockClass elseBlock)
			{
				this.ifBlock = ifBlock;
				this.elseBlock = elseBlock;
			}

			public override string ToString() => $"{ifBlock}{(elseBlock == null ? "" : "\n" + elseBlock)}";
		}

		public class ifBlockClass : BaseAHKNode
		{
			public BaseAHKNode condition;
			public List<BaseAHKNode> body;

			public ifBlockClass(BaseAHKNode condition, List<BaseAHKNode> body)
			{
				this.condition = condition;
				this.body = body;
			}

			public override string ToString() => $"if ({condition})\n{{\n\t{body.Flatten("\n\t")}\n}}";
		}

		public class elseBlockClass : BaseAHKNode
		{
			public List<BaseAHKNode> body;

			public elseBlockClass(List<BaseAHKNode> body)
			{
				this.body = body;
			}

			public override string ToString()
			{
				if (body.Count == 1 && body[0].GetType() == typeof(ifBlockClass)) //to properly chain else ifs
					return $"else {body}";
				else
					return $"else\n{{\n\t{body.Flatten("\n\t")}\n}}";
			}
		}
	}
}