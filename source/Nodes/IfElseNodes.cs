using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class ifElseBlockClass : Nodes
		{
			public ifBlockClass ifBlock;
			public elseBlockClass elseBlock;
			public object extraInfo;

			public ifElseBlockClass(ifBlockClass ifBlock, elseBlockClass elseBlock)
			{
				this.ifBlock = ifBlock;
				this.elseBlock = elseBlock;
			}

			public override string ToString() => $"{ifBlock}{(elseBlock == null ? "" : "\n" + elseBlock)}";

			public List<object> Searchables
			{
				get {return new List<object>() {ifBlock, elseBlock};}
			}
		}

		public class ifBlockClass : ISearchable
		{
			public object extraInfo, condition;
			public List<object> body;

			public ifBlockClass(object condition, List<object> body)
			{
				this.condition = condition;
				this.body = body;
			}

			public override string ToString() => $"if ({condition})\n{{\n\t{body.Flatten("\n\t")}\n}}";

			public List<object> Searchables
			{
				get 
				{
					var retList = new List<object>();
					retList.Add(condition);
					return retList.Concat(body).ToList();
				}
			}
		}

		public class elseBlockClass : ISearchable
		{
			public List<object> body;
			public object extraInfo;

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

			public List<object> Searchables
			{
				get {return body;}
			}
		}
	}
}