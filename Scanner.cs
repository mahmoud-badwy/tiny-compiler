using System;
using System.Collections.Generic;

namespace Tiny
{
    public class Scanner
    {
        public List<Token> Tokens = new List<Token>();

        Dictionary<string, Token_Class> ReservedWords = new Dictionary<string, Token_Class>();
        Dictionary<string, Token_Class> Operators = new Dictionary<string, Token_Class>();

        public Scanner()
        {
            // Reserved words
            ReservedWords.Add("if", Token_Class.If);
            ReservedWords.Add("repeat", Token_Class.Repeat);
            ReservedWords.Add("read", Token_Class.Read);
            ReservedWords.Add("write", Token_Class.Write);
            ReservedWords.Add("then", Token_Class.Then);
            ReservedWords.Add("else", Token_Class.Else);
            ReservedWords.Add("elseif", Token_Class.ElseIf);
            ReservedWords.Add("end", Token_Class.End);
            ReservedWords.Add("return", Token_Class.Return);
            ReservedWords.Add("int", Token_Class.Int);
            ReservedWords.Add("float", Token_Class.Float);
            ReservedWords.Add("string", Token_Class.String);
            ReservedWords.Add("endl", Token_Class.Endl);
            ReservedWords.Add("until", Token_Class.Until);

            // Operators & symbols
            Operators.Add(":=", Token_Class.Assign);
            Operators.Add("+", Token_Class.PlusOp);
            Operators.Add("-", Token_Class.MinusOp);
            Operators.Add("*", Token_Class.MultiplyOp);
            Operators.Add("/", Token_Class.DivideOp);
            Operators.Add("<", Token_Class.LessThanOp);
            Operators.Add(">", Token_Class.GreaterThanOp);
            Operators.Add("=", Token_Class.EqualOp);
            Operators.Add("<>", Token_Class.NotEqualOp);
            Operators.Add("&&", Token_Class.AndOp);
            Operators.Add("||", Token_Class.OrOp);
            Operators.Add("(", Token_Class.LParanthesis);
            Operators.Add(")", Token_Class.RParanthesis);
            Operators.Add(";", Token_Class.Semicolon);
            Operators.Add(",", Token_Class.Comma);
            Operators.Add("{", Token_Class.LBrace);
            Operators.Add("}", Token_Class.RBrace);
        }

        public void StartScanning(string code)
        {
            Tokens.Clear();
            Errors.Error_List.Clear();

            for (int i = 0; i < code.Length; i++)
            {
                char c = code[i];

                // Skip whitespace
                if (char.IsWhiteSpace(c))
                    continue;

                string lex = c.ToString();

                //  COMMENT 
                if (c == '/' && i + 1 < code.Length && code[i + 1] == '*')
                {
                    i += 2;

                    while (i + 1 < code.Length && !(code[i] == '*' && code[i + 1] == '/'))
                    {
                        i++;
                    }

                    if (i + 1 >= code.Length)
                    {
                        Errors.Error_List.Add("Unclosed comment");
                        return;
                    }

                    i++; // skip *
                    continue;
                }

                //  STRING 
                if (c == '"')
                {
                    int j = i + 1;
                    lex = "\"";

                    while (j < code.Length && code[j] != '"')
                    {
                        lex += code[j];
                        j++;
                    }

                    if (j >= code.Length)
                    {
                        Errors.Error_List.Add("Unclosed string");
                        return;
                    }

                    lex += "\"";
                    AddToken(lex);
                    i = j;
                    continue;
                }

                //  TWO-CHAR OPERATORS 
                if (i + 1 < code.Length)
                {
                    string two = lex + code[i + 1];
                    if (Operators.ContainsKey(two))
                    {
                        AddToken(two);
                        i++;
                        continue;
                    }
                }

                //  IDENTIFIER / RESERVED 
                if (char.IsLetter(c))
                {
                    int j = i;
                    lex = "";

                    while (j < code.Length && char.IsLetterOrDigit(code[j]))
                    {
                        lex += code[j];
                        j++;
                    }

                    AddToken(lex);
                    i = j - 1;
                    continue;
                }

                // NUMBER 
                if (char.IsDigit(c))
                {
                    int j = i;
                    lex = "";
                    bool hasDot = false;

                    while (j < code.Length &&
                           (char.IsDigit(code[j]) || (!hasDot && code[j] == '.')))
                    {
                        if (code[j] == '.') hasDot = true;

                        lex += code[j];
                        j++;
                    }

                    AddToken(lex);
                    i = j - 1;
                    continue;
                }

                //  SINGLE SYMBOL 
                AddToken(lex);
            }
        }

        void AddToken(string lex)
        {
            Token t = new Token();
            t.lex = lex;

            if (ReservedWords.ContainsKey(lex))
                t.token_type = ReservedWords[lex];

            else if (Operators.ContainsKey(lex))
                t.token_type = Operators[lex];

            else if (lex.StartsWith("\"") && lex.EndsWith("\""))
                t.token_type = Token_Class.StringValue;

            else if (char.IsDigit(lex[0]))
                t.token_type = Token_Class.Number;

            else if (char.IsLetter(lex[0]))
                t.token_type = Token_Class.Identifier;

            else
            {
                t.token_type = Token_Class.Identifier;
                Errors.Error_List.Add("Unknown token: " + lex);
            }

            Tokens.Add(t);
        }
    }
}
