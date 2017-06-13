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

			by ensuring that variableOrFunction does not end with ')', it is ensured that it ends with a variable.
			This reduces the original grammar to a simpler one while also ensuring code reuse.
		 */
		string complexVariable(string code, ref int origin)
		{
			int pos = origin;
			string _this = THIS(code, ref pos);
			var vorF = variableOrFunctionChaining(code, ref pos);
			if (vorF == null || vorF[vorF.Count - 1].GetType() == typeof(functionCallClass))
				return null;

			origin = pos;
			return _this + vorF.Flatten();
		}
	}
}