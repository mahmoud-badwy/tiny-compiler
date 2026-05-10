using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tiny;

namespace Tiny_Compiler
{
    public class Node
    {
        public List<Node> Children = new List<Node>();
        public string Name;

        public Node(string N)
        {
            this.Name = N;
        }
    }

    public class Parser
    {
        int InputPointer = 0;
        List<Token> TokenStream;
        public Node root;

        public Node StartParsing(List<Token> TokenStream)
        {
            this.InputPointer = 0;
            this.TokenStream = TokenStream;
            root = new Node("Program");
            root.Children.Add(Program());
            return root;
        }

        // Grammar Rule 1: Program → FunctionStatements MainFunction
        Node Program()
        {
            Node program = new Node("Program");
            program.Children.Add(FunctionStatements());
            program.Children.Add(MainFunction());

            // Check if we've consumed all tokens
            if (InputPointer < TokenStream.Count)
            {
                Errors.Error_List.Add("Parsing Error: Unexpected tokens after program end\r\n");
            }
            else
            {
                MessageBox.Show("Parsing completed successfully!");
            }

            return program;
        }

        // Grammar Rule 2-3: FunctionStatements → FunctionStatement FunctionStatements'
        // FunctionStatements' → FunctionStatement FunctionStatements' | ε
        Node FunctionStatements()
        {
            Node functionStatements = new Node("FunctionStatements");

            // Check if we have function declarations (not main)
            while (InputPointer < TokenStream.Count &&
                   (TokenStream[InputPointer].token_type == Token_Class.Int ||
                    TokenStream[InputPointer].token_type == Token_Class.Float ||
                    TokenStream[InputPointer].token_type == Token_Class.String))
            {
                // Look ahead to see if this is main function
                if (InputPointer + 1 < TokenStream.Count &&
                    TokenStream[InputPointer + 1].token_type == Token_Class.Main)
                {
                    break; // This is the main function, stop
                }

                functionStatements.Children.Add(FunctionStatement());
            }

            return functionStatements;
        }

        // Grammar Rule 4: MainFunction → Datatype main ( ) FunctionBody
        Node MainFunction()
        {
            Node mainFunction = new Node("MainFunction");
            mainFunction.Children.Add(Datatype());
            mainFunction.Children.Add(match(Token_Class.Main));
            mainFunction.Children.Add(match(Token_Class.LParanthesis));
            mainFunction.Children.Add(match(Token_Class.RParanthesis));
            mainFunction.Children.Add(FunctionBody());
            return mainFunction;
        }

        // Grammar Rule 5: FunctionStatement → FunctionDeclaration FunctionBody
        Node FunctionStatement()
        {
            Node functionStatement = new Node("FunctionStatement");
            functionStatement.Children.Add(FunctionDeclaration());
            functionStatement.Children.Add(FunctionBody());
            return functionStatement;
        }

        // Grammar Rule 6: FunctionDeclaration → Datatype FunctionName ( Parameters )
        Node FunctionDeclaration()
        {
            Node functionDeclaration = new Node("FunctionDeclaration");
            functionDeclaration.Children.Add(Datatype());
            functionDeclaration.Children.Add(FunctionName());
            functionDeclaration.Children.Add(match(Token_Class.LParanthesis));
            functionDeclaration.Children.Add(Parameters());
            functionDeclaration.Children.Add(match(Token_Class.RParanthesis));
            return functionDeclaration;
        }

        // Grammar Rule 7: FunctionName → Identifier
        Node FunctionName()
        {
            Node functionName = new Node("FunctionName");
            functionName.Children.Add(match(Token_Class.Identifier));
            return functionName;
        }

