using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpertSystemUIRuleCreator.Extension;

public static class Extensions
{
    public static IEnumerable<T> AppendToStart<T>(this IEnumerable<T> enumerable, T value)
    {
        var enumArr = enumerable.ToArray();
        var arr = new T[enumArr.Length + 1];
        arr[0] = value;
        if (enumArr.Length > 0)
            Array.Copy(enumArr, 0, arr, 1, enumArr.Length);
        return arr;
    }
}