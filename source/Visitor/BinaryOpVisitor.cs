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
            public string defaultValue, op;
            public object expression;

            public binaryOpLink(string op, object expression)
            {
                this.op = op;
                this.expression = expression;

                this.defaultValue = (op == null? "" : op + " ") + expression;
            }

            public override string ToString() => defaultValue;
        }

        // the first item of the list should always be a head. A head's op = null
        public class binaryOperationClass
        {
            public List<binaryOpLink> binaryOpLinkList= new List<binaryOpLink>();
            public string defaultValue;

            public binaryOperationClass(List<binaryOpLink> binaryOpLinkList)
            {
                this.binaryOpLinkList = binaryOpLinkList;
                this.defaultValue = binaryOpLinkList.FlattenAsChain(" ");
            }

            public override string ToString() => defaultValue;
        }

        public virtual binaryOperationClass binaryOperation(List<binaryOpLink> binaryOpLinkList)
        {
            return new binaryOperationClass(binaryOpLinkList);
        }
		#endregion
	}
}