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
			public string defaultValue;
			public ifBlockClass ifBlock;
			public elseBlockClass elseBlock;

			public ifElseBlockClass(ifBlockClass ifBlock, elseBlockClass elseBlock)
			{
				this.ifBlock = ifBlock;
				this.elseBlock = elseBlock;
				this.defaultValue = $"{ifBlock}{(elseBlock == null ? "" : "\n" + elseBlock)}";
			}

			public override string ToString() => defaultValue;
		}

		public virtual ifElseBlockClass ifElseBlock(ifBlockClass ifBlock, elseBlockClass elseBlock)
		{
			return new ifElseBlockClass(ifBlock, elseBlock);
		}
		#endregion

		#region ifBlock
		public class ifBlockClass
		{
			public string defaultValue;
			public object condition;
			public List<object> body;

			public ifBlockClass(object condition, List<object> body)
			{
				this.condition = condition;
				this.body = body;
				this.defaultValue = $"if ({condition})\n{{\n\t{body.FlattenAsChain("\n\t")}\n}}";
			}

			public override string ToString() => defaultValue;
		}

		public virtual ifBlockClass ifBlock(object condition, List<object> body)
		{
			return new ifBlockClass(condition, body);
		}
		#endregion

		#region elseBlock
		public class elseBlockClass
		{
			public string defaultValue;
			public List<object> body;

			public elseBlockClass(List<object> body)
			{
				this.body = body;
				if (body.Count == 1 && body[0].GetType() == typeof(ifBlockClass)) //to properly chain else ifs
					this.defaultValue = $"else {body}";
				else
					this.defaultValue = $"else\n{{\n\t{body.FlattenAsChain("\n\t")}\n}}";
			}

			public override string ToString() => defaultValue;
		}

		public virtual elseBlockClass elseBlock(List<object> body)
		{
			return new elseBlockClass(body);
		}
		#endregion
	}
}