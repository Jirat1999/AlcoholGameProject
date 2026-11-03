using AlcoholGame.Shared.Models;

namespace AlcoholGame.Shared.Services;

public sealed class DeckService
{
    public IReadOnlyList<Card> All { get; } = BuiltIn;

    /// <summary>Returns the cards of the given categories in random order (Fisher-Yates).</summary>
    public List<Card> Shuffle(IEnumerable<Category> categories, Random rng)
    {
        var set = categories.ToHashSet();
        var deck = All.Where(c => set.Contains(c.Category)).ToList();
        for (var i = deck.Count - 1; i > 0; i--)
        {
            var j = rng.Next(i + 1);
            (deck[i], deck[j]) = (deck[j], deck[i]);
        }
        return deck;
    }

    private static readonly Card[] BuiltIn =
    [
        // ความจริง
        new(Category.Truth, "เล่าเรื่องที่น่าอายที่สุดที่เคยทำตอนเมา ไม่ตอบ ดื่ม 2 จิบ"),
        new(Category.Truth, "บอกชื่อคนในวงที่คิดว่าเมาเร็วที่สุด"),
        new(Category.Truth, "เคยโกหกเพื่อนในวงเรื่องอะไร ไม่ตอบ ดื่ม 2 จิบ"),
        new(Category.Truth, "เล่าครั้งแรกที่ได้ลองดื่มแอลกอฮอล์"),
        new(Category.Truth, "บอกสิ่งที่ชอบที่สุดของ {player}"),
        new(Category.Truth, "เล่าความลับเล็กๆ ที่ยังไม่เคยบอกใครในวงนี้"),
        new(Category.Truth, "ข้อความล่าสุดที่ส่งหาแฟนเก่า/คนที่เคยชอบคืออะไร ไม่ตอบ ดื่ม 3 จิบ"),
        new(Category.Truth, "เคยแอบชอบใครในวงนี้ไหม ไม่ตอบ ดื่ม 2 จิบ"),
        new(Category.Truth, "เล่าช่วงเวลาที่แย่ที่สุดของการไปเที่ยวกับเพื่อน"),
        new(Category.Truth, "บอกนิสัยของตัวเองที่คนอื่นเกลียดที่สุด"),

        // ท้าทาย
        new(Category.Dare, "ทำท่าเต้นจากเพลงที่คนอื่นเลือก 15 วินาที ไม่ทำ ดื่ม 2 จิบ"),
        new(Category.Dare, "เลียนแบบ {player} ให้คนอื่นทาย ถ้าไม่มีใครทายถูก ดื่ม 2 จิบ"),
        new(Category.Dare, "พูดภาษาถิ่นต่างจากที่ตัวเองพูดจนถึงตาถัดไป"),
        new(Category.Dare, "ให้ {player} เลือกท่าโพสต์ถ่ายรูปแล้วทำตาม"),
        new(Category.Dare, "ร้องท่อนฮุกเพลงที่คนในวงสั่ง"),
        new(Category.Dare, "ห้ามใช้มือถือ 3 ตาถัดไป ใครเห็นว่าแอบใช้ ดื่ม 2 จิบ"),
        new(Category.Dare, "เล่านิทานสั้นๆ 3 ประโยค โดยทุกประโยคต้องขึ้นต้นด้วยตัวอักษรเดียวกัน"),
        new(Category.Dare, "ทำหน้าตลกที่สุดให้เพื่อนถ่ายรูป"),
        new(Category.Dare, "พูดชมทุกคนในวงคนละ 1 ประโยค ไม่ครบ ดื่ม 2 จิบ"),
        new(Category.Dare, "เล่นมุกตลกหนึ่งมุก ถ้าไม่มีใครขำ ดื่ม 2 จิบ"),

        // ทุกคน
        new(Category.Group, "ทุกคนที่เคยเมาจนจำอะไรไม่ได้ ดื่ม 1 จิบ"),
        new(Category.Group, "ทุกคนที่ใส่เสื้อสีดำ ดื่ม 1 จิบ"),
        new(Category.Group, "คนที่อายุน้อยที่สุดในวง ดื่ม 2 จิบ"),
        new(Category.Group, "คนที่ตื่นเช้าที่สุดวันนี้ ดื่ม 2 จิบ"),
        new(Category.Group, "ทุกคนที่เคยร้องไห้เพราะความรัก ดื่ม 1 จิบ"),
        new(Category.Group, "ทุกคนชี้ไปที่คนที่คิดว่าเมาง่ายที่สุด คนที่ถูกชี้มากสุด ดื่ม 2 จิบ"),
        new(Category.Group, "ทุกคนที่มือถือแบตเหลือน้อยกว่า 30% ดื่ม 1 จิบ"),
        new(Category.Group, "ทุกคนที่เคยโดนเทแล้วยังไม่ลืม ดื่ม 1 จิบ"),

        // กติกา
        new(Category.Rule, "{player} เป็นผู้คุมกติกา ตั้งกติกาหนึ่งข้อให้ทุกคนทำตามจนจบเกม"),
        new(Category.Rule, "ห้ามพูดคำว่า \"ไม่\" จนจบรอบนี้ ใครพูด ดื่ม 1 จิบ"),
        new(Category.Rule, "ทุกคนต้องเรียก {player} ว่า \"ท่านประธาน\" จนจบรอบนี้ ใครลืม ดื่ม 1 จิบ"),
        new(Category.Rule, "ห้ามชี้นิ้ว ใครชี้ ดื่ม 1 จิบ มีผลจนจบรอบนี้"),
        new(Category.Rule, "ก่อนดื่มทุกครั้งต้องพูดว่า \"ชนแก้ว\" ใครลืม ดื่มเพิ่ม 1 จิบ"),
        new(Category.Rule, "{player} เลือกคำต้องห้าม 1 คำ ใครพูดคำนั้น ดื่ม 1 จิบ มีผลจนจบรอบนี้"),

        // โหวต
        new(Category.Vote, "โหวตว่าใครในวงน่าจะดังที่สุดบนโซเชียล คนที่ได้โหวตมากสุด ดื่ม 2 จิบ"),
        new(Category.Vote, "โหวตว่าใครในวงน่าจะแต่งงานก่อน คนที่ได้โหวตมากสุด ดื่ม 2 จิบ"),
        new(Category.Vote, "โหวตว่าใครในวงน่าจะลืมกระเป๋าตังค์ที่ร้านมากที่สุด คนที่ได้โหวตมากสุด ดื่ม 2 จิบ"),
        new(Category.Vote, "โหวตว่าใครในวงน่าจะรอดจากซอมบี้ได้นานที่สุด คนที่ได้โหวตน้อยสุด ดื่ม 2 จิบ"),
        new(Category.Vote, "โหวตว่าใครในวงขี้เมาที่สุด คนที่ได้โหวตมากสุด ดื่ม 2 จิบ"),
        new(Category.Vote, "โหวตว่าใครในวงน่าจะเล่าเรื่องผีได้น่ากลัวที่สุด คนที่ได้โหวตมากสุด ดื่ม 1 จิบ"),
        new(Category.Vote, "โหวตว่าใครในวงน่าจะมาสายที่สุด คนที่ได้โหวตมากสุด ดื่ม 2 จิบ"),
    ];
}