        // Grammar Rule 8-9: Parameters → Parameter ParameterList | ε
        // ParameterList → , Parameter ParameterList | ε
        Node Parameters()
        {
            Node parameters = new Node("Parameters");

            // Check if there are parameters (not empty)
            if (InputPointer < TokenStream.Count &&
                (TokenStream[InputPointer].token_type == Token_Class.Int ||
                 TokenStream[InputPointer].token_type == Token_Class.Float ||
                 TokenStream[InputPointer].token_type == Token_Class.String))
            {
                parameters.Children.Add(Parameter());

                // Handle additional parameters
                while (InputPointer < TokenStream.Count &&
                       TokenStream[InputPointer].token_type == Token_Class.Comma)
                {
                    parameters.Children.Add(match(Token_Class.Comma));
                    parameters.Children.Add(Parameter());
                }
            }

            return parameters;
        }

        // Grammar Rule 10: Parameter → Datatype Identifier
        Node Parameter()
        {
            Node parameter = new Node("Parameter");
            parameter.Children.Add(Datatype());
            parameter.Children.Add(match(Token_Class.Identifier));
            return parameter;
        }

        // Grammar Rule 11: FunctionBody → { Statements ReturnStatement }
        Node FunctionBody()
        {
            Node functionBody = new Node("FunctionBody");
            functionBody.Children.Add(match(Token_Class.LBrace));
            functionBody.Children.Add(Statements());
            functionBody.Children.Add(ReturnStatement());
            functionBody.Children.Add(match(Token_Class.RBrace));
            return functionBody;
        }

        // Grammar Rule 12-13: Statements → Statement StatementList
        // StatementList → Statement StatementList | ε
        Node Statements()
        {
            Node statements = new Node("Statements");

            // Parse statements until we hit return or closing brace
            while (InputPointer < TokenStream.Count &&
                   TokenStream[InputPointer].token_type != Token_Class.Return &&
                   TokenStream[InputPointer].token_type != Token_Class.RBrace &&
                   TokenStream[InputPointer].token_type != Token_Class.Until &&
                   TokenStream[InputPointer].token_type != Token_Class.Else &&
                   TokenStream[InputPointer].token_type != Token_Class.ElseIf &&
                   TokenStream[InputPointer].token_type != Token_Class.End)
            {
                int previousInputPointer = InputPointer;
                Node stmt = Statement();
                if (stmt != null)
                    statements.Children.Add(stmt);

                if (InputPointer == previousInputPointer)
                {
                    Errors.Error_List.Add("Parsing Error: Unexpected token " +
                                          TokenStream[InputPointer].token_type.ToString() +
                                          " ('" + TokenStream[InputPointer].lex + "')\r\n");
                    InputPointer++;
                }
            }

            return statements;
        }

        // Grammar Rule 14: Statement → DeclarationStatement | AssignmentStatement | 
        //                              WriteStatement | ReadStatement | IfStatement | 
        //                              RepeatStatement | FunctionCall ; | ε
        Node Statement()
        {
            Node statement = new Node("Statement");

            if (InputPointer >= TokenStream.Count)
                return statement;

            Token_Class currentTokenType = TokenStream[InputPointer].token_type;

            // Check for declaration statement (starts with datatype)
            if (currentTokenType == Token_Class.Int ||
                currentTokenType == Token_Class.Float ||
                currentTokenType == Token_Class.String)
            {
                statement.Children.Add(DeclarationStatement());
            }
            // Check for assignment or function call (starts with identifier)
            else if (currentTokenType == Token_Class.Identifier)
            {
                // Look ahead to determine if it's assignment or function call
                if (InputPointer + 1 < TokenStream.Count)
                {
                    if (TokenStream[InputPointer + 1].token_type == Token_Class.Assign)
                    {
                        statement.Children.Add(AssignmentStatement());
                    }
                    else if (TokenStream[InputPointer + 1].token_type == Token_Class.LParanthesis)
                    {
                        statement.Children.Add(FunctionCall());
                        statement.Children.Add(match(Token_Class.Semicolon));
                    }
                }
            }
            // Check for write statement
            else if (currentTokenType == Token_Class.Write)
            {
                statement.Children.Add(WriteStatement());
            }
            // Check for read statement
            else if (currentTokenType == Token_Class.Read)
            {
                statement.Children.Add(ReadStatement());
            }
            // Check for if statement
            else if (currentTokenType == Token_Class.If)
            {
                statement.Children.Add(IfStatement());
            }
            // Check for repeat statement
            else if (currentTokenType == Token_Class.Repeat)
            {
                statement.Children.Add(RepeatStatement());
            }
            else
            {
                // Empty statement (ε)
                return null;
            }

            return statement;
        }

