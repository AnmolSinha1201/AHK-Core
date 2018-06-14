using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		public virtual BaseAHKNode expression(BaseAHKNode context)
		{
			return context;
		}

		public virtual parenthesesExpressionClass parenthesesExpression(parenthesesExpressionClass context)
		{
			return context;
		}
	}
}