using System;

namespace AHKCore
{
	partial class Parser
	{
		string variable(string code, ref int origin)
		{
			return NAME(code, ref origin);
		}

		/*
			complexVariable 
				: this? variableOrFunction '.' variable
				| this? variableOrFunction WS* '[' WS* expression WS* ']' // variableOrFunction bracketUnwrap
				| this? variable ;

			implemented as 
			complexVariable : this? variableOrFunction ;

			by ensuring that variableOrFunction does not end with ')', it is ensured that it ends with a variable.
			This reduces the original grammar to a simpler one while also ensuring code reuse.
		 */
		string complexVariable(string code, ref int origin)
		{
			int pos = origin;
			string _this = THIS(code, ref pos);
			string vorF = variableOrFunctionChaining(code, ref pos);
			if (vorF == null || vorF[vorF.Length - 1] == ')')
				return null;

			origin = pos;
			return _this + vorF;
		}
	}
}