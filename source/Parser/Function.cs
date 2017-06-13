using System;
using System.Text;
using System.Collections.Generic;
using static AHKCore.BaseVisitor;

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
			return visitor.functionCall(functionName, functionParams);
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
			List<object> expressionList = functionParameterList(code, ref pos) ?? new List<object>();

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

		complexFunctionCallClass complexFunctionCall(string code, ref int origin)
		{
			int pos = origin;
			string _this = THIS(code, ref pos);

			var vOrF = variableOrFunctionChaining(code, ref pos);
			if (vOrF == null)
				return null;
			
			List<object> fParam = null;
			if (vOrF[vOrF.Count - 1].GetType() != typeof(functionCallClass))
			{
				WS(code, ref pos);
				fParam = functionParameter(code, ref pos);
				if (fParam == null)
					return null;
			}

			origin = pos;
			return visitor.complexFunctionCall(_this, vOrF, fParam);
		}
	}
}