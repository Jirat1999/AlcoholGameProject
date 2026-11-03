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

    /// <summary>Icon name (see Components/IconData.cs) shown for the category.</summary>
    public static string Icon(Category c) => c switch
    {
        Category.Truth => "message-circle-question-mark",
        Category.Dare => "flame",
        Category.Group => "users",
        Category.Rule => "scroll-text",
        Category.Vote => "vote",
        _ => "sparkles",
    };

    public static string Hint(Category c) => c switch
    {
        Category.Truth => "เปิดโปงความลับ",
        Category.Dare => "ทำตามคำท้า",
        Category.Group => "ทั้งวงมีส่วนร่วม",
        Category.Rule => "กติกาพิเศษ",
        Category.Vote => "ชี้นิ้วโหวตกัน",
        _ => "",
    };
}

/// <summary>A card template. <c>{player}</c> in <see cref="Text"/> is replaced with another player's name when drawn.</summary>
public sealed record Card(Category Category, string Text);
