using System;
using static AHKCore.BaseVisitor;

namespace AHKCore
{
	partial class Parser
	{
        breakBlockClass breakBlock(string code, ref int origin)
        {
			return stringMatcher(code, ref origin, "break") == null ? null : visitor.breakBlock();
        }

        continueBlockClass continueBLock(string code, ref int origin)
        {
			return stringMatcher(code, ref origin, "continue") == null ? null : visitor.continueBlock();
        }

		loopLoopClass loopLoop(string code, ref int origin)
		{
			int pos = origin;

			var _command = commandBlock(code, ref pos);
			if (_command == null)
				return null;
			
			const string _loop = "loop";
			if (!_command.commandBlockList[0].ToString().Equals(_loop, StringComparison.OrdinalIgnoreCase))
				return null;

			CRLFWS(code, ref pos);
			var loopBody = loopBodyBlock(code, ref pos);
			if (loopBody == null)
				return null;
			
			origin = pos;
			return visitor.loopLoop(_command.commandBlockList.Count > 1 ? _command.commandBlockList[1] : null, loopBody);
		}

		whileLoopClass whileLoop(string code, ref int origin)
		{
			int pos = origin;

			if (stringMatcher(code, ref pos, "while") == null)
				return null;

			WS(code, ref pos);
			var condition = conditionalBlock(code, ref pos);
			if (condition == null)
				return null;
			
			CRLFWS(code, ref pos);
			var loopBody = loopBodyBlock(code, ref pos);
			if (loopBody == null)
				return null;

			origin = pos;
			return visitor.whileLoop(condition, loopBody);			
		}

		foreachLoopClass foreachLoop(string code, ref int origin) //for is not a command
		{
			int pos = origin;

			if (stringMatcher(code, ref pos, "for") == null)
				return null;

			if (WS(code, ref pos) == null)
				return null;
			var key = variable(code,ref pos);
			if (key == null)
				return null;

			int pos2 = pos;
			CRLFWS(code, ref pos2);
			var value = valueFinder(code, ref pos2);
			if (value != null)
				pos = pos2;
			
			if (WS(code, ref pos) == null) //proceed to "in" in both the cases
				return null;
			if (stringMatcher(code, ref pos, "in") == null)
				return null;
			if (WS(code, ref pos) == null)
				return null;
			
			var iterationObject = variable(code,ref pos);
			if (iterationObject == null)
				return null;
			
			CRLFWS(code, ref pos);
			var loopBody = loopBodyBlock(code, ref pos);
			if (loopBody == null)
				return null;

			return visitor.foreachLoop(key, value, iterationObject, loopBody);
		}

		variableClass valueFinder(string code, ref int origin)
		{
			int pos = origin;

			if (stringMatcher(code, ref pos, ",") == null)
				return null;
			WS(code, ref pos);

			var value = variable(code,ref pos);
			if (value == null)
				return null;

			origin = pos;
			return value;
		}
	}
}