using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        #region breakBlock
        public class breakBlockClass
        {
            public override string ToString() => "break";
        }

        public virtual breakBlockClass breakBlock()
        {
            return new breakBlockClass();
        }
		#endregion

        #region continueBlock
        public class continueBlockClass
        {
            public override string ToString() => "continue";
        }

        public virtual continueBlockClass continueBlock()
        {
            return new continueBlockClass();
        }
        #endregion

        #region loopLoop
        public class loopLoopClass
        {
            public object count;
            public List<object> loopBody;

            public loopLoopClass(object count, List<object> loopBody)
            {
                this.count = count;
                this.loopBody = loopBody;
            }

            public override string ToString() => $"loop{(count == null? "" : ", " + count)}\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";
        }

        public virtual loopLoopClass loopLoop(object count, List<object> loopBody)
        {
            return new loopLoopClass(count, loopBody);
        }
        #endregion

        #region whileLoop
        public class whileLoopClass
        {
            public object condition;
            public List<object> loopBody;

            public whileLoopClass(object condition, List<object> loopBody)
            {
                this.condition = condition;
                this.loopBody = loopBody;
            }

            public override string ToString() => $"while ({condition})\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";
        }

        public virtual whileLoopClass whileLoop(object condition, List<object> loopBody)
        {
            return new whileLoopClass(condition, loopBody);
        }
        #endregion

        #region foreachLoop
        public class foreachLoopClass
        {
            public variableClass key, value;
            public object iterationObject;
            public List<object> loopBody;

            public foreachLoopClass(variableClass key, variableClass value, object iterationObject, List<object> loopBody)
            {
                this.key = key;
                this.value = value;
                this.iterationObject = iterationObject;
                this.loopBody = loopBody;
            }

            public override string ToString() => 
            $"for {key}{(value == null ? "" : ", " + value)} in {iterationObject}\n{{\n\t{loopBody.Flatten("\n\t")}\n}}";
        }

        public virtual foreachLoopClass foreachLoop(variableClass key, variableClass value, object iterationObject, List<object> loopBody)
        {
            return new foreachLoopClass(key, value, iterationObject, loopBody);
        }
        #endregion
	}
}