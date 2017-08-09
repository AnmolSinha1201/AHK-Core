using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		#region ifElseBlock
		public class ifElseBlockClass
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

		public virtual ifElseBlockClass ifElseBlock(ifBlockClass ifBlock, elseBlockClass elseBlock)
		{
			return new ifElseBlockClass(ifBlock, elseBlock);
		}
		#endregion

		#region ifBlock
		public class ifBlockClass
		{
			public object condition;
			public List<object> body;

			public ifBlockClass(object condition, List<object> body)
			{
				this.condition = condition;
				this.body = body;
			}

			public override string ToString() => $"if ({condition})\n{{\n\t{body.Flatten("\n\t")}\n}}";
		}

		public virtual ifBlockClass ifBlock(object condition, List<object> body)
		{
			return new ifBlockClass(condition, body);
		}
		#endregion

		#region elseBlock
		public class elseBlockClass
		{
			public List<object> body;

			public elseBlockClass(List<object> body)
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

		public virtual elseBlockClass elseBlock(List<object> body)
		{
			return new elseBlockClass(body);
		}
		#endregion
	}
}