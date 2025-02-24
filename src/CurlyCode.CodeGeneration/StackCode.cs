﻿using CurlyCode.Common.Enums;

namespace CurlyCode.CodeGeneration;

public static class StackCode
{
    public static void AddValueToStack(StreamWriter writer, int number)
    {
        writer.WriteLine($"push {number}");
    }

    public static void GetValueFromStack(StreamWriter writer,int offset)
    {
        writer.WriteLine($"mov eax,[ebp+{offset*4}]");
    }

    //public static void PopValue(StreamWriter writer) {
    //    writer.WriteLine("");
    //}
}
