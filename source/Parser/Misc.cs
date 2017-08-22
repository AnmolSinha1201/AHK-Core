using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class Parser
	{	
		string NAME(string code, ref int origin)
		{
			const string first = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
			const string last = first + "0123456789";

			int start = origin;
			if (code.Length < origin + 1) // minimum size of NAME is 1
				return null;
			if (!first.Contains(code[origin].ToString()))
				return null;
			origin++;

			while(code.Length > origin && last.Contains(code[origin].ToString()))
				origin++;
			return code.Substring(start, origin - start);
		}

		STRINGClass STRING(string code, ref int origin)
		{
			Regex regex = new Regex(@"""(?>[^\\\n""]+|\\.)*""");
			Match match = regex.Match(code, origin);
			if (match.Success)
			{
				origin = match.Index + match.Length;
				return visitor.STRING(match.Value);
			}
			return null;
		}

		/*
			implemented as "THIS" because "this" is reserved
		 */
		string THIS(string code, ref int origin)
		{
			return stringMatcher(code, ref origin, "this.");
		}

		DOUBLEClass DOUBLE(string code, ref int origin)
		{
			int pos = origin;
			var pre = INT(code, ref pos);
			if (pre == null)
				return null;

			if (stringMatcher(code, ref pos, ".") == null)
				return null;
			
			var post = INT(code, ref pos);
			if (post == null)
				return null;
			
			origin = pos;
			return visitor.DOUBLE(pre + "." + post);
		}

		HEXClass HEX(string code, ref int origin)
		{
			int pos = origin;
			const string hexID = "0x";

			if (stringMatcher(code, ref pos, hexID) == null)
				return null;

			while(code.Length > pos && "0123456789abcdefABCDEF".Contains(code[pos].ToString()))
				pos++;
			
			string retVal = code.Substring(origin + hexID.Length, pos - origin - hexID.Length);
			origin = pos;
			return visitor.HEX(retVal);
		}

		INTClass INT(string code, ref int origin)
		{
			int pos = origin;
			while(code.Length > origin && "0123456789".Contains(code[origin].ToString()))
				origin++;
			if (origin == pos)
				return null;
			return visitor.INT(code.Substring(pos, origin - pos));
		}

		/*
			Call Order :
			HEX -> INT
			DOUBLE -> INT
		*/
		object NUMBER(string code, ref int origin)
		{
			return HEX(code, ref origin) ?? (object) DOUBLE(code, ref origin) ?? INT(code, ref origin);
		}

		string CRLFWS(string code, ref int origin)
		{
			bool retVal = false;
			while (code.Length > origin && "\r\n\t ".Contains(code[origin].ToString()))
			{
				retVal = true;
				origin++;
			}
			return retVal ? " " : null;
		}

		string WS(string code, ref int origin)
		{
			bool retVal = false;
			while (code.Length > origin && "\t ".Contains(code[origin].ToString()))
			{
				retVal = true;
				origin++;
			}
			return retVal ? " " : null;
		}

		string CRLF(string code, ref int origin)
		{
			bool retVal = false;
			while (code.Length > origin && "\r\n".Contains(code[origin].ToString()))
			{
				retVal = true;
				origin++;
			}
			return retVal ? "\n" : null;
		}

		string opChecker(string code, ref int origin, string[] ops)
		{
			ops = ops.OrderByDescending(op => op.Length).ToArray();
			foreach (var op in ops)
				if (code.Length > origin + op.Length && code.Substring(origin, op.Length) == op)
				{
					origin += op.Length;
					return op;
				}
			
			return null;
		}

		string stringMatcher(string code, ref int origin, string toMatch)
		{
			if (code.Length < origin + toMatch.Length)
				return null;
			if (!code.Substring(origin, toMatch.Length).Equals(toMatch, StringComparison.OrdinalIgnoreCase))
				return null;
			origin += toMatch.Length;

			return toMatch;
		}
	}
}