---
name: add-cards
description: Add, edit or review drinking-game cards in AlcoholGameProject (Thai text, categories, {player} placeholder) and verify with tests. Use when the user asks for new cards, a new category, or changes to card wording or rules.
---

# เพิ่ม/แก้การ์ดเกม

การ์ดทั้งหมดอยู่ใน `src/AlcoholGame.Shared/Services/DeckService.cs` ที่อาร์เรย์ `BuiltIn` หมวดต่างๆ อยู่ใน `src/AlcoholGame.Shared/Models/Card.cs`

## ขั้นตอน

1. อ่าน `DeckService.cs` เพื่อดูสไตล์และการ์ดที่มีอยู่ เพื่อไม่ให้เนื้อหาซ้ำ
2. เพิ่มการ์ดด้วยรูปแบบ `new(Category.X, "ข้อความ"),` ในกลุ่มของหมวดนั้น
3. ถ้าเพิ่ม **หมวดใหม่**:
   - เพิ่มค่าใน `enum Category` และชื่อไทยใน `CategoryInfo.Label` (Card.cs)
   - เพิ่มสีการ์ดใน `Pages/Play.razor.css` (`.card.cat-<ชื่อตัวพิมพ์เล็ก>`) และสีป้ายใน `Pages/Setup.razor.css` ถ้าต้องการ
   - หน้า Setup แสดงทุกหมวดอัตโนมัติ ไม่ต้องแก้
4. รัน `dotnet test tests/AlcoholGame.Shared.Tests` ต้องผ่านทั้งหมด
5. ถ้าแก้การทำงานของเกม (ไม่ใช่แค่ข้อความ) เพิ่ม test ใน `tests/AlcoholGame.Shared.Tests/GameTests.cs`

## กติกาของการ์ด

- **ความจริง / ท้าทาย**: พูดถึงผู้เล่นที่ถือตาอยู่ จึงไม่ขึ้นต้นด้วยชื่อ (เช่น "เล่าเรื่องที่น่าอายที่สุด...")
- **`{player}`**: แทนที่ด้วยผู้เล่น *คนอื่น* แบบสุ่ม ใช้ได้เมื่อการ์ดต้องมีอีกคนมาเกี่ยวข้อง ห้ามใช้ placeholder อื่นนอกจาก `{player}` (test จะล้ม)
- **ทุกคน / โหวต / กติกา**: ใช้กับทั้งวง ระบุให้ชัดว่าใครต้องดื่ม
- โทนสนุก ไม่ล่วงละเมิด ไม่เหยียด ไม่กดดันให้ดื่มเกินกำลัง ไม่เกี่ยวกับการขับขี่
- ใช้ "ดื่ม N จิบ" (1–3 จิบ) ไม่ระบุปริมาณแบบแก้ว/ช็อต และการ์ดที่ให้ดื่มควรมีทางเลือกเมื่อไม่ตอบหรือไม่ทำ ("ไม่ตอบ ดื่ม 2 จิบ")
- ข้อความสั้น อ่านจบในครั้งเดียวบนจอมือถือ (ไม่เกิน ~2 บรรทัด)
- เครื่องหมายคำพูดภายในสตริงให้ escape เป็น `\"`

## ตรวจก่อนจบงาน

- `dotnet test tests/AlcoholGame.Shared.Tests` ผ่าน
- ทุกหมวดมีการ์ดอย่างน้อย 1 ใบ
- สรุปให้ผู้ใช้ว่าเพิ่ม/แก้การ์ดอะไร จำนวนเท่าไหร่ในแต่ละหมวด
