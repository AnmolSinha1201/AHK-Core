using System;
using System.Text;
using System.Collections.Generic;
using static AHKCore.Nodes;
using System.Linq;

namespace AHKCore
{
	partial class Parser
	{
        ifElseBlockClass ifElseBlock(string code, ref int origin)
        {
            int pos = origin;
            var _ifBlock = ifBlock(code, ref pos);
            if (_ifBlock == null)
                return null;
            
            CRLFWS(code, ref pos);
            var _elseBlock = elseBlock(code, ref pos); //else block not compulsory
            
            origin = pos;
            return visitor.ifElseBlock(_ifBlock, _elseBlock);
        }

        ifBlockClass ifBlock(string code, ref int origin)
        {
            int pos = origin;

            if (stringMatcher(code, ref pos, "if") == null)
                return null;
                
            WS(code, ref pos);
            var condition = conditionalBlock(code, ref pos);
            if (condition == null)
                return null;

            CRLFWS(code, ref pos);
            var body = loopBodyBlock(code, ref pos);
            if (body == null)
                return null;
            
            origin = pos;
            return visitor.ifBlock(condition, body);
        }

        elseBlockClass elseBlock(string code, ref int origin)
        {
            int pos = origin;

            if (stringMatcher(code, ref pos, "else") == null)
                return null;
            
            CRLFWS(code, ref pos);
            var body = loopBodyBlock(code, ref pos);
            if (body == null)
                return null;

            origin = pos;
            return visitor.elseBlock(body);
        }
	}
}