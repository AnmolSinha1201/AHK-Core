using System;
using System.Text;

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
		string variableOrFunctionChaining(string code, ref int origin)
		{
			string retVal =  variableOrFunction(code, ref origin);
			if (retVal == null)
				return null; 
			
			string right = null;
			StringBuilder rightBuilder = new StringBuilder();
			while (true) 
			{
				if (origin == code.Length)
					break;
				right = dotUnwrap(code, ref origin) ?? bracketUnwrap(code, ref origin);
				if (right == null)
					break;
				rightBuilder.Append(right);
			}
			
			return retVal + (rightBuilder.Length == 0 ? "" : rightBuilder.ToString());
		}

		/*
			Call Order :
			- functionCall -> variable (to prevent variable from consuming functionName)
		 */
		string variableOrFunction(string code, ref int origin)
		{
			return functionCall(code, ref origin) ?? variable(code, ref origin);
		}
		
		string dotUnwrap(string code, ref int origin)
		{
			int pos = origin;
			if (code[pos] != '.')
				return null;
			pos++;
			string retVal = variableOrFunction(code, ref pos);
				
			if (retVal == null)
				return null;
			
			origin = pos;
			return "." + retVal;
		}

		string bracketUnwrap(string code, ref int origin)
		{
			int pos = origin;

			WS(code, ref pos);
			if (code[pos] != '[')
				return null;
			pos++;
			WS(code, ref pos);

			string retVal = Expression(code, ref pos);
			if (retVal == null)
				return null;
			
			WS(code, ref pos);
			if (code[pos] != ']')
				return null;
			pos++;

			origin = pos;
			return "[" + retVal + "]";
		}
	}
}