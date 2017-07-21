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
	}
}