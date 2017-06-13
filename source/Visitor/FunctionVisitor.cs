using System;
using System.Collections.Generic;
using System.Text;

namespace AHKCore
{
	public abstract partial class BaseVisitor
	{
        #region functionCall
		public class functionCallClass
		{
			public string functionName, defaultValue;
			public List<object> functionParameterList;
			
			public functionCallClass(string functionName, List<object> functionParameterList)
			{
				this.functionName = functionName;
				this.functionParameterList = functionParameterList;

				StringBuilder sb = new StringBuilder();
				foreach (var v in functionParameterList)
					sb.Append(sb.Length == 0 ? v : ", " + v);
				this.defaultValue = $"{functionName} ({sb.ToString()})";
			}

			public override string ToString() => defaultValue;
		}

        public virtual functionCallClass functionCall(string functionName, List<object> functionParameterList)
		{
            return new functionCallClass(functionName, functionParameterList);
		}
        #endregion
	}
}