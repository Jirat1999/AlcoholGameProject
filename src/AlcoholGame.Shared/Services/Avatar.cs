namespace AlcoholGame.Shared.Services;

/// <summary>Colour and initial used to draw a player's avatar.</summary>
public static class Avatar
{
    private static readonly string[] Palette =
    [
        "#ff5d8f", "#ffb703", "#2ec4b6", "#8e7dff", "#4cc9f0", "#ff7f50",
        "#90be6d", "#f15bb5", "#00bbf9", "#f9c74f", "#9b5de5", "#06d6a0",
    ];

    public static string Color(int index) => Palette[((index % Palette.Length) + Palette.Length) % Palette.Length];

    /// <summary>First letter of the name; Thai leading vowels (เ แ โ ไ ใ) are skipped so the initial is a consonant.</summary>
    public static string Initial(string name)
    {
        name = name.Trim();
        if (name.Length == 0) return "?";
        var i = 0;
        while (i < name.Length - 1 && "เแโไใ".Contains(name[i])) i++;
        return char.ToUpperInvariant(name[i]).ToString();
    }
}
