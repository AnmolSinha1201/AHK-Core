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
			noDefaultParam can consume partial grammar of other paramter types. Therefore ensure that parameter is no other type
			Eg : var * ---> var will be consumed by nDP and will leave *

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

		List<parameterInfoClass> defaultParameterList(string code, ref int origin)
		{
			int pos = origin;
			var parameterInfoList = new List<parameterInfoClass>();
			parameterInfoClass param;
			
			if ((param = defaultParam(code, ref pos)) == null)
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

				if ((param = defaultParam(code, ref pos2)) == null)
					break;

				pos = pos2;
				parameterInfoList.Add(param);
			}

			origin = pos;
			return parameterInfoList;
		}

		List<parameterInfoClass> variadicParameterList(string code, ref int origin)
		{
			var parameterInfoList = new List<parameterInfoClass>();
			parameterInfoClass param;
			
			if ((param = variadicParam(code, ref origin)) != null)
				parameterInfoList.Add(param);
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

			var list2 = defaultParameterList(code, ref pos);
			if (list2.Count > 0) // check for ','s only if list2 is empty.
			{
				CRLFWS(code, ref pos);
				if (code.Length < pos + ",".Length) //end of string
					return list1;
				if (code[pos] != ',') 
					return list1;
				pos++;
				WS(code, ref pos);
			}

			var list3 = variadicParameterList(code, ref pos);

			origin = pos;
			return list1.Concat(list2).Concat(list3).ToList();
		}

		/*
			functionDeclaration : functionHead functionBody;
		 */

		functionDeclarationClass functionDeclaration(string code, ref int origin)
		{
			int pos = origin;
			var fHead = functionHead(code, ref pos);
			if (fHead == null)
				return null;
			
			CRLFWS(code, ref pos);
			var fBody = functionBody(code, ref pos);
			if (fBody == null)
				return null;
			
			origin = pos;
			return visitor.functionDeclaration(fHead, fBody);
		}

		functionHeadClass functionHead(string code, ref int origin)
		{
			int pos = origin;

			string functionName = NAME(code, ref pos);
			if (functionName == null)
				return null;

			if (code.Length < pos + "(".Length) //end of string
				return null;
			if (code[pos] != '(') 
				return null;
			pos++;

			WS(code, ref pos);
			var functionParameters = functionDeclarationParamterList(code, ref pos);
			WS(code, ref pos);

			if (code.Length < pos + ")".Length) //end of string
				return null;
			if (code[pos] != ')') 
				return null;
			pos++;

			origin = pos;
			return visitor.functionHead(functionName, functionParameters);
		}

		List<object> functionBody(string code, ref int origin)
		{
			int pos = origin;

			if (code.Length < pos + "{".Length) //end of string
				return null;
			if (code[pos] != '{') 
				return null;
			pos++;
			
			CRLFWS(code, ref pos);
			var functionBody = functionBodyBlock(code, ref pos);
			if (functionBody == null)
				return null;
			CRLFWS(code, ref pos);

			if (code.Length < pos + "}".Length) //end of string
				return null;
			if (code[pos] != '}') 
				return null;
			pos++;

			origin = pos;
			return functionBody;
		}
	}
}