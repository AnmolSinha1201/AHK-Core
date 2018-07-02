using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class Parser
	{
		unaryOperationClass unaryOperation(string code, ref int origin)
		{
			return preOp(code, ref origin) ?? postOp(code, ref origin);
		}

		unaryOperationClass preOp(string code, ref int origin)
		{
			int pos = origin;
			
			var op = opChecker(code, ref pos, new string[]{"++", "--"});
			if (op == null)
				return null;
			
			var variable = complexVariable(code, ref pos);
			if (variable == null)
				return null;

			origin = pos;
			var retList = new List<BaseAHKNode>() { new opClass(op), variable };
			return visitor.unaryOperation(new unaryOperationClass(retList));
		}

		unaryOperationClass postOp(string code, ref int origin)
		{
			int pos = origin;

			var variable = complexVariable(code, ref pos);
			if (variable == null)
				return null;
			
			var op = opChecker(code, ref pos, new string[]{"++", "--"});
			if (op == null)
				return null;

			origin = pos;
			var retList = new List<BaseAHKNode>() { variable, new opClass(op) };
			return visitor.unaryOperation(new unaryOperationClass(retList));
		}
	}
}