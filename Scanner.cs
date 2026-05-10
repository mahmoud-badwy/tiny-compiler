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
            ReservedWords.Add("main", Token_Class.Main);

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
            Operators.Add("[", Token_Class.LBracket);
            Operators.Add("]", Token_Class.RBracket);
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
                        i++;

                    if (i + 1 >= code.Length) // end without */
                    {
                        Errors.Error_List.Add("Unclosed comment");
                        return;
                    }

                    i++;
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

                    if (j >= code.Length) // no closing quote
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
                    string two = lex + code[i + 1]; // combine first two charachters
                    if (Operators.ContainsKey(two))
                    {
                        AddToken(two);
                        i++;
                        continue;
                    }
                }

                //  INVALID START (like ?omar) 
                if (!char.IsLetterOrDigit(c) && !Operators.ContainsKey(lex))
                {
                    int j = i;
                    lex = "";

                    while (j < code.Length &&
                           !char.IsWhiteSpace(code[j]) &&
                           !Operators.ContainsKey(code[j].ToString())) // to make sure ?omar taken as whole
                    {
                        lex += code[j];
                        j++;
                    }

                    Errors.Error_List.Add("Invalid token: " + lex);
                    i = j - 1;
                    continue;
                }

                //  UNIFIED WORD 
                if (char.IsLetter(c) || char.IsDigit(c))
                {
                    int j = i;
                    lex = "";

                    while (j < code.Length &&
                           !char.IsWhiteSpace(code[j]) &&
                           !Operators.ContainsKey(code[j].ToString()))
                    {
                        lex += code[j];
                        j++;
                    }

                    bool hasDot = false;
                    bool invalid = false;

                    foreach (char ch in lex)
                    {
                        if (char.IsLetterOrDigit(ch))
                        {
                            continue;
                        }
                        else if (ch == '.')
                        {
                            if (hasDot) // second dot
                                invalid = true;

                            hasDot = true;
                        }
                        else
                        {
                            invalid = true;//mark first dot
                        }
                    }

                    if (invalid)
                    {
                        Errors.Error_List.Add("Invalid token: " + lex);
                    }
                    else if (char.IsLetter(lex[0]))
                    {
                        AddToken(lex);
                    }
                    else if (char.IsDigit(lex[0]))
                    {
                        if (lex.IndexOfAny("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()) != -1) // to handle 2counter 
                            Errors.Error_List.Add("Invalid number: " + lex);
                        else
                            AddToken(lex);
                    }

                    i = j - 1;
                    continue;
                }

                //  SINGLE SYMBOL 
                if (Operators.ContainsKey(lex))
                {
                    AddToken(lex);
                }
                else
                {
                    Errors.Error_List.Add("Unknown token: " + lex); // & @
                }
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
                Errors.Error_List.Add("Unknown token: " + lex);
                return;
            }

            Tokens.Add(t);
        }
    }
}

// approved by omar