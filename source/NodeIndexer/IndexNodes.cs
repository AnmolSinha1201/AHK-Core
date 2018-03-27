using System.Collections.Generic;
using System.Linq;
using static AHKCore.Nodes;

namespace AHKCore
{
	public partial class NodeIndexer
	{
		public IndexedNode IndexNodes(List<BaseAHKNode> AHKNodes)
		{
			var indexed = new IndexedNode();
			
			foreach(var o in AHKNodes)
			{
				switch(o)
				{
					case classDeclarationClass c:
					indexed.Classes[c.className] = IndexNodes(c.classBody);
					break;

					case functionDeclarationClass f:
					indexed.Functions[f.functionHead.functionName].Add(f);
					break;

					default:
					var other = othersFilter(o);
					if (other != null)
						indexed.AutoExecute.Add(other);
					break;
				}
			}
			
			return indexed;
		}

		public virtual BaseAHKNode othersFilter(BaseAHKNode context)
		{
			return context;
		}
	}
}