using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        public virtual breakBlockClass breakBlock()
        {
            return new breakBlockClass();
        }

        public virtual continueBlockClass continueBlock()
        {
            return new continueBlockClass();
        }

        public virtual loopLoopClass loopLoop(object count, List<object> loopBody)
        {
            return new loopLoopClass(count, loopBody);
        }

        public virtual whileLoopClass whileLoop(object condition, List<object> loopBody)
        {
            return new whileLoopClass(condition, loopBody);
        }

        public virtual foreachLoopClass foreachLoop(variableClass key, variableClass value, object iterationObject, List<object> loopBody)
        {
            return new foreachLoopClass(key, value, iterationObject, loopBody);
        }
	}
}