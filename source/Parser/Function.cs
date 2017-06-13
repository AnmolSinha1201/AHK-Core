using System;
using System.Text;
using System.Collections.Generic;

namespace AHKCore
{
	partial class Parser
	{
		/*
			functionCall : NAME WS* functionParameter ;
			functionParameter : '(' WS* (expression? | (expression (CRLFWS* ',' WS* expression)*)) ')' ;

			implemented as
			
			functionCall : NAME WS* functionParameter;
			functionParameter : '(' WS* functionParameterList ')' ;
			functionParameterList : expression? | (expression (CRLFWS* ',' WS* expression)*) ;

			no need of WS* before ')' as functionParameterList ensures the WS. See Grammar file for more details.
			functionParameter is abstracted so it can be reused in complexFunctions.
		 */
		functionCallClass functionCall(string code, ref int origin)
		{
			int pos = origin;
			string functionName = NAME(code, ref pos);
			if (functionName == null)
				return null;

			WS(code, ref pos);
			List<object> functionParams = functionParameter(code, ref pos);
			if (functionParams == null)
				return null;
			
			origin = pos;
			return new functionCallClass(functionName, functionParams);
		}

		// just to "wrap" functionParameterList
		List<object> functionParameter(string code, ref int origin)
		{
			int pos = origin;
			
			if (code.Length < pos + "(".Length)
				return null;
			if (code[pos] != '(')
				return null;
			pos++;

			WS(code, ref pos);
			List<object> expressionList = functionParameterList(code, ref pos);

			if (code.Length < pos + ")".Length)
				return null;
			if (code[pos] != ')')
				return null;
			pos++;

			origin = pos;
			return expressionList;
		}

		
		// functionParameterList is separated so that it can use its visitor and return a list instead of string.
		List<object> functionParameterList(string code, ref int origin)
		{
			int pos = origin;
			List<object> expressionList = new List<object>();
			object o;
			
			if ((o = Expression(code, ref pos)) == null)
				return null;
			expressionList.Add(o);

			while (true)
			{
				CRLFWS(code, ref pos);

				if (code.Length < pos + ",".Length)
					break;
				if (code[pos] != ',')
					break;
				pos++;

				WS(code, ref pos);
				if ((o = Expression(code, ref pos)) == null)
					return null;
				expressionList.Add(o);
			}
			
			origin = pos;
			return expressionList;
		}

		// using List<objects> because of different types of Expressions
		class functionCallClass
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
	}
}