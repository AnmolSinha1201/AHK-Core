using System;
using static AHKCore.BaseVisitor;

namespace AHKCore
{
	partial class Parser
	{
        breakBlockClass breakBlock(string code, ref int origin)
        {
            const string _break = "break";
			if (code.Length < origin + _break.Length)
				return null;
			if (!code.Substring(origin, _break.Length).Equals(_break, StringComparison.OrdinalIgnoreCase))
				return null;
			origin += _break.Length;
			return visitor.breakBlock();
        }

        continueBlockClass continueBLock(string code, ref int origin)
        {
            const string _continue = "continue";
			if (code.Length < origin + _continue.Length)
				return null;
			if (!code.Substring(origin, _continue.Length).Equals(_continue, StringComparison.OrdinalIgnoreCase))
				return null;
			origin += _continue.Length;
			return visitor.continueBlock();
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
	}
}