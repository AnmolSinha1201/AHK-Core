using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.BaseVisitor;

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
		List<object> variableOrFunctionChaining(string code, ref int origin)
		{
			var chain = new List<object>();
			object retVal =  variableOrFunction(code, ref origin);
			if (retVal == null)
				return null; 
			chain.Add(retVal);

			while (true) 
			{
				if (origin == code.Length)
					break;
				retVal = (object) dotUnwrap(code, ref origin) ?? bracketUnwrap(code, ref origin);
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
		object variableOrFunction(string code, ref int origin)
		{
			return (object) functionCall(code, ref origin) ?? variable(code, ref origin);
		}

		dotUnwrapClass dotUnwrap(string code, ref int origin)
		{
			int pos = origin;

			if (stringMatcher(code, ref pos, ".") == null)
				return null;
			object retVal = variableOrFunction(code, ref pos);
				
			if (retVal == null)
				return null;
			
			origin = pos;
			return visitor.dotUnwrap(retVal);
		}

		bracketUnwrapClass bracketUnwrap(string code, ref int origin)
		{
			int pos = origin;

			WS(code, ref pos);
			if (stringMatcher(code, ref pos, "[") == null)
				return null;
			WS(code, ref pos);

			object retVal = Expression(code, ref pos);
			if (retVal == null)
				return null;
			
			WS(code, ref pos);
			if (stringMatcher(code, ref pos, "]") == null)
				return null;

			origin = pos;
			return visitor.bracketUnwrap(retVal);
		}
	}
}