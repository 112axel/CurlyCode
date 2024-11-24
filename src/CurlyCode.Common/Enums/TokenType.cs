﻿namespace CurlyCode.Common.Enums;
[Flags]
public enum TokenType
{
    Assignment = 1 << 0,
    NewLine = 1 << 1,
    Text = 1 << 2,
    Type = 1 << 3,
    Comment = 1 << 4,

    Number = 1 << 5,
    Add = 1 << 6,
    Subtract = 1 << 7,
    Divide = 1 << 8,
    Multiply = 1 << 9,

    End = 1 << 10,

    //contains data
    Data = Text ^ Type ^ Number
}
