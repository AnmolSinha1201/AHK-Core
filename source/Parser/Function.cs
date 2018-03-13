using System;
using System.Text;
using System.Collections.Generic;
using static AHKCore.Nodes;
using System.Linq;

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
			List<BaseAHKNode> functionParams = functionParameter(code, ref pos);
			if (functionParams == null)
				return null;
			
			origin = pos;
			return visitor.functionCall(new functionCallClass(functionName, functionParams));
		}

		// just to "wrap" functionParameterList
		List<BaseAHKNode> functionParameter(string code, ref int origin)
		{
			int pos = origin;
			
			if (stringMatcher(code, ref pos, "(") == null)
				return null;

			WS(code, ref pos);
			List<BaseAHKNode> expressionList = functionParameterList(code, ref pos) ?? new List<BaseAHKNode>();

			if (stringMatcher(code, ref pos, ")") == null)
				return null;

			origin = pos;
			return expressionList;
		}

		// functionParameterList is separated so that it can use its visitor and return a list instead of string.
		List<BaseAHKNode> functionParameterList(string code, ref int origin)
		{
			int pos = origin;
			List<BaseAHKNode> expressionList = new List<BaseAHKNode>();
			BaseAHKNode o;
			
			if ((o = Expression(code, ref pos)) == null)
				return null;
			expressionList.Add(o);

			while (true)
			{
				CRLFWS(code, ref pos);

				if (stringMatcher(code, ref pos, ",") == null)
					break;

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
			
			/*
				Last item should either be bracketUnwrap (so next would be its parameters),
				or dotUnwrap or functionCall. Check if dotUnwrap-ed item is a functionCall.
			 */
			if (vOrF.Last() is variableClass || (vOrF.Last() is dotUnwrapClass d && !(d.variableOrFunction is functionCallClass)))
				return null;
			
			List<BaseAHKNode> fParam = null;
			if (vOrF.Last() is bracketUnwrapClass)
			{
				WS(code, ref pos);
				fParam = functionParameter(code, ref pos);
				if (fParam == null)
					return null;
			}

			origin = pos;
			return visitor.complexFunctionCall(new complexFunctionCallClass(_this, vOrF, fParam));
		}
	}
}