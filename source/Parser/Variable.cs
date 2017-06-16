using System;
using static AHKCore.BaseVisitor;

namespace AHKCore
{
	partial class Parser
	{
		variableClass variable(string code, ref int origin)
		{
			string variableName = NAME(code, ref origin);
			if (variableName == null)
				return null;

			return visitor.variable(variableName);
		}

		/*
			complexVariable 
				: this? variableOrFunction '.' variable
				| this? variableOrFunction WS* '[' WS* expression WS* ']' // variableOrFunction bracketUnwrap
				| this? variable ;

			implemented as 
			complexVariable : this? variableOrFunction ;

			by ensuring that variableOrFunction does not end with function, it is ensured that it ends with a variable.
			This reduces the original grammar to a simpler one while also ensuring code reuse.
			This check, however does not ensure if functionParameter is not present after variableOrFunction,
			i.e. this? variableOrFunction functionParameter (example : className[funcName](params) //invalid complexVariable). 
			For this purpose, queue complexFunction before complexVariable to ensure it consumes functionParameter.
		 */
		complexVariableClass complexVariable(string code, ref int origin)
		{
			int pos = origin;
			string _this = THIS(code, ref pos);
			var vorF = variableOrFunctionChaining(code, ref pos);
			// also ensures vOrF.Count > 0 since vOrF = null if not found
			if (vorF == null || vorF[vorF.Count - 1].GetType() == typeof(functionCallClass))
				return null;

			origin = pos;
			return visitor.complexVariable(_this,vorF);
		}

		string variableAssign(string code, ref int origin)
		{
			int pos = origin;

			var variableName = complexVariable(code, ref pos);
			if (variableName == null)
				return null;
			
			CRLFWS(code, ref pos);
			string[] ops = {":=", "+=", "-=", "*=", "/=", "//=", "+=", ".=", "|=", "&=", "^=", ">>=", "<<="};
			string op;
			if ((op = opChecker(code, ref pos, ops)) == null)
				return null;
			WS(code, ref pos);

			string expression = Expression(code, ref pos);
			if (expression == null)
				return null;

			origin = pos;
			return $"{variableName} {op} {expression}";
		}

		string opChecker(string code, ref int origin, string[] ops)
		{
			foreach (var op in ops)
				if (code.Substring(origin, op.Length) == op)
				{
					origin += op.Length;
					return op;
				}
			
			return null;
		}
	}
}