        // Grammar Rule 15: DeclarationStatement → Datatype IdentifierList ;
        Node DeclarationStatement()
        {
            Node declarationStatement = new Node("DeclarationStatement");
            declarationStatement.Children.Add(Datatype());
            declarationStatement.Children.Add(IdentifierList());
            declarationStatement.Children.Add(match(Token_Class.Semicolon));
            return declarationStatement;
        }

        // Grammar Rule 16: Datatype → int | float | string
        Node Datatype()
        {
            Node datatype = new Node("Datatype");

            if (InputPointer < TokenStream.Count)
            {
                Token_Class currentType = TokenStream[InputPointer].token_type;

                if (currentType == Token_Class.Int)
                    datatype.Children.Add(match(Token_Class.Int));
                else if (currentType == Token_Class.Float)
                    datatype.Children.Add(match(Token_Class.Float));
                else if (currentType == Token_Class.String)
                    datatype.Children.Add(match(Token_Class.String));
                else
                {
                    Errors.Error_List.Add("Parsing Error: Expected datatype (int, float, or string)\r\n");
                    InputPointer++;
                }
            }

            return datatype;
        }

        // Grammar Rule 17-18: IdentifierList → IdentifierItem IdentifierListTail
        // IdentifierListTail → , IdentifierItem IdentifierListTail | ε
        Node IdentifierList()
        {
            Node identifierList = new Node("IdentifierList");
            identifierList.Children.Add(IdentifierItem());

            // Handle additional identifiers
            while (InputPointer < TokenStream.Count &&
                   TokenStream[InputPointer].token_type == Token_Class.Comma)
            {
                identifierList.Children.Add(match(Token_Class.Comma));
                identifierList.Children.Add(IdentifierItem());
            }

            return identifierList;
        }

        // Grammar Rule 19: IdentifierItem → Identifier | Identifier := Expression
        Node IdentifierItem()
        {
            Node identifierItem = new Node("IdentifierItem");
            identifierItem.Children.Add(match(Token_Class.Identifier));

            // Check if there's an assignment
            if (InputPointer < TokenStream.Count &&
                TokenStream[InputPointer].token_type == Token_Class.Assign)
            {
                identifierItem.Children.Add(match(Token_Class.Assign));
                identifierItem.Children.Add(Expression());
            }

            return identifierItem;
        }

        // Grammar Rule 20: AssignmentStatement → Identifier := Expression ;
        Node AssignmentStatement()
        {
            Node assignmentStatement = new Node("AssignmentStatement");
            assignmentStatement.Children.Add(match(Token_Class.Identifier));
            assignmentStatement.Children.Add(match(Token_Class.Assign));
            assignmentStatement.Children.Add(Expression());
            assignmentStatement.Children.Add(match(Token_Class.Semicolon));
            return assignmentStatement;
        }

        // Grammar Rule 21: WriteStatement → write Expression ; | write endl ;
        Node WriteStatement()
        {
            Node writeStatement = new Node("WriteStatement");
            writeStatement.Children.Add(match(Token_Class.Write));

            if (InputPointer < TokenStream.Count &&
                TokenStream[InputPointer].token_type == Token_Class.Endl)
            {
                writeStatement.Children.Add(match(Token_Class.Endl));
            }
            else
            {
                writeStatement.Children.Add(Expression());
            }

            writeStatement.Children.Add(match(Token_Class.Semicolon));
            return writeStatement;
        }

