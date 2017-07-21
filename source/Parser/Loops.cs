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
	}
}