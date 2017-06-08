using System;
using System.Text.RegularExpressions;

namespace AHKCore
{
	partial class Parser
	{	
		string NAME(string code, ref int origin)
		{
			const string first = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
			const string last = first + "0123456789";

			int start = origin;
			if (code.Length <= origin)
				return null;
			if (!first.Contains(code[origin].ToString()))
				return null;
			origin++;

			while(code.Length > origin && last.Contains(code[origin].ToString()))
				origin++;
			return code.Substring(start, origin - start);
		}

		string STRING(string code, ref int origin)
		{
			Regex regex = new Regex(@"""(?>[^\\\n""]+|\\.)*""");
			Match match = regex.Match(code, origin);
			if (match.Success)
			{
				origin = match.Index + match.Length;
				return match.Value;
			}
			return null;
		}

		/*
			implemented as "THIS" because "this" is reserved
		 */
		string THIS(string code, ref int origin)
		{
			const string _this = "this.";
			if (code.Length < origin + _this.Length)
				return null;
			if (!code.Substring(origin, 4).Equals(_this, StringComparison.OrdinalIgnoreCase))
				return null;
			origin += _this.Length;
			return _this;
		}

		string CRLFWS(string code, ref int origin)
		{
			bool retVal = false;
			while (origin < code.Length && "\r\n\t ".Contains(code[origin].ToString()))
			{
				retVal = true;
				origin++;
			}
			return retVal ? " " : null;
		}

		string WS(string code, ref int origin)
		{
			bool retVal = false;
			while (origin < code.Length && "\t ".Contains(code[origin].ToString()))
			{
				retVal = true;
				origin++;
			}
			return retVal ? " " : null;
		}

		string CRLF(string code, ref int origin)
		{
			bool retVal = false;
			while (origin < code.Length && "\r\n".Contains(code[origin].ToString()))
			{
				retVal = true;
				origin++;
			}
			return retVal ? "\n" : null;
		}
	}
}