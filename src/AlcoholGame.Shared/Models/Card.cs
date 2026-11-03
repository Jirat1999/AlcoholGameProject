namespace AlcoholGame.Shared.Models;

public enum Category
{
    Truth,
    Dare,
    Group,
    Rule,
    Vote,
}

public static class CategoryInfo
{
    public static string Label(Category c) => c switch
    {
        Category.Truth => "ความจริง",
        Category.Dare => "ท้าทาย",
        Category.Group => "ทุกคน",
        Category.Rule => "กติกา",
        Category.Vote => "โหวต",
        _ => c.ToString(),
    };
}

/// <summary>A card template. <c>{player}</c> in <see cref="Text"/> is replaced with another player's name when drawn.</summary>
public sealed record Card(Category Category, string Text);
