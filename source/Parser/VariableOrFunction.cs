using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class Parser
	{
		/*
			variableOrFunction
				: variable
				| functionCall
				| variableOrFunction '.' variableOrFunction
				| variableOrFunction WS* '[' WS* expression WS* ']' ;

			implemented as

			variableOrFunctionChaining : variableOrFunction (dotUnwrap | bracketUnwrap)? ;
			variableOrFunction : functionCall | variable ;
			dotUnwrap : '.' variableOrFunction ;
			bracketUnwrap : WS* '[' WS* expression WS* ']' ;
		 */
		List<IAHKNode> variableOrFunctionChaining(string code, ref int origin)
		{
			var chain = new List<IAHKNode>();
			IAHKNode retVal =  variableOrFunction(code, ref origin);
			if (retVal == null)
				return null; 
			chain.Add(retVal);

			while (true) 
			{
				if (origin == code.Length)
					break;
				retVal = dotUnwrap(code, ref origin) ?? (IAHKNode) bracketUnwrap(code, ref origin);
				if (retVal == null)
					break;
				chain.Add(retVal);
			}
			
			return chain;
		}

		/*
			Call Order :
			- functionCall -> variable (to prevent variable from consuming functionName)
		 */
		IAHKNode variableOrFunction(string code, ref int origin)
		{
			return (IAHKNode) functionCall(code, ref origin) ?? variable(code, ref origin);
		}

		dotUnwrapClass dotUnwrap(string code, ref int origin)
		{
			int pos = origin;

			if (stringMatcher(code, ref pos, ".") == null)
				return null;
			IAHKNode retVal = variableOrFunction(code, ref pos);
				
			if (retVal == null)
				return null;
			
			origin = pos;
			return visitor.dotUnwrap(new dotUnwrapClass(retVal));
		}

		bracketUnwrapClass bracketUnwrap(string code, ref int origin)
		{
			int pos = origin;

			WS(code, ref pos);
			if (stringMatcher(code, ref pos, "[") == null)
				return null;
			WS(code, ref pos);

			IAHKNode retVal = Expression(code, ref pos);
			if (retVal == null)
				return null;
			
			WS(code, ref pos);
			if (stringMatcher(code, ref pos, "]") == null)
				return null;

			origin = pos;
			return visitor.bracketUnwrap(new bracketUnwrapClass(retVal));
		}
	}
}