        // Grammar Rule 22: ReadStatement → read Identifier ;
        Node ReadStatement()
        {
            Node readStatement = new Node("ReadStatement");
            readStatement.Children.Add(match(Token_Class.Read));
            readStatement.Children.Add(match(Token_Class.Identifier));
            readStatement.Children.Add(match(Token_Class.Semicolon));
            return readStatement;
        }

        // Grammar Rule 23: IfStatement → if ConditionStatement then Statements 
        //                                ElseIfStatements ElseStatement end
        Node IfStatement()
        {
            Node ifStatement = new Node("IfStatement");
            ifStatement.Children.Add(match(Token_Class.If));
            ifStatement.Children.Add(ConditionStatement());
            ifStatement.Children.Add(match(Token_Class.Then));
            ifStatement.Children.Add(Statements());
            ifStatement.Children.Add(ElseIfStatements());
            ifStatement.Children.Add(ElseStatement());
            ifStatement.Children.Add(match(Token_Class.End));
            return ifStatement;
        }

        // Grammar Rule 24: ElseIfStatements → ElseIfStatement ElseIfStatements | ε
        Node ElseIfStatements()
        {
            Node elseIfStatements = new Node("ElseIfStatements");

            while (InputPointer < TokenStream.Count &&
                   TokenStream[InputPointer].token_type == Token_Class.ElseIf)
            {
                elseIfStatements.Children.Add(ElseIfStatement());
            }

            return elseIfStatements;
        }

        // Grammar Rule 25: ElseIfStatement → elseif ConditionStatement then Statements
        Node ElseIfStatement()
        {
            Node elseIfStatement = new Node("ElseIfStatement");
            elseIfStatement.Children.Add(match(Token_Class.ElseIf));
            elseIfStatement.Children.Add(ConditionStatement());
            elseIfStatement.Children.Add(match(Token_Class.Then));
            elseIfStatement.Children.Add(Statements());
            return elseIfStatement;
        }

        // Grammar Rule 26: ElseStatement → else Statements | ε
        Node ElseStatement()
        {
            Node elseStatement = new Node("ElseStatement");

            if (InputPointer < TokenStream.Count &&
                TokenStream[InputPointer].token_type == Token_Class.Else)
            {
                elseStatement.Children.Add(match(Token_Class.Else));
                elseStatement.Children.Add(Statements());
            }

            return elseStatement;
        }

        // Grammar Rule 27: RepeatStatement → repeat Statements until ConditionStatement
        Node RepeatStatement()
        {
            Node repeatStatement = new Node("RepeatStatement");
            repeatStatement.Children.Add(match(Token_Class.Repeat));
            repeatStatement.Children.Add(Statements());
            repeatStatement.Children.Add(match(Token_Class.Until));
            repeatStatement.Children.Add(ConditionStatement());
            return repeatStatement;
        }

        // Grammar Rule 28: Expression → String | Equation
        Node Expression()
        {
            Node expression = new Node("Expression");

            if (InputPointer < TokenStream.Count)
            {
                if (TokenStream[InputPointer].token_type == Token_Class.StringValue)
                {
                    expression.Children.Add(match(Token_Class.StringValue));
                }
                else
                {
                    expression.Children.Add(Equation());
                }
            }

            return expression;
        }

        // Grammar Rule 29-30: Equation → Term EquationTail
        // EquationTail → ArithmeticOperator Term EquationTail | ε
        Node Equation()
        {
            Node equation = new Node("Equation");
            equation.Children.Add(Term());

            // Handle additional terms with operators
            while (InputPointer < TokenStream.Count &&
                   (TokenStream[InputPointer].token_type == Token_Class.PlusOp ||
                    TokenStream[InputPointer].token_type == Token_Class.MinusOp ||
                    TokenStream[InputPointer].token_type == Token_Class.MultiplyOp ||
                    TokenStream[InputPointer].token_type == Token_Class.DivideOp))
            {
                equation.Children.Add(ArithmeticOperator());
                equation.Children.Add(Term());
            }

            return equation;
        }

