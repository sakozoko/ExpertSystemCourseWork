using ExpertSystemUI.Model;

namespace ExpertSystemUI.Extension;

public static class Mapper
{
    public static string MapToString(this Condition? cond)
    {
        if (cond is null)
            return string.Empty;
        return (int)cond switch
        {
            -1 => "",
            0 => "=",
            1 => ">",
            2 => "<",
            _ => string.Empty
        };
    }

    public static Condition MapToCondition(this string? cond)
    {
        return cond switch
        {
            "=" => Condition.Equal,
            ">" => Condition.Greater,
            "<" => Condition.Less,
            _ => Condition.None
        };
    }
}