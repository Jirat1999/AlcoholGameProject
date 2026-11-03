namespace AlcoholGame.Shared.Components;

/// <summary>Inner SVG markup (24x24 viewBox, stroke icons) from Lucide (ISC license, see THIRD-PARTY-NOTICES.md).</summary>
internal static class IconData
{
    public static readonly Dictionary<string, string> Paths = new()
    {
        ["beer"] = """<path d="M17 11h1a3 3 0 0 1 0 6h-1" /> <path d="M9 12v6" /> <path d="M13 12v6" /> <path d="M14 7.5c-1 0-1.44.5-3 .5s-2-.5-3-.5-1.72.5-2.5.5a2.5 2.5 0 0 1 0-5c.78 0 1.57.5 2.5.5S9.44 2 11 2s2 1.5 3 1.5 1.72-.5 2.5-.5a2.5 2.5 0 0 1 0 5c-.78 0-1.5-.5-2.5-.5Z" /> <path d="M5 8v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V8" />""",
        ["flame"] = """<path d="M12 3q1 4 4 6.5t3 5.5a1 1 0 0 1-14 0 5 5 0 0 1 1-3 1 1 0 0 0 5 0c0-2-1.5-3-1.5-5q0-2 2.5-4" />""",
        ["message-circle-question-mark"] = """<path d="M2.992 16.342a2 2 0 0 1 .094 1.167l-1.065 3.29a1 1 0 0 0 1.236 1.168l3.413-.998a2 2 0 0 1 1.099.092 10 10 0 1 0-4.777-4.719" /> <path d="M9.09 9a3 3 0 0 1 5.83 1c0 2-3 3-3 3" /> <path d="M12 17h.01" />""",
        ["scroll-text"] = """<path d="M15 12h-5" /> <path d="M15 8h-5" /> <path d="M19 17V5a2 2 0 0 0-2-2H4" /> <path d="M8 21h12a2 2 0 0 0 2-2v-1a1 1 0 0 0-1-1H11a1 1 0 0 0-1 1v1a2 2 0 1 1-4 0V5a2 2 0 1 0-4 0v2a1 1 0 0 0 1 1h3" />""",
        ["vote"] = """<path d="m9 12 2 2 4-4" /> <path d="M5 7c0-1.1.9-2 2-2h10a2 2 0 0 1 2 2v12H5V7Z" /> <path d="M22 19H2" />""",
        ["users"] = """<path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2" /> <path d="M16 3.128a4 4 0 0 1 0 7.744" /> <path d="M22 21v-2a4 4 0 0 0-3-3.87" /> <circle cx="9" cy="7" r="4" />""",
        ["dice-5"] = """<rect width="18" height="18" x="3" y="3" rx="2" ry="2" /> <path d="M16 8h.01" /> <path d="M8 8h.01" /> <path d="M8 16h.01" /> <path d="M16 16h.01" /> <path d="M12 12h.01" />""",
        ["arrow-right"] = """<path d="M5 12h14" /> <path d="m12 5 7 7-7 7" />""",
        ["check"] = """<path d="M20 6 9 17l-5-5" />""",
        ["x"] = """<path d="M18 6 6 18" /> <path d="m6 6 12 12" />""",
        ["plus"] = """<path d="M5 12h14" /> <path d="M12 5v14" />""",
        ["sparkles"] = """<path d="M11.017 2.814a1 1 0 0 1 1.966 0l1.051 5.558a2 2 0 0 0 1.594 1.594l5.558 1.051a1 1 0 0 1 0 1.966l-5.558 1.051a2 2 0 0 0-1.594 1.594l-1.051 5.558a1 1 0 0 1-1.966 0l-1.051-5.558a2 2 0 0 0-1.594-1.594l-5.558-1.051a1 1 0 0 1 0-1.966l5.558-1.051a2 2 0 0 0 1.594-1.594z" /> <path d="M20 2v4" /> <path d="M22 4h-4" /> <circle cx="4" cy="20" r="2" />""",
        ["martini"] = """<path d="M12 12 4.207 4.207A.707.707 0 0 1 4.707 3h14.586a.707.707 0 0 1 .5 1.207z" /> <path d="M12 12v10" /> <path d="M7 22h10" />""",
        ["wine"] = """<path d="M8 22h8" /> <path d="M7 10h10" /> <path d="M12 15v7" /> <path d="M12 15a5 5 0 0 0 5-5c0-2-.5-4-2-8H9c-1.5 4-2 6-2 8a5 5 0 0 0 5 5Z" />""",
    };
}
