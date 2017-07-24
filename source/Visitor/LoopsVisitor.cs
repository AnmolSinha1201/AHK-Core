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
            public string defaultValue = "break";

            public override string ToString() => defaultValue;
        }

        public virtual breakBlockClass breakBlock()
        {
            return new breakBlockClass();
        }
		#endregion

        #region continueBlock
        public class continueBlockClass
        {
            public string defaultValue = "continue";

            public override string ToString() => defaultValue;
        }

        public virtual continueBlockClass continueBlock()
        {
            return new continueBlockClass();
        }
        #endregion

        #region loopLoop
        public class loopLoopClass
        {
            public string defaultValue;
            public object count;
            public List<object> loopBody;

            public loopLoopClass(object count, List<object> loopBody)
            {
                this.count = count;
                this.loopBody = loopBody;
                this.defaultValue = $"loop{(count == null? "" : ", " + count)}\n{{\n\t{loopBody.FlattenAsChain("\n\t")}\n}}";
            }

            public override string ToString() => defaultValue;
        }

        public virtual loopLoopClass loopLoop(object count, List<object> loopBody)
        {
            return new loopLoopClass(count, loopBody);
        }
        #endregion

        #region whileLoop
        public class whileLoopClass
        {
            public string defaultValue;
            public object condition;
            public List<object> loopBody;

            public whileLoopClass(object condition, List<object> loopBody)
            {
                this.condition = condition;
                this.loopBody = loopBody;
                this.defaultValue = $"while ({condition})\n{{\n\t{loopBody.FlattenAsChain("\n\t")}\n}}";
            }

            public override string ToString() => defaultValue;
        }

        public virtual whileLoopClass whileLoop(object condition, List<object> loopBody)
        {
            return new whileLoopClass(condition, loopBody);
        }
        #endregion

        #region foreachLoop
        public class foreachLoopClass
        {
            public string defaultValue;
            public variableClass key, value;
            public object iterationObject;
            public List<object> loopBody;

            public foreachLoopClass(variableClass key, variableClass value, object iterationObject, List<object> loopBody)
            {
                this.key = key;
                this.value = value;
                this.iterationObject = iterationObject;
                this.loopBody = loopBody;

                this.defaultValue = $"for {key}{(value == null ? "" : ", " + value)} in {iterationObject}\n{{\n\t{loopBody.FlattenAsChain("\n\t")}\n}}";
            }

            public override string ToString() => defaultValue;
        }

        public virtual foreachLoopClass foreachLoop(variableClass key, variableClass value, object iterationObject, List<object> loopBody)
        {
            return new foreachLoopClass(key, value, iterationObject, loopBody);
        }
        #endregion
	}
}