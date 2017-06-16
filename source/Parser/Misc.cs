using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static AHKCore.BaseVisitor;

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
			const string _this = "this.";
			if (code.Length < origin + _this.Length)
				return null;
			if (!code.Substring(origin, _this.Length).Equals(_this, StringComparison.OrdinalIgnoreCase))
				return null;
			origin += _this.Length;
			return _this;
		}

		string DOUBLE(string code, ref int origin)
		{
			int pos = origin;
			string pre = INT(code, ref pos);
			if (pre == null)
				return null;

			if (code.Length < pos + ".".Length)
				return null;
			if (code[pos] != '.')
				return null;
			pos++;
			
			string post = INT(code, ref pos);
			if (post == null)
				return null;
			
			origin = pos;
			return pre + "." + post;
		}

		string HEX(string code, ref int origin)
		{
			int pos = origin;
			const string hexID = "0x";

			if (code.Length < pos + hexID.Length)
				return null;
			if (!code.Substring(pos, hexID.Length).Equals(hexID, StringComparison.OrdinalIgnoreCase))
				return null;
			pos += hexID.Length;

			while(pos < code.Length && "0123456789abcdefABCDEF".Contains(code[pos].ToString()))
				pos++;
			
			string retVal = code.Substring(origin, pos - origin);
			origin = pos;
			return retVal;
		}

		string INT(string code, ref int origin)
		{
			int pos = origin;
			while(origin < code.Length && "0123456789".Contains(code[origin].ToString()))
				origin++;
			if (origin == pos)
				return null;
			return code.Substring(pos, origin - pos);
		}

		/*
			Call Order :
			HEX -> INT
			DOUBLE -> INT
		*/
		string NUMBER(string code, ref int origin)
		{
			return HEX(code, ref origin) ?? DOUBLE(code, ref origin) ?? INT(code, ref origin);
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

	public static class listExtension
	{
		// extension method to convert List<object> to correct string
		public static string FlattenAsChain(this List<object> l)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var v in l)
				sb.Append(v.ToString());
			
			return sb.ToString();
		}

		public static string FlattenAsFunctionParam(this List<object> l)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var v in l)
				sb.Append(sb.Length == 0 ? v : ", " + v);
			
			return sb.ToString();
		}
	}
}