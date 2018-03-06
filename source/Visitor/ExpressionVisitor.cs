using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual expressionClass expression(expressionClass context)
		{
			return context;
		}
	}
}