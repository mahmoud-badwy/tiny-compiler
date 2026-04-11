using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny
{
    public enum Token_Class
    {
        // Reserved Words
        If, Repeat, Read, Write, Then, Else, ElseIf, End, Return,
        Int, Float, String,

        // Symbols
        LParanthesis, RParanthesis, Semicolon, Comma,

        // Operators
        Assign, PlusOp, MinusOp, MultiplyOp, DivideOp,
        LessThanOp, GreaterThanOp, EqualOp, NotEqualOp,
        AndOp, OrOp,

        
        Identifier, Number, StringValue
    }
}
