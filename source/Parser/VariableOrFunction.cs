using System;
using System.Collections.Generic;
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
			if (code[pos] != '.')
				return null;
			pos++;
			object retVal = variableOrFunction(code, ref pos);
				
			if (retVal == null)
				return null;
			
			origin = pos;
			return new dotUnwrapClass(retVal);
		}

		bracketUnwrapClass bracketUnwrap(string code, ref int origin)
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
			return new bracketUnwrapClass(retVal);
		}

		class dotUnwrapClass
		{
			object variableOrFunction;
			string defaultValue;

			public dotUnwrapClass(object variableOrFunction)
			{
				this.variableOrFunction = variableOrFunction;
				this.defaultValue = "." + variableOrFunction;
			}

			public override string ToString() => defaultValue;
		}

		class bracketUnwrapClass
		{
			object expression;
			string defaultValue;

			public bracketUnwrapClass(object expression)
			{
				this.expression = expression;
				this.defaultValue = "[" + expression + "]";
			}

			public override string ToString() => defaultValue;
		}
	}
}