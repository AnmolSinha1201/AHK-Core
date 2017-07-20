using System;
using System.Text;
using System.Collections.Generic;
using static AHKCore.BaseVisitor;
using System.Linq;

namespace AHKCore
{
	partial class Parser
	{
		parameterInfoClass noDefaultParam(string code, ref int origin)
		{
			var variableName = variable(code, ref origin);
			if (variableName == null)
				return null;

			return visitor.parameterInfo(variableName);
		}

		parameterInfoClass defaultParam(string code, ref int origin)
		{
			int pos = origin;

			var variableName = variable(code, ref pos);
			if (variableName == null)
				return null;

			CRLFWS(code, ref pos);
			if (code.Length < pos + "=".Length)
				return null;
			if (code[pos] != '=')
				return null;
			pos++;
			WS(code, ref pos);

			var expression = Expression(code, ref pos);
			if (expression == null)
				return null;
			
			origin = pos;
			return visitor.parameterInfo(variableName, expression);
		}

		parameterInfoClass variadicParam(string code, ref int origin)
		{
			int pos = origin;

			var variableName = variable(code, ref pos);
			if (variableName == null)
				return null;

			CRLFWS(code, ref pos);
			if (code.Length < pos + "*".Length)
				return null;
			if (code[pos] != '*')
				return null;
			pos++;
			
			origin = pos;
			return visitor.parameterInfo(variableName, true);
		}


		/*
			Split noDefaultParamList from other types of param lists because nDPs can consume other parameter partials leaving
			rest of the param list stuck.
			Eg : var * ---> var will be consumed by nDP and will leave *

			noDefaultParamList also ensures that the param is of no other type.

			All listing functions are implemented as : PARAM (',' PARAM)* so that we can check ','s after each list. This
			is necessary for stricter grammar conditions. Just putting them in a loop would be easier to implement but would
			result in bad grammar that would be a superset of required grammar.
		 */
		List<parameterInfoClass> noDefaultParamList(string code, ref int origin)
		{
			int pos = origin;
			var parameterInfoList = new List<parameterInfoClass>();
			parameterInfoClass param;

			if (variadicParam(code, ref pos) != null || defaultParam(code, ref pos) != null || (param = noDefaultParam(code, ref pos)) == null)
				return parameterInfoList;
			parameterInfoList.Add(param);

			while (true)
			{
				int pos2 = pos;
				CRLFWS(code, ref pos2);
				if (code.Length < pos2 + ",".Length) //end of string
					break;
				if (code[pos2] != ',')
					break;
				pos2++;
				WS(code, ref pos2);

				if ((defaultParam(code, ref pos2) != null || variadicParam(code, ref pos2) != null) || (param = noDefaultParam(code, ref pos2)) == null)
					break;
				pos = pos2;

				parameterInfoList.Add(param);
			}

			origin = pos;
			return parameterInfoList;
		}

		
		delegate parameterInfoClass functionParameterParserDelegate(string code, ref int origin);
		List<parameterInfoClass> otherParameterList(string code, ref int origin)
		{
			int pos = origin;
			var parameterParserQueue = new Queue<functionParameterParserDelegate>();
			parameterParserQueue.Enqueue(defaultParam);
			parameterParserQueue.Enqueue(variadicParam);
			var parameterInfoList = new List<parameterInfoClass>();
			parameterInfoClass param;
			
			var parserFunction = parameterParserQueue.Dequeue();
			if ((param = parserFunction(code, ref pos)) == null)
			{
				parserFunction = parameterParserQueue.Dequeue();
				if ((param = parserFunction(code, ref pos)) == null)
					return parameterInfoList;
			}
			parameterInfoList.Add(param);

			
			while (true)
			{
				int pos2 = pos;
				CRLFWS(code, ref pos2);
				if (code.Length < pos2 + ",".Length) //end of string
					break;
				if (code[pos2] != ',')
					break;
				pos2++;
				WS(code, ref pos2);

				if ((param = parserFunction(code, ref pos2)) == null)
				{
					if (parameterParserQueue.Count == 0)
						break;
					parserFunction = parameterParserQueue.Dequeue();
					continue;
				}
				parameterInfoList.Add(param);
				pos = pos2;
			}

			origin = pos;
			return parameterInfoList;
		}

		/*
			This function ties together all the lists. Also, each list starts with PARAM, and not ','. Therefore, ensure the ','
			has been removed before you call any list.

			Also, CRLFWS does nto tail or lead any lead. So ensure to remove them as well.
		 */
		List<parameterInfoClass> functionDeclarationParamterList(string code, ref int origin)
		{
			int pos = origin;
			var list1 = noDefaultParamList(code, ref pos);

			if (list1.Count > 0) // check for ','s only if list1 is empty.
			{
				CRLFWS(code, ref pos);
				if (code.Length < pos + ",".Length) //end of string
					return list1;
				if (code[pos] != ',') 
					return list1;
				pos++;
				WS(code, ref pos);
			}

			var list2 = otherParameterList(code, ref pos);

			origin = pos;
			return list1.Concat(list2).ToList();
		}
	}
}