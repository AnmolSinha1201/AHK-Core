using System;
using System.Collections.Generic;
using System.Text;
using static AHKCore.Nodes;

namespace AHKCore
{
	partial class NodeTraverser
	{
		/*
			- objectDispatcher is for 'object' types where a rule produces multiple types, and we would need to typecheck for possible types.
			- Instead of typechecking for object types inside a function, this is a common function to achieve the same thing, though with
				slightly bigger overhead.
			- Composite types like 'functionDeclaraion' have their own traverser function which calls their sub component's traversers, 
				so call them instead of calling their visitors directly.
			- It is preferred to call the traverser function instead of calling visitors directly (even for simple types) just for the sake
				of consistency.
		 */
		public virtual BaseAHKNode objectDispatcher(BaseAHKNode node)
		{
			switch (node)
			{
				case variableAssignClass v :
					return variableAssign(v);

				case variableDeclarationClass v :
					return variableDeclaration(v);

				case classDeclarationClass c :
					return classDeclaration(c);

				case functionDeclarationClass f :
					return functionDeclaration(f);

				case complexFunctionCallClass c :
					return complexFunctionCall(c);

				case functionCallClass f :
					return functionCall(f);

				case complexVariableClass c :
					return complexVariable(c);

				case variableClass v :
					return variable(v);

				case INTClass i :
					return INT(i);

				case DOUBLEClass d :
					return DOUBLE(d);

				case HEXClass h :
					return HEX(h);
				
				case STRINGClass s :
					return STRING(s);

				case returnBlockClass r :
					return returnBlock(r);

				case newObjectClass o :
					return newObject(o);

				case binaryOperationClass o:
					return binaryOperation(o);

				case parenthesesExpressionClass o:
					return parenthesesExpression(o);

				case unaryOperationClass o:
					return unaryOperation(o);

				case ternaryOperationClass o:
					return ternaryOperation(o);

				case ifElseBlockClass o:
					return ifElseBlock(o);

				case loopLoopClass o:
					return loopLoop(o);

				default :
					return node;
			}
		}
	}
}