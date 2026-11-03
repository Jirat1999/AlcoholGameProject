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

    public static string Emoji(Category c) => c switch
    {
        Category.Truth => "🤫",
        Category.Dare => "🔥",
        Category.Group => "🍻",
        Category.Rule => "📜",
        Category.Vote => "🗳️",
        _ => "🃏",
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
