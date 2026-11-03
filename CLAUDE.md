# CLAUDE.md

เกมวงเหล้าแบบการ์ดคำสั่ง (UI ภาษาไทย) สร้างด้วย .NET 8 MAUI Blazor Hybrid: เขียนโค้ดครั้งเดียวใน Shared แล้วรันได้ทั้งแอป (Android/iOS/macOS/Windows) และเว็บ (Blazor WebAssembly)

## โครงสร้าง

```
AlcoholGameProject.sln
src/AlcoholGame.Shared/   Razor Class Library: หน้าจอ, component, โค้ดเกมทั้งหมด (แก้ที่นี่เป็นหลัก)
  Models/                 Card, Category, GameState
  Services/               DeckService (การ์ดในตัว + สับไพ่), Avatar (สี/ตัวอักษรอวตาร), AddAlcoholGame() สำหรับ DI
  Pages/                  Home (ยืนยัน 20+), Setup (/setup), Play (/play: จั่ว-เปิดการ์ด-ส่งต่อ)
  wwwroot/game.css        ธีมกลาง (สีพื้นหลัง, ตัวแปรสีหมวด .cat-*, ปุ่ม .btn-main) โหลดจาก index.html ของทั้ง Web และ Maui
  Layout/                 MainLayout (มือถือก่อน, ไม่มี sidebar)
src/AlcoholGame.Maui/     แอป MAUI: BlazorWebView โหลด App จาก Shared (ไม่ค่อยต้องแก้)
src/AlcoholGame.Web/      Blazor WASM: mount App จาก Shared (ไม่ค่อยต้องแก้)
tests/AlcoholGame.Shared.Tests/   xunit ทดสอบ GameState/DeckService
```

โค้ดเกมและ UI ต้องอยู่ใน Shared เท่านั้น ห้ามใส่โค้ดเฉพาะ MAUI หรือเฉพาะเว็บใน Shared (ถ้าต้องใช้ฟีเจอร์เครื่อง เช่น กล้อง/สั่น ให้ทำ interface ใน Shared แล้ว implement แยกใน Maui กับ Web)

## คำสั่งที่ใช้บ่อย

```bash
dotnet test tests/AlcoholGame.Shared.Tests            # unit test
dotnet run --project src/AlcoholGame.Web              # รันเว็บในเครื่อง
dotnet publish src/AlcoholGame.Web -c Release -o out  # publish เว็บ

# แอป MAUI (ต้อง dotnet workload install maui ก่อน)
dotnet build src/AlcoholGame.Maui -f net8.0-windows10.0.19041.0              # Windows
dotnet build src/AlcoholGame.Maui -t:Run -f net8.0-android                   # Android
# iOS / Mac Catalyst ต้อง build บน Mac เท่านั้น
```

MAUI build บน Linux ไม่ได้ ถ้าทำงานบน Linux ให้ตรวจได้เฉพาะ Shared, Web และ tests

## กติกาของเกม (ต้องคงไว้เมื่อแก้โค้ด)

- ผู้เล่น 2–12 คน ชื่อถูก trim, ตัดชื่อว่าง และตัดชื่อซ้ำแบบไม่สนตัวพิมพ์ (`GameState.Start`)
- การ์ดแต่ละใบขึ้นครั้งเดียวต่อรอบ หมดสำรับแล้วสับใหม่และ `Round++`
- การ์ดหมวดความจริง/ท้าทาย ส่งถึง **ผู้เล่นที่ถือตาอยู่** (แสดงบนหน้าจอ) จึงไม่ขึ้นต้นด้วยชื่อ
- `{player}` ในข้อความการ์ดหมายถึง **ผู้เล่นคนอื่น** ที่สุ่มมา ไม่ใช่คนที่ถือตา และต้องถูกแทนที่หมดก่อนแสดง
- ข้อความการ์ดใช้คำว่า "ดื่ม N จิบ" ไม่บังคับปริมาณตายตัว และต้องไม่มีการ์ดที่เสี่ยงอันตราย (ห้ามขับรถ ห้ามกดดันให้ดื่ม)
- ต้องคงหน้ายืนยัน 20+ และข้อความเตือนดื่มอย่างรับผิดชอบใน footer
- `GameState` เป็น singleton ใน DI (ลงทะเบียนผ่าน `AddAlcoholGame()` ใน Program.cs ของทั้ง Web และ Maui); `GameState.Rng` เปลี่ยนเป็น `Random` ที่กำหนด seed ได้เพื่อทดสอบ

## แนวทางเขียนโค้ด

- C# nullable enabled, file-scoped namespace, `record` สำหรับข้อมูลไม่เปลี่ยนค่า
- สไตล์ของแต่ละหน้าอยู่ใน `.razor.css` ข้างไฟล์ (CSS isolation) เน้นมือถือก่อน ปุ่มกดใหญ่ ความกว้างสูงสุด 480px; สี/ตัวแปรที่ใช้ร่วมกันอยู่ใน `wwwroot/game.css` (หมวดใหม่ต้องเพิ่ม `.cat-<ชื่อ>` ที่นี่ด้วย)
- UI ต้องให้ความรู้สึกเป็นเกม: หน้า Play ใช้การ์ดคว่ำที่ต้องแตะเพื่อเปิด (`.card-back` → `.card`) มีอนิเมชันพลิก และ `@key` ผูกกับรอบ/ใบ เพื่อให้อนิเมชันเล่นใหม่ทุกใบ; เคารพ `prefers-reduced-motion`
- ข้อความที่ผู้ใช้เห็นเป็นภาษาไทย ชื่อ class/method เป็นอังกฤษ
- เพิ่ม/แก้การ์ดที่ `Services/DeckService.cs` (`BuiltIn`) แล้วเพิ่ม test ถ้าเปลี่ยนกติกา; test เดิมตรวจว่าทุกหมวดมีการ์ดและ placeholder ถูกต้อง
- เพิ่มหน้าใหม่ใน `Shared/Pages/` ด้วย `@page` ไม่ต้องแก้ host

## ข้อควรระวัง

- ถ้าเขียนสคริปต์ทดสอบเบราว์เซอร์ (Playwright) อย่าเลือกปุ่มด้วยข้อความ (ข้อความการ์ดบางใบมีคำว่า "ถัดไป") ให้ใช้ selector: `.card-back` (เปิดการ์ด), `button.btn-main` (ผู้เล่นคนต่อไป / เริ่มเกม), `button.btn-link` (จบเกม), `.btn-add` (เพิ่มผู้เล่น); ต้องคลิก `.card-back` ก่อนจึงจะอ่าน `.text` ได้
- ปุ่มใน Blazor ผูก `@onclick` กับเมธอดของคอมโพเนนต์ (เช่น `NextCard`) ตามที่ Play.razor ทำ
- Branch หลักที่ใช้พัฒนาอยู่คือ `web-blazor-hybrid` (branch `maui-initial-project` เป็นแบบ MAUI เปล่า ไม่ใช้แล้ว)