        // Grammar Rule 31: Term → Number | Identifier | FunctionCall | ( Equation )
        Node Term()
        {
            Node term = new Node("Term");

            if (InputPointer < TokenStream.Count)
            {
                Token_Class currentType = TokenStream[InputPointer].token_type;

                if (currentType == Token_Class.Number)
                {
                    term.Children.Add(match(Token_Class.Number));
                }
                else if (currentType == Token_Class.Identifier)
                {
                    // Look ahead to check if it's a function call
                    if (InputPointer + 1 < TokenStream.Count &&
                        TokenStream[InputPointer + 1].token_type == Token_Class.LParanthesis)
                    {
                        term.Children.Add(FunctionCall());
                    }
                    else
                    {
                        term.Children.Add(match(Token_Class.Identifier));
                    }
                }
                else if (currentType == Token_Class.LParanthesis)
                {
                    term.Children.Add(match(Token_Class.LParanthesis));
                    term.Children.Add(Equation());
                    term.Children.Add(match(Token_Class.RParanthesis));
                }
            }

            return term;
        }

        // Grammar Rule 32: ArithmeticOperator → + | - | * | /
        Node ArithmeticOperator()
        {
            Node arithmeticOperator = new Node("ArithmeticOperator");

            if (InputPointer < TokenStream.Count)
            {
                Token_Class currentType = TokenStream[InputPointer].token_type;

                if (currentType == Token_Class.PlusOp)
                    arithmeticOperator.Children.Add(match(Token_Class.PlusOp));
                else if (currentType == Token_Class.MinusOp)
                    arithmeticOperator.Children.Add(match(Token_Class.MinusOp));
                else if (currentType == Token_Class.MultiplyOp)
                    arithmeticOperator.Children.Add(match(Token_Class.MultiplyOp));
                else if (currentType == Token_Class.DivideOp)
                    arithmeticOperator.Children.Add(match(Token_Class.DivideOp));
            }

            return arithmeticOperator;
        }

        // Grammar Rule 33-34: ConditionStatement → Condition ConditionTail
        // ConditionTail → BooleanOperator Condition ConditionTail | ε
        Node ConditionStatement()
        {
            Node conditionStatement = new Node("ConditionStatement");
            conditionStatement.Children.Add(Condition());

            // Handle additional conditions with boolean operators
            while (InputPointer < TokenStream.Count &&
                   (TokenStream[InputPointer].token_type == Token_Class.AndOp ||
                    TokenStream[InputPointer].token_type == Token_Class.OrOp))
            {
                conditionStatement.Children.Add(BooleanOperator());
                conditionStatement.Children.Add(Condition());
            }

            return conditionStatement;
        }

        // Grammar Rule 35: Condition → Identifier ConditionOperator Term
        Node Condition()
        {
            Node condition = new Node("Condition");
            condition.Children.Add(match(Token_Class.Identifier));
            condition.Children.Add(ConditionOperator());
            condition.Children.Add(Term());
            return condition;
        }

        // Grammar Rule 36: ConditionOperator → < | > | = | <>
        Node ConditionOperator()
        {
            Node conditionOperator = new Node("ConditionOperator");

            if (InputPointer < TokenStream.Count)
            {
                Token_Class currentType = TokenStream[InputPointer].token_type;

                if (currentType == Token_Class.LessThanOp)
                    conditionOperator.Children.Add(match(Token_Class.LessThanOp));
                else if (currentType == Token_Class.GreaterThanOp)
                    conditionOperator.Children.Add(match(Token_Class.GreaterThanOp));
                else if (currentType == Token_Class.EqualOp)
                    conditionOperator.Children.Add(match(Token_Class.EqualOp));
                else if (currentType == Token_Class.NotEqualOp)
                    conditionOperator.Children.Add(match(Token_Class.NotEqualOp));
            }

            return conditionOperator;
        }

