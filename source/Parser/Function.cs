using System;
using System.Text;
using System.Collections.Generic;

namespace AHKCore
{
	partial class Parser
	{
		/*
			functionCall : NAME WS* '(' WS* expression? (CRLFWS* ',' WS* expression)* WS* ')' ;
			implemented as
			functionCall : NAME WS* '(' WS* functionCallList ')';
			functionCallList : Expression? (CRLFWS* ',' WS* Expression)*;
			no need of WS* before ')' as functionCallList can break after CRLFWS
		 */
        string functionCall(string code, ref int origin)
		{
			int pos = origin;
			string functionName = NAME(code, ref pos);
			if (functionName == null)
				return null;
			WS(code, ref pos);

			if (code.Length < pos + "(".Length && code[pos] != '(')
				return null;
			pos++;

			WS(code, ref pos);
			List<string> expressionList = functionCallList(code, ref pos);

			if (code.Length < pos + ")".Length && code[pos] != ')')
				return null;
			pos++;

			origin = pos;
			return functionName + "(" + expressionList.Count + ")";
		}

		List<string> functionCallList(string code, ref int origin)
		{
			int pos = origin;
			List<string> expressionList = new List<string>();
			string s;
			
			if ((s = Expression(code, ref pos)) != null)
			{
				expressionList.Add(s);
				while (true)
				{
					CRLFWS(code, ref pos);

					if (code.Length < pos + ",".Length && code[pos] != ',')
						break;
					pos++;

					WS(code, ref pos);
					if ((s = Expression(code, ref pos)) == null)
						return null; //error
					expressionList.Add(s);
				}
			}

			origin = pos;
			return expressionList;
		}
	}
}