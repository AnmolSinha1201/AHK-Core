using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.BaseVisitor;

namespace AHKCore
{
	partial class Parser
	{
		commandBlockClass commandBlock(string code, ref int origin)
		{
			int pos = origin;
			var commandList = new List<object>();

			var _command = command(code, ref pos);
			if (_command == null)
				return null;
			
			var _subCommand = subCommand(code, ref pos);
			if (_subCommand == null)
				return null;

			commandList.Add(_command);
			commandList.Add(_subCommand);
			
			while (true)
			{
				int pos2 = pos;
				CRLFWS(code, ref pos2);

				var param = commandParam(code, ref pos2);
				if (param == null)
					break;
				commandList.Add(param);
				pos = pos2;
			}

			origin = pos;
			return visitor.commandBlock(commandList);
		}

		string command(string code, ref int origin)
		{
			return NAME(code, ref origin);
		}

		/*
			must have a "," if it has CRLF
		 */
		object subCommand(string code, ref int origin)
		{
			int pos = origin;
			bool hasComma = false;

			WS(code, ref pos);
			var hasCRLF = CRLF(code, ref pos);
			CRLFWS(code, ref pos);

			if (code.Length < pos + ",".Length) //end of string
				return null;
			if (code[pos] == ',')
			{
				hasComma = true;
				pos++;
			}
			if (hasCRLF != null && !hasComma)
				return null;

			WS(code, ref pos);
			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;
			
			origin = pos;
			return expression;
		}

		object commandParam(string code, ref int origin)
		{
			int pos = origin;

			if (code.Length < pos + ",".Length) //end of string
				return null;
			if (code[pos] != ',')
				return null;
			pos++;
			
			WS(code, ref pos);
			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;
			
			origin = pos;
			return expression;
		}
	}
}