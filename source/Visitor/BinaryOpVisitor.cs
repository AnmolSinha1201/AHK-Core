using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
		#region binaryOp
        public class binaryOpLink
        {
            public string op;
            public object expression;

            public binaryOpLink(string op, object expression)
            {
                this.op = op;
                this.expression = expression;
            }

            public override string ToString() =>(op == null? "" : op + " ") + expression;
        }

        // the first item of the list should always be a head. A head's op = null
        public class binaryOperationClass
        {
            public List<binaryOpLink> binaryOpLinkList;

            public binaryOperationClass(List<binaryOpLink> binaryOpLinkList)
            {
                this.binaryOpLinkList = binaryOpLinkList;
            }

            public override string ToString() => binaryOpLinkList.Flatten(" ");
        }

        public virtual binaryOperationClass binaryOperation(List<binaryOpLink> binaryOpLinkList)
        {
            return new binaryOperationClass(binaryOpLinkList);
        }
		#endregion
	}
}