        // Grammar Rule 37: BooleanOperator → && | ||
        Node BooleanOperator()
        {
            Node booleanOperator = new Node("BooleanOperator");

            if (InputPointer < TokenStream.Count)
            {
                Token_Class currentType = TokenStream[InputPointer].token_type;

                if (currentType == Token_Class.AndOp)
                    booleanOperator.Children.Add(match(Token_Class.AndOp));
                else if (currentType == Token_Class.OrOp)
                    booleanOperator.Children.Add(match(Token_Class.OrOp));
            }

            return booleanOperator;
        }

        // Grammar Rule 38: FunctionCall → Identifier ( Arguments )
        Node FunctionCall()
        {
            Node functionCall = new Node("FunctionCall");
            functionCall.Children.Add(match(Token_Class.Identifier));
            functionCall.Children.Add(match(Token_Class.LParanthesis));
            functionCall.Children.Add(Arguments());
            functionCall.Children.Add(match(Token_Class.RParanthesis));
            return functionCall;
        }

        // Grammar Rule 39-40: Arguments → Expression ArgumentList | ε
        // ArgumentList → , Expression ArgumentList | ε
        Node Arguments()
        {
            Node arguments = new Node("Arguments");

            // Check if there are arguments (not empty)
            if (InputPointer < TokenStream.Count &&
                IsExpressionStart(TokenStream[InputPointer].token_type))
            {
                arguments.Children.Add(Expression());

                // Handle additional arguments
                while (InputPointer < TokenStream.Count &&
                       TokenStream[InputPointer].token_type == Token_Class.Comma)
                {
                    arguments.Children.Add(match(Token_Class.Comma));
                    arguments.Children.Add(Expression());
                }
            }

            return arguments;
        }

        bool IsExpressionStart(Token_Class tokenType)
        {
            return tokenType == Token_Class.StringValue ||
                   tokenType == Token_Class.Number ||
                   tokenType == Token_Class.Identifier ||
                   tokenType == Token_Class.LParanthesis;
        }

        // Grammar Rule 41: ReturnStatement → return Expression ;
        Node ReturnStatement()
        {
            Node returnStatement = new Node("ReturnStatement");
            returnStatement.Children.Add(match(Token_Class.Return));
            returnStatement.Children.Add(Expression());
            returnStatement.Children.Add(match(Token_Class.Semicolon));
            return returnStatement;
        }

        // Match function - verifies and consumes a token
        public Node match(Token_Class ExpectedToken)
        {
            if (InputPointer < TokenStream.Count)
            {
                if (ExpectedToken == TokenStream[InputPointer].token_type)
                {
                    Node newNode = new Node(ExpectedToken.ToString());
                    InputPointer++;
                    return newNode;
                }
                else
                {
                    Errors.Error_List.Add("Parsing Error: Expected " + ExpectedToken.ToString() +
                                          " but found " + TokenStream[InputPointer].token_type.ToString() +
                                          " ('" + TokenStream[InputPointer].lex + "')\r\n");
                    InputPointer++;
                    return null;
                }
            }
            else
            {
                Errors.Error_List.Add("Parsing Error: Expected " + ExpectedToken.ToString() +
                                      " but reached end of file\r\n");
                return null;
            }
        }

        // Print parse tree for visualization
        public static TreeNode PrintParseTree(Node root)
        {
            TreeNode tree = new TreeNode("Parse Tree");
            TreeNode treeRoot = PrintTree(root);
            if (treeRoot != null)
                tree.Nodes.Add(treeRoot);
            return tree;
        }

        static TreeNode PrintTree(Node root)
        {
            if (root == null || root.Name == null)
                return null;

            TreeNode tree = new TreeNode(root.Name);

            if (root.Children.Count == 0)
                return tree;

            foreach (Node child in root.Children)
            {
                if (child == null)
                    continue;
                tree.Nodes.Add(PrintTree(child));
            }

            return tree;
        }
    }
}