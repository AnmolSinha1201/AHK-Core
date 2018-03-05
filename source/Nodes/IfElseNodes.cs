using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHKCore
{
	public abstract partial class Nodes
	{
		public class ifElseBlockClass : ISearchable, IAHKNode
		{
			public ifBlockClass ifBlock;
			public elseBlockClass elseBlock;

			public ifElseBlockClass(ifBlockClass ifBlock, elseBlockClass elseBlock)
			{
				this.ifBlock = ifBlock;
				this.elseBlock = elseBlock;
			}

			public override string ToString() => $"{ifBlock}{(elseBlock == null ? "" : "\n" + elseBlock)}";

			public List<IAHKNode> Searchables
			{
				get {return new List<IAHKNode>() {ifBlock, elseBlock};}
			}

			public IAHKNode extraInfo {get; set;}
		}

		public class ifBlockClass : ISearchable, IAHKNode
		{
			public IAHKNode condition;
			public List<IAHKNode> body;

			public ifBlockClass(IAHKNode condition, List<IAHKNode> body)
			{
				this.condition = condition;
				this.body = body;
			}

			public override string ToString() => $"if ({condition})\n{{\n\t{body.Flatten("\n\t")}\n}}";

			public List<IAHKNode> Searchables
			{
				get 
				{
					var retList = new List<IAHKNode>();
					retList.Add(condition);
					return retList.Concat(body).ToList();
				}
			}

			public IAHKNode extraInfo {get; set;}
		}

		public class elseBlockClass : ISearchable, IAHKNode
		{
			public List<IAHKNode> body;

			public elseBlockClass(List<IAHKNode> body)
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

			public List<IAHKNode> Searchables
			{
				get {return body;}
			}

			public IAHKNode extraInfo {get; set;}
		}
	